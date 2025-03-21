using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace subs_check.win.gui
{
    public partial class Form1: Form
    {
        string 版本号;
        string 标题;
        private Process subsCheckProcess = null;
        string nodeInfo;//进度
        private Icon originalNotifyIcon; // 保存原始图标
        private ToolStripMenuItem startMenuItem;
        private ToolStripMenuItem stopMenuItem;
        string githubProxyURL;

        public Form1()
        {
            InitializeComponent();
            originalNotifyIcon = notifyIcon1.Icon;

            // 注册窗体大小改变事件
            this.Resize += new EventHandler(Form1_Resize);

            toolTip1.SetToolTip(numericUpDown1, "并发线程数：推荐 宽带峰值/50M。");
            toolTip1.SetToolTip(numericUpDown2, "检查间隔时间(分钟)：放置后台的时候，下次自动测速的间隔时间。");
            toolTip1.SetToolTip(numericUpDown3, "超时时间(毫秒)：节点的最大延迟。");
            toolTip1.SetToolTip(numericUpDown4, "最低测速结果舍弃(KB/s)。");
            toolTip1.SetToolTip(numericUpDown5, "下载测试时间(s)：与下载链接大小相关，默认最大测试10s。");
            toolTip1.SetToolTip(numericUpDown6, "本地监听端口：用于直接返回节点信息，方便订阅。");
            toolTip1.SetToolTip(textBox1, "节点池订阅地址：支持 base64/clash 格式的订阅链接。");
            toolTip1.SetToolTip(checkBox1, "以节点IP查询位置重命名节点。\n质量差的节点可能造成IP查询失败，造成整体检查速度稍微变慢。");
            toolTip1.SetToolTip(comboBox3, "GitHub 代理：代理订阅 GitHub raw 节点池。");
            toolTip1.SetToolTip(comboBox2, "测速地址：注意 并发数*节点速度<最大网速 否则测速结果不准确\n尽量不要使用Speedtest，Cloudflare提供的下载链接，因为很多节点屏蔽测速网站。");
            toolTip1.SetToolTip(textBox7, "将测速结果推送到Worker的地址。");
            toolTip1.SetToolTip(textBox6, "Worker令牌。");
            toolTip1.SetToolTip(comboBox1, "测速结果的保存方法。");
            toolTip1.SetToolTip(textBox2, "Gist ID：注意！非Github用户名！");
            toolTip1.SetToolTip(textBox3, "Github TOKEN");
            toolTip1.SetToolTip(textBox10, "Github用户名：无实际作用，只是方便生成订阅链接");
            // 设置通知图标的上下文菜单
            SetupNotifyIconContextMenu();
        }

        private void SetupNotifyIconContextMenu()
        {
            // 创建上下文菜单
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // 创建"启动"菜单项
            startMenuItem = new ToolStripMenuItem("启动");
            startMenuItem.Click += (sender, e) =>
            {
                if (button1.Text == "启动")
                {
                    button1_Click(sender, e);
                }
            };

            // 创建"停止"菜单项
            stopMenuItem = new ToolStripMenuItem("停止");
            stopMenuItem.Click += (sender, e) =>
            {
                if (button1.Text == "停止")
                {
                    button1_Click(sender, e);
                }
            };
            stopMenuItem.Enabled = false; // 初始状态下禁用

            // 创建"退出"菜单项
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("退出");
            exitMenuItem.Click += (sender, e) =>
            {
                try
                {
                    // 如果程序正在运行，先停止它
                    if (subsCheckProcess != null && !subsCheckProcess.HasExited)
                    {
                        StopSubsCheckProcess();
                    }

                    // 确保通知图标被移除
                    notifyIcon1.Visible = false;

                    // 使用更直接的退出方式
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"退出程序时发生错误: {ex.Message}", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // 如果仍然失败，尝试强制退出
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            };

            // 将菜单项添加到上下文菜单
            contextMenu.Items.Add(startMenuItem);
            contextMenu.Items.Add(stopMenuItem);
            contextMenu.Items.Add(new ToolStripSeparator()); // 分隔线
            contextMenu.Items.Add(exitMenuItem);

            // 将上下文菜单分配给通知图标
            notifyIcon1.ContextMenuStrip = contextMenu;

            // 确保通知图标可见
            notifyIcon1.Visible = true;
        }

        // 确保在窗体关闭时清理资源
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 如果程序正在运行，先停止它
            if (subsCheckProcess != null && !subsCheckProcess.HasExited)
            {
                StopSubsCheckProcess();
            }

            // 确保通知图标被移除
            notifyIcon1.Visible = false;

            base.OnFormClosing(e);
        }

        private async void timer1_Tick(object sender, EventArgs e)//初始化
        {
            timer1.Enabled = false;
            if (button2.Text == "高级设置∧") button2_Click(sender, e);
            // 检查并创建config文件夹
            string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string configFolderPath = System.IO.Path.Combine(executablePath, "config");
            if (!System.IO.Directory.Exists(configFolderPath))
            {
                // 文件不存在，可以给用户反馈
                string 免责声明 = "SubsCheck-Win-GUI 项目仅供教育、研究和安全测试目的而设计和开发。本项目旨在为安全研究人员、学术界人士及技术爱好者提供一个探索和实践网络通信技术的工具。\r\n在下载和使用本项目代码时，使用者必须严格遵守其所适用的法律和规定。使用者有责任确保其行为符合所在地区的法律框架、规章制度及其他相关规定。\r\n\r\n使用条款\r\n\r\n教育与研究用途：本软件仅可用于网络技术和编程领域的学习、研究和安全测试。\r\n禁止非法使用：严禁将 SubsCheck-Win-GUI 用于任何非法活动或违反使用者所在地区法律法规的行为。\r\n使用时限：基于学习和研究目的，建议用户在完成研究或学习后，或在安装后的24小时内，删除本软件及所有相关文件。\r\n免责声明：SubsCheck-Win-GUI 的创建者和贡献者不对因使用或滥用本软件而导致的任何损害或法律问题负责。\r\n用户责任：用户对使用本软件的方式以及由此产生的任何后果完全负责。\r\n无技术支持：本软件的创建者不提供任何技术支持或使用协助。\r\n知情同意：使用 SubsCheck-Win-GUI 即表示您已阅读并理解本免责声明，并同意受其条款的约束。\r\n\r\n请记住：本软件的主要目的是促进学习、研究和安全测试。创作者不支持或认可任何其他用途。使用者应当在合法和负责任的前提下使用本工具。\r\n\r\n同意以上条款请点击\"是 / Yes\"，否则程序将退出。";

                // 显示带有 "同意" 和 "拒绝" 选项的对话框
                DialogResult result = MessageBox.Show(免责声明, "SubsCheck-Win-GUI 免责声明", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // 如果用户点击 "拒绝" (对应于 No 按钮)
                if (result == DialogResult.No)
                {
                    // 退出程序
                    Environment.Exit(0); // 立即退出程序
                }
                System.IO.Directory.CreateDirectory(configFolderPath);
            }

            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            版本号 = "v" + myFileVersionInfo.FileVersion;
            标题 = "SubsCheck Win GUI " + 版本号;
            this.Text = 标题 + " TG:CMLiussss BY:CM喂饭 干货满满";
            comboBox1.Text = "本地";
            ReadConfig();
            /*
            string subsCheckPath = Path.Combine(executablePath, "subs-check.exe");
            if (File.Exists(subsCheckPath)) button1.Enabled = true;
            else 
            {
                Log("没有找到 subs-check.exe 文件。", true);
                MessageBox.Show("缺少 subs-check.exe 核心文件。\n\n您可以前往 https://github.com/beck-8/subs-check/releases 自行下载！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            */
            await CheckGitHubVersionAsync();
        }

        private async Task CheckGitHubVersionAsync()
        {
            try
            {
                // 首先检查是否有网络连接
                if (!IsNetworkAvailable())
                {
                    return; // 静默返回，不显示错误
                }

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                        client.Timeout = TimeSpan.FromSeconds(5); // 设置5秒超时

                        string url = "https://api.github.com/repos/cmliu/SubsCheck-Win-GUI/releases/latest";
                        HttpResponseMessage response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            JObject json = JObject.Parse(responseBody);
                            string latestVersion = json["tag_name"].ToString();

                            if (latestVersion != 版本号)
                            {
                                标题 = "SubsCheck Win GUI " + 版本号 + $"  发现新版本: {latestVersion} 请及时更新！";
                                this.Text = 标题;
                            }
                        }
                    }
                    catch
                    {
                        // 静默处理所有异常（网络错误、超时、JSON解析错误等）
                        return;
                    }
                }
            }
            catch
            {
                // 静默处理任何其他异常
                return;
            }
        }

        // 添加检查网络连接的辅助方法
        private bool IsNetworkAvailable()
        {
            try
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
            catch
            {
                return false; // 如果无法检查网络状态，假设网络不可用
            }
        }

        private void ReadConfig()//读取配置文件
        {
            try
            {
                string executablePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string configFilePath = Path.Combine(executablePath, "config", "config.yaml");

                if (File.Exists(configFilePath))
                {
                    // 读取YAML文件内容
                    string yamlContent = File.ReadAllText(configFilePath);

                    // 使用YamlDotNet解析YAML
                    var deserializer = new YamlDotNet.Serialization.Deserializer();
                    var config = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);

                    // 使用新函数获取整数值并设置UI控件
                    int? concurrentValue = 读取config整数(config, "concurrent");
                    if (concurrentValue.HasValue) numericUpDown1.Value = concurrentValue.Value;

                    int? checkIntervalValue = 读取config整数(config, "check-interval");
                    if (checkIntervalValue.HasValue) numericUpDown2.Value = checkIntervalValue.Value;

                    int? timeoutValue = 读取config整数(config, "timeout");
                    if (timeoutValue.HasValue) numericUpDown3.Value = timeoutValue.Value;

                    int? minspeedValue = 读取config整数(config, "min-speed");
                    if (minspeedValue.HasValue) numericUpDown4.Value = minspeedValue.Value;

                    int? downloadtimeoutValue = 读取config整数(config, "download-timeout");
                    if (downloadtimeoutValue.HasValue) numericUpDown5.Value = downloadtimeoutValue.Value;

                    string speedTestUrl = 读取config字符串(config, "speed-test-url");
                    if (speedTestUrl != null)  comboBox2.Text = speedTestUrl;

                    string savemethod = 读取config字符串(config, "save-method");
                    if (savemethod != null)
                    {
                        if (savemethod == "local") comboBox1.Text = "本地";
                        else  comboBox1.Text = savemethod;
                    }

                    string listenport = 读取config字符串(config, "listen-port");
                    if (listenport != null)
                    {
                        listenport = listenport.Replace(":", "");
                        numericUpDown6.Value = decimal.Parse(listenport);
                    }

                    string githubproxy = 读取config字符串(config, "githubproxy");
                    if (githubproxy != null) comboBox3.Text = githubproxy;

                    List<string> subUrls = 读取config列表(config, "sub-urls");
                    if (subUrls != null && subUrls.Count > 0)
                    {
                        // 处理URLs，检查是否包含GitHub raw链接
                        const string githubRawPrefix = "https://raw.githubusercontent.com/";
                        for (int i = 0; i < subUrls.Count; i++)
                        {
                            int index = subUrls[i].IndexOf(githubRawPrefix);
                            if (index > 0) // 如果找到且不在字符串开头
                            {
                                // 只保留从githubRawPrefix开始的部分
                                subUrls[i] = subUrls[i].Substring(index);
                            }
                        }

                        // 将列表中的每个URL放在单独的行上
                        textBox1.Text = string.Join(Environment.NewLine, subUrls);
                    }

                    string renamenode = 读取config字符串(config, "rename-node");
                    if (renamenode != null && renamenode == "true") checkBox1.Checked = true;
                    else checkBox1.Checked = false;

                    string githubgistid = 读取config字符串(config, "github-gist-id");
                    if (githubgistid != null) textBox2.Text = githubgistid;
                    string githubtoken = 读取config字符串(config, "github-token");
                    if (githubtoken != null) textBox3.Text = githubtoken;
                    string githubname = 读取config字符串(config, "github-name");
                    if (githubname != null) textBox10.Text = githubname;
                    string githubapimirror = 读取config字符串(config, "github-api-mirror");
                    if (githubapimirror != null) textBox4.Text = githubapimirror;

                    string workerurl = 读取config字符串(config, "worker-url");
                    if (workerurl != null) textBox7.Text = workerurl;
                    string workertoken = 读取config字符串(config, "worker-token");
                    if (workertoken != null) textBox6.Text = workertoken;

                    string webdavusername = 读取config字符串(config, "webdav-username");
                    if (webdavusername != null) textBox9.Text = webdavusername;
                    string webdavpassword = 读取config字符串(config, "webdav-password");
                    if (webdavpassword != null) textBox8.Text = webdavpassword;
                    string webdavurl = 读取config字符串(config, "webdav-url");
                    if (webdavurl != null) textBox5.Text = webdavurl;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取配置文件时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? 读取config整数(Dictionary<string, object> config, string fieldName)
        {
            // 检查是否存在指定字段且不为null
            if (config.ContainsKey(fieldName) && config[fieldName] != null)
            {
                int value;
                if (int.TryParse(config[fieldName].ToString(), out value))
                    return value;
            }
            return null;
        }

        private string 读取config字符串(Dictionary<string, object> config, string fieldName)
        {
            // 检查是否存在指定字段且不为null
            if (config.ContainsKey(fieldName) && config[fieldName] != null)
            {
                return config[fieldName].ToString();
            }
            return null;
        }

        private List<string> 读取config列表(Dictionary<string, object> config, string fieldName)
        {
            // 检查是否存在指定字段且不为null
            if (config.ContainsKey(fieldName) && config[fieldName] != null)
            {
                // 尝试将对象转换为列表
                if (config[fieldName] is List<object> listItems)
                {
                    return listItems.Select(item => item?.ToString()).Where(s => !string.IsNullOrEmpty(s)).ToList();
                }
            }
            return null;
        }

        private void SaveConfig()//保存配置文件
        {
            try
            {
                string executablePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string configFilePath = Path.Combine(executablePath, "config", "config.yaml");

                // 创建配置字典
                var config = new Dictionary<string, object>();

                // 从UI控件获取值并添加到字典中
                config["concurrent"] = (int)numericUpDown1.Value;
                config["check-interval"] = (int)numericUpDown2.Value;
                config["timeout"] = (int)numericUpDown3.Value;
                config["min-speed"] = (int)numericUpDown4.Value;
                config["download-timeout"] = (int)numericUpDown5.Value;

                if (!string.IsNullOrEmpty(comboBox2.Text)) config["speed-test-url"] = comboBox2.Text;

                // 保存save-method，将"本地"转换为"local"
                config["save-method"] = comboBox1.Text == "本地" ? "local" : comboBox1.Text;

                // 保存gist参数
                config["github-gist-id"] = textBox2.Text;
                config["github-token"] = textBox3.Text;
                config["github-name"] = textBox10.Text;
                config["github-api-mirror"] = textBox4.Text;

                // 保存r2参数
                config["worker-url"] = textBox7.Text;
                config["worker-token"] = textBox6.Text;

                // 保存webdav参数
                config["webdav-username"] = textBox9.Text;
                config["webdav-password"] = textBox8.Text;
                config["webdav-url"] = textBox5.Text;

                // 保存listen-port
                config["listen-port"] = $@":{numericUpDown6.Value}";

                // 保存githubproxy
                if (!string.IsNullOrEmpty(comboBox3.Text)) config["githubproxy"] = comboBox3.Text;

                // 保存sub-urls列表，将textBox1的文本按行分割为列表
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    var subUrls = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    // 检查并处理 GitHub Raw URLs
                    if (!string.IsNullOrEmpty(comboBox3.Text))
                    {
                        const string githubRawPrefix = "https://raw.githubusercontent.com/";
                        githubProxyURL = $"https://{comboBox3.Text}/";
                        // 将此代码添加到"自动选择"部分：
                        if (comboBox3.Text == "自动选择")
                        {
                            // 创建不包含"自动选择"的代理列表
                            List<string> proxyItems = new List<string>();
                            for (int j = 0; j < comboBox3.Items.Count; j++)
                            {
                                string proxyItem = comboBox3.Items[j].ToString();
                                if (proxyItem != "自动选择")
                                    proxyItems.Add(proxyItem);
                            }

                            // 随机打乱列表顺序
                            Random random = new Random();
                            proxyItems = proxyItems.OrderBy(x => random.Next()).ToList();

                            bool proxyFound = false;
                            Log("检测可用 GitHub 代理...");

                            // 遍历随机排序后的代理列表
                            foreach (string proxyItem in proxyItems)
                            {
                                string checkUrl = $"https://{proxyItem}/https://raw.githubusercontent.com/cmliu/SubsCheck-Win-GUI/master/packages.config";
                                Log($"正在测试 GitHub 代理: {proxyItem}");
                                richTextBox1.Refresh();

                                try
                                {
                                    using (HttpClient client = new HttpClient())
                                    {
                                        client.Timeout = TimeSpan.FromSeconds(5); // 设置5秒超时
                                        // 添加User-Agent头，避免被拒绝访问
                                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");

                                        HttpResponseMessage response = client.GetAsync(checkUrl).Result;
                                        if (response.IsSuccessStatusCode)
                                        {
                                            // 找到可用代理
                                            githubProxyURL = $"https://{proxyItem}/";
                                            Log($"找到可用 GitHub 代理: {proxyItem}");
                                            proxyFound = true;
                                            break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // 记录错误但继续尝试下一个
                                    Log($"代理 {proxyItem} 测试失败: {ex.Message}", true);
                                    richTextBox1.Refresh();
                                }
                            }

                            // 如果没有找到可用的代理
                            if (!proxyFound)
                            {
                                Log("未找到可用的 GitHub 代理，请在高级设置中手动设置。", true);
                                MessageBox.Show("未找到可用的 GitHub 代理。\n\n请打开高级设置手动填入一个可用的Github Proxy，或检查您的网络连接。",
                                    "代理检测失败",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                // 如果没有找到可用代理，设置为空
                                githubProxyURL = "";
                            }
                        }

                        for (int i = 0; i < subUrls.Count; i++)
                        {
                            if (subUrls[i].StartsWith(githubRawPrefix))
                            {
                                // 替换为代理 URL 格式
                                subUrls[i] = githubProxyURL  + githubRawPrefix + subUrls[i].Substring(githubRawPrefix.Length);
                            }
                        }
                    }

                    if (subUrls.Count > 0)
                        config["sub-urls"] = subUrls;
                }

                config["rename-node"] = checkBox1.Checked;//以节点IP查询位置重命名节点
                config["keep-success-proxies"] = true;//保留之前测试成功的节点
                config["print-progress"] = true;//是否显示进度
                config["sub-urls-retry"] = 3;//重试次数(获取订阅失败后重试次数)

                // 使用YamlDotNet序列化配置
                var serializer = new YamlDotNet.Serialization.SerializerBuilder()
                    .WithIndentedSequences()  // 使序列化结果更易读
                    .Build();

                string yamlContent = serializer.Serialize(config);

                // 确保配置目录存在
                string configDirPath = Path.GetDirectoryName(configFilePath);
                if (!Directory.Exists(configDirPath))
                    Directory.CreateDirectory(configDirPath);

                // 写入YAML文件
                File.WriteAllText(configFilePath, yamlContent);

                //MessageBox.Show("配置已成功保存。", "提示",  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配置文件时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "高级设置∧")
            {
                button2.Text = "高级设置∨";
                groupBox3.Visible = false;
            }
            else
            {
                button2.Text = "高级设置∧";
                groupBox3.Visible = true;
            }
            判断保存类型();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "启动")
            {
                if (button3.Enabled==false || button4.Enabled == false)
                {
                    string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                    string outputFolderPath = System.IO.Path.Combine(executablePath, "output");
                    if (System.IO.Directory.Exists(outputFolderPath))
                    {
                        string alltxtFilePath = System.IO.Path.Combine(outputFolderPath, "all.txt");
                        if (System.IO.File.Exists(alltxtFilePath))
                        {
                            button3.Enabled = true;
                        }
                        string allyamlFilePath = System.IO.Path.Combine(outputFolderPath, "all.yaml");
                        if (System.IO.File.Exists(allyamlFilePath))
                        {
                            button4.Enabled = true;
                        }
                    }
                }

                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;
                numericUpDown6.Enabled = false;
                comboBox1.Enabled = false;
                textBox1.ReadOnly = true;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                groupBox6.Enabled = false;
                button1.Text = "停止";

                // 清空 richTextBox1
                richTextBox1.Clear();
                SaveConfig();

                // 更新菜单项的启用状态
                startMenuItem.Enabled = false;
                stopMenuItem.Enabled = true;

                // 清空 richTextBox1
                //richTextBox1.Clear();

                notifyIcon1.Text = "SubsCheck: 已就绪";

                // 启动 subs-check.exe 程序
                StartSubsCheckProcess();
            }
            else
            {
                progressBar1.Value = 0;
                groupBox2.Text = "实时日志";
                notifyIcon1.Text = "SubsCheck: 未运行";
                // 停止 subs-check.exe 程序
                StopSubsCheckProcess();
                button3.Enabled = false;
                button4.Enabled = false;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = true;
                numericUpDown6.Enabled = true;
                comboBox1.Enabled = true;
                textBox1.ReadOnly = false;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                groupBox6.Enabled = true;
                button1.Text = "启动";

                // 更新菜单项的启用状态
                startMenuItem.Enabled = true;
                stopMenuItem.Enabled = false;
            }
        }

        private async Task DownloadSubsCheckEXE()
        {
            try
            {
                Log("正在检查网络连接...");

                // 首先检查是否有网络连接
                if (!IsNetworkAvailable())
                {
                    Log("网络连接不可用，无法下载核心文件。", true);
                    MessageBox.Show("缺少 subs-check.exe 核心文件。\n\n您可以前往 https://github.com/beck-8/subs-check/releases 自行下载！",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");
                        client.Timeout = TimeSpan.FromSeconds(30); // 增加超时时间以适应下载需求

                        Log("正在获取最新版本 subs-check.exe 内核下载地址...");
                        string url = "https://api.github.com/repos/beck-8/subs-check/releases/latest";

                        // 使用异步方法
                        HttpResponseMessage response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            // 异步读取内容
                            string responseBody = await response.Content.ReadAsStringAsync();
                            JObject json = JObject.Parse(responseBody);
                            string latestVersion = json["tag_name"].ToString();
                            Log($"subs-check.exe 最新版本为: {latestVersion} ");
                            // 查找Windows i386版本的资源
                            string downloadUrl = null;
                            JArray assets = (JArray)json["assets"];
                            foreach (var asset in assets)
                            {
                                if (asset["name"].ToString() == "subs-check_Windows_i386.zip")
                                {
                                    downloadUrl = asset["browser_download_url"].ToString();
                                    break;
                                }
                            }

                            if (downloadUrl != null)
                            {
                                string executablePath = Path.GetDirectoryName(Application.ExecutablePath);
                                string zipFilePath = Path.Combine(executablePath, "subs-check_Windows_i386.zip");
                                // 如果文件已存在，先删除
                                if (File.Exists(zipFilePath)) File.Delete(zipFilePath);

                                Log($"开始下载 {downloadUrl}");

                                // 重置进度条
                                progressBar1.Value = 0;

                                // 获取文件大小
                                HttpResponseMessage sizeResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, githubProxyURL + downloadUrl));
                                long totalBytes = sizeResponse.Content.Headers.ContentLength ?? 0;

                                // 创建下载请求
                                using (var downloadResponse = await client.GetAsync(githubProxyURL + downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                                using (var contentStream = await downloadResponse.Content.ReadAsStreamAsync())
                                using (var fileStream = new FileStream(zipFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                                {
                                    byte[] buffer = new byte[8192];
                                    long totalBytesRead = 0;
                                    int bytesRead;

                                    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                    {
                                        await fileStream.WriteAsync(buffer, 0, bytesRead);

                                        totalBytesRead += bytesRead;

                                        // 更新进度条
                                        if (totalBytes > 0)
                                        {
                                            int progressPercentage = (int)((totalBytesRead * 100) / totalBytes);
                                            // 确保进度值在有效范围内 (0-100)
                                            progressPercentage = Math.Min(100, Math.Max(0, progressPercentage));
                                            progressBar1.Value = progressPercentage;
                                        }
                                    }
                                }

                                Log("下载完成，正在解压文件...");

                                // 解压文件
                                using (System.IO.Compression.ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(zipFilePath))
                                {
                                    // 查找subs-check.exe
                                    System.IO.Compression.ZipArchiveEntry exeEntry = archive.Entries.FirstOrDefault(
                                        entry => entry.Name.Equals("subs-check.exe", StringComparison.OrdinalIgnoreCase));

                                    if (exeEntry != null)
                                    {
                                        string exeFilePath = Path.Combine(executablePath, "subs-check.exe");

                                        // 如果文件已存在，先删除
                                        if (File.Exists(exeFilePath))
                                        {
                                            File.Delete(exeFilePath);
                                        }

                                        // 解压文件
                                        exeEntry.ExtractToFile(exeFilePath);

                                        Log("subs-check.exe 已成功安装！");
                                        // 这里保留原有行为，不修改button1.Enabled

                                        // 删除下载的zip文件
                                        //File.Delete(zipFilePath);
                                    }
                                    else
                                    {
                                        Log("无法在压缩包中找到 subs-check.exe 文件。", true);
                                    }
                                }
                            }
                            else
                            {
                                Log("无法找到适用于 Windows i386 的下载链接。", true);
                                MessageBox.Show("未能找到适用的 subs-check.exe 下载链接。\n\n请前往 https://github.com/beck-8/subs-check/releases 自行下载！",
                                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            Log($"获取版本信息失败: HTTP {(int)response.StatusCode} {response.ReasonPhrase}", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"下载过程中出错: {ex.Message}", true);
                        MessageBox.Show($"下载 subs-check.exe 时出错: {ex.Message}\n\n请前往 https://github.com/beck-8/subs-check/releases 自行下载！",
                            "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"初始化下载过程出错: {ex.Message}", true);
                MessageBox.Show($"下载准备过程出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void StartSubsCheckProcess()
        {
            try
            {
                // 重置进度条
                progressBar1.Value = 0;
                groupBox2.Text = "实时日志";
                using (MemoryStream ms = new MemoryStream(Properties.Resources.going))
                {
                    notifyIcon1.Icon = new Icon(ms);
                }
                // 获取当前应用程序目录
                string executablePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string subsCheckPath = Path.Combine(executablePath, "subs-check.exe");

                // 检查文件是否存在
                if (!File.Exists(subsCheckPath))
                {
                    Log("没有找到 subs-check.exe 文件。", true);
                    await DownloadSubsCheckEXE(); // 使用异步等待
                    /*
                    button1.Text = "启动";
                    button1.Enabled = false;
                    return;
                    */
                }

                // 创建进程启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = subsCheckPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = executablePath,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                // 创建进程
                subsCheckProcess = new Process { StartInfo = startInfo };

                // 设置输出和错误数据接收事件处理
                subsCheckProcess.OutputDataReceived += SubsCheckProcess_OutputDataReceived;
                subsCheckProcess.ErrorDataReceived += SubsCheckProcess_OutputDataReceived;

                // 启动进程
                subsCheckProcess.Start();

                // 开始异步读取输出
                subsCheckProcess.BeginOutputReadLine();
                subsCheckProcess.BeginErrorReadLine();

                // 设置进程退出事件处理
                subsCheckProcess.EnableRaisingEvents = true;
                subsCheckProcess.Exited += SubsCheckProcess_Exited;

                Log("subs-check.exe 已启动...");
            }
            catch (Exception ex)
            {
                Log($"启动 subs-check.exe 时出错: {ex.Message}", true);
                button1.Text = "启动";
            }
        }

        private void StopSubsCheckProcess()
        {
            if (subsCheckProcess != null && !subsCheckProcess.HasExited)
            {
                try
                {
                    // 尝试正常关闭进程
                    subsCheckProcess.Kill();
                    subsCheckProcess.WaitForExit();
                    Log("subs-check.exe 已停止");
                    notifyIcon1.Icon = originalNotifyIcon;
                }
                catch (Exception ex)
                {
                    Log($"停止 subs-check.exe 时出错: {ex.Message}", true);
                }
                finally
                {
                    subsCheckProcess.Dispose();
                    subsCheckProcess = null;
                }
            }
        }

        private string lastProgressLine = null; // 这个变量已经在类中定义，用于记录最后的进度行
        private string nextCheckTime = null;// 用于存储下次检查时间
        private void SubsCheckProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                // 由于此事件在另一个线程中触发，需要使用 Invoke 在 UI 线程上更新控件
                BeginInvoke(new Action(() =>
                {
                    // 过滤ANSI转义序列
                    string cleanText = RemoveAnsiEscapeCodes(e.Data);

                    // 检查是否包含"下次检查时间"信息
                    if (cleanText.Contains("下次检查时间:"))
                    {
                        if (button3.Enabled == false || button4.Enabled == false)
                        {
                            string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                            string outputFolderPath = System.IO.Path.Combine(executablePath, "output");
                            if (System.IO.Directory.Exists(outputFolderPath))
                            {
                                string alltxtFilePath = System.IO.Path.Combine(outputFolderPath, "all.txt");
                                if (System.IO.File.Exists(alltxtFilePath))
                                {
                                    button3.Enabled = true;
                                }
                                string allyamlFilePath = System.IO.Path.Combine(outputFolderPath, "all.yaml");
                                if (System.IO.File.Exists(allyamlFilePath))
                                {
                                    button4.Enabled = true;
                                }
                            }
                        }
                        // 提取完整的下次检查时间信息
                        int startIndex = cleanText.IndexOf("下次检查时间:");
                        nextCheckTime = cleanText.Substring(startIndex);

                        // 确保通知图标文本不超过63个字符
                        string notifyText = "SubsCheck: " + nodeInfo + "\n" + nextCheckTime;
                        if (notifyText.Length > 63)
                        {
                            // 优先保留节点信息
                            int remainingLength = 63 - ("SubsCheck: ".Length + nodeInfo.Length);
                            if (remainingLength > 5) // 确保至少有足够空间显示部分下次检查时间
                            {
                                notifyText = "SubsCheck: " + nodeInfo + "\n" + nextCheckTime.Substring(0, Math.Min(remainingLength, nextCheckTime.Length));
                            }
                            else
                            {
                                notifyText = "SubsCheck: " + nodeInfo;
                            }
                        }
                        notifyIcon1.Text = notifyText;
                    }

                    // 检查是否是进度信息行
                    if (cleanText.StartsWith("进度: ["))
                    {
                        // 解析百分比
                        int percentIndex = cleanText.IndexOf('%');
                        if (percentIndex > 0)
                        {
                            // 查找百分比前面的数字部分
                            int startIndex = cleanText.LastIndexOfAny(new char[] { ' ', '>' }, percentIndex) + 1;
                            string percentText = cleanText.Substring(startIndex, percentIndex - startIndex);

                            if (double.TryParse(percentText, out double percentValue))
                            {
                                // 更新进度条，将百分比值（0-100）设置给进度条
                                progressBar1.Value = (int)Math.Round(percentValue);
                            }
                        }

                        // 解析节点信息部分（例如：(12/6167) 可用: 0）
                        int infoStartIndex = cleanText.IndexOf('(');
                        if (infoStartIndex > 0)
                        {
                            string fullNodeInfo = cleanText.Substring(infoStartIndex);

                            // 提取最重要的信息：节点数量和可用数量
                            int endIndex = fullNodeInfo.IndexOf("2025-"); // 查找日期部分开始位置
                            if (endIndex > 0)
                            {
                                nodeInfo = fullNodeInfo.Substring(0, endIndex).Trim();
                            }
                            else
                            {
                                // 如果找不到日期部分，则取前30个字符
                                nodeInfo = fullNodeInfo.Length > 30 ? fullNodeInfo.Substring(0, 30) + "..." : fullNodeInfo;
                            }

                            groupBox2.Text = "实时日志 " + nodeInfo;

                            // 确保通知图标文本不超过63个字符
                            string notifyText = "SubsCheck: " + nodeInfo;
                            if (notifyText.Length > 63)
                            {
                                notifyText = notifyText.Substring(0, 60) + "...";
                            }
                            notifyIcon1.Text = notifyText;
                        }

                        // 更新lastProgressLine，但不向richTextBox添加文本
                        lastProgressLine = cleanText;
                    }
                    else
                    {
                        // 如果不是进度行，则添加到日志中
                        richTextBox1.AppendText(cleanText + "\r\n");
                        // 滚动到最底部
                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.ScrollToCaret();
                    }
                }));
            }
        }


        // 添加一个方法来过滤ANSI转义序列
        private string RemoveAnsiEscapeCodes(string input)
        {
            // 匹配ANSI转义序列的正则表达式
            // 这将匹配类似 "[2m"、"[0m"、"[92m" 等格式的ANSI颜色代码
            return System.Text.RegularExpressions.Regex.Replace(input, @"\x1B\[[0-9;]*[mGK]", string.Empty);
        }

        private void SubsCheckProcess_Exited(object sender, EventArgs e)
        {
            // 进程退出时，在 UI 线程上更新控件
            BeginInvoke(new Action(() =>
            {
                Log("subs-check.exe 已退出");
                button1.Text = "启动";

                // 更新菜单项的启用状态
                startMenuItem.Enabled = true;
                stopMenuItem.Enabled = false;

                // 重新启用控件
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = true;
                numericUpDown6.Enabled = true;
                textBox1.ReadOnly = false;
                groupBox3.Enabled = true;
            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // 构造URL
                string url = $"http://127.0.0.1:{numericUpDown6.Value}/all.txt";
                if(comboBox1.Text== "gist")
                {
                    if (textBox10.Text != null && textBox10.Text != "")
                    {
                        url = $"https://gist.githubusercontent.com/{textBox10.Text}/{textBox2.Text}/raw/all.txt";
                    }
                    else
                    {
                        // 弹出提示框，提示github name为空
                        MessageBox.Show("GitHub 用户名为空，无法获取 Gist 订阅链接！",
                                       "提示",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning);
                        return; // 提前返回，不复制空链接
                    }
                } 
                else if(comboBox1.Text == "r2")
                {
                    url = $"{textBox7.Text}/storage?key=all.txt&token={textBox6.Text}";
                }
                // 将URL复制到剪贴板
                Clipboard.SetText(url);
                button4.Text = "Clash订阅";
                button3.Text = "复制成功";
                timer2.Enabled = false;
                timer2.Enabled = true;
                // 可选：显示提示消息
                //MessageBox.Show($"URL已复制到剪贴板：\n{url}", "复制成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"复制到剪贴板时出错：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // 构造URL
                string url = $"http://127.0.0.1:{numericUpDown6.Value}/all.yaml";
                if (comboBox1.Text == "gist")
                {
                    if (textBox10.Text != null && textBox10.Text != "")
                    {
                        url = $"https://gist.githubusercontent.com/{textBox10.Text}/{textBox2.Text}/raw/all.yaml";
                    }
                    else
                    {
                        // 弹出提示框，提示github name为空
                        MessageBox.Show("GitHub 用户名为空，无法获取 Gist 订阅链接！",
                                       "提示",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning);
                        return; // 提前返回，不复制空链接
                    }
                }
                else if (comboBox1.Text == "r2")
                {
                    url = $"{textBox7.Text}/storage?key=all.yaml&token={textBox6.Text}";
                }
                // 将URL复制到剪贴板
                Clipboard.SetText(url);
                button3.Text = "Base64订阅";
                button4.Text = "复制成功";
                timer2.Enabled = false;
                timer2.Enabled = true;
                // 可选：显示提示消息
                //MessageBox.Show($"URL已复制到剪贴板：\n{url}", "复制成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"复制到剪贴板时出错：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            button3.Text = "Base64订阅";
            button4.Text = "Clash订阅";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // 当窗口被最小化时
            if (this.WindowState == FormWindowState.Minimized)
            {
                // 隐藏窗体（从任务栏消失）
                this.Hide();

                // 确保通知图标可见
                notifyIcon1.Visible = true;

                // 可选：显示气泡提示
                notifyIcon1.ShowBalloonTip(1000, "SubsCheck", "程序已最小化到系统托盘", ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 只处理鼠标左键双击
            if (e.Button == MouseButtons.Left)
            {
                // 显示窗体
                this.Show();

                // 恢复窗口状态
                this.WindowState = FormWindowState.Normal;

                // 激活窗口（使其获得焦点）
                this.Activate();
            }
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            // 检查是否有内容
            if (string.IsNullOrWhiteSpace(comboBox3.Text))
                return;

            string input = comboBox3.Text.Trim();

            // 检查是否存在 "://" 协议部分
            int protocolIndex = input.IndexOf("://");
            if (protocolIndex >= 0)
            {
                // 保留 "://" 之后的内容
                input = input.Substring(protocolIndex + 3);
            }

            // 检查是否存在 "/" 路径部分
            int pathIndex = input.IndexOf('/');
            if (pathIndex >= 0)
            {
                // 只保留 "/" 之前的域名部分
                input = input.Substring(0, pathIndex);
            }

            // 更新 comboBox3 的文本
            comboBox3.Text = input;
        }

        private void 判断保存类型()
        {
            if (comboBox1.Text == "本地" || button2.Text == "高级设置∨")
            {
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                groupBox6.Visible = false;
            }
            else if (comboBox1.Text == "gist" && button2.Text == "高级设置∧")
            {
                groupBox4.Visible = true;

                groupBox5.Visible = false;
                groupBox6.Visible = false;
            }
            else if (comboBox1.Text == "r2" && button2.Text == "高级设置∧")
            {
                groupBox5.Location = groupBox4.Location;
                groupBox5.Visible = true;

                groupBox4.Visible = false;
                groupBox6.Visible = false;
            }
            else if (comboBox1.Text == "webdav" && button2.Text == "高级设置∧")
            {
                groupBox6.Location = groupBox4.Location;
                groupBox6.Visible = true;

                groupBox4.Visible = false;
                groupBox5.Visible = false;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            判断保存类型();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '\0';
            textBox6.PasswordChar = '\0';
            textBox8.PasswordChar = '\0';
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
            textBox6.PasswordChar = '*';
            textBox8.PasswordChar = '*';
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            // 检查是否有内容
            if (string.IsNullOrWhiteSpace(textBox7.Text))
                return;

            string input = textBox7.Text.Trim();

            try
            {
                // 尝试解析为 URI
                Uri uri = new Uri(input);

                // 构建基础 URL (scheme + authority)
                string baseUrl = $"{uri.Scheme}://{uri.Authority}";

                // 更新 textBox7 的文本为基础 URL
                textBox7.Text = baseUrl;
            }
            catch (UriFormatException)
            {
                // 如果输入的不是有效 URI，尝试使用简单的字符串处理
                // 查找双斜杠后的第一个斜杠
                int schemeIndex = input.IndexOf("://");
                if (schemeIndex >= 0)
                {
                    int pathStartIndex = input.IndexOf('/', schemeIndex + 3);
                    if (pathStartIndex >= 0)
                    {
                        // 截取到路径开始之前
                        textBox7.Text = input.Substring(0, pathStartIndex);
                    }
                }
            }
        }

        private void Log(string message, bool isError = false)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logType = isError ? "ERR" : "INF";
            richTextBox1.AppendText($"{timestamp} {logType} {message}\r\n");

            // 滚动到最底部
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
