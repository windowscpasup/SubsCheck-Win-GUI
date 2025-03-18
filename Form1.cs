using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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

        public Form1()
        {
            InitializeComponent();
            originalNotifyIcon = notifyIcon1.Icon;

            // 注册窗体大小改变事件
            this.Resize += new EventHandler(Form1_Resize);

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

        private void timer1_Tick(object sender, EventArgs e)//初始化
        {
            // 检查并创建config文件夹
            string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string configFolderPath = System.IO.Path.Combine(executablePath, "config");
            if (!System.IO.Directory.Exists(configFolderPath))
            {
                System.IO.Directory.CreateDirectory(configFolderPath);
            }

            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            版本号 = "v" + myFileVersionInfo.FileVersion;
            标题 = "SubsCheck Win GUI " + 版本号;
            this.Text = 标题 + " TG:CMLiussss BY:CM喂饭 干货满满";

            comboBox1.Text = "本地";
            ReadConfig();
            if (button2.Text == "高级设置∧")
            {
                button2_Click(sender, e);
            }
            timer1.Enabled = false;
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

                }
                else
                {
                    MessageBox.Show("配置文件不存在，将使用默认设置。", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 创建一个示例配置文件
                    CreateSampleConfigFile(configFilePath);
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

        private void CreateSampleConfigFile(string configFilePath)
        {
            try
            {
                // 创建一个简单的配置示例
                string sampleConfig =
@"# 配置文件示例 https://github.com/beck-8/subs-check/blob/master/config/config.example.yaml
concurrent: 16
check_interval: 300
timeout: 5000
speed_limit: 1024
speed_test_time: 10
subscription_port: 8199
save_method: local
";
                File.WriteAllText(configFilePath, sampleConfig);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"创建示例配置文件时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (!string.IsNullOrEmpty(comboBox2.Text))
                    config["speed-test-url"] = comboBox2.Text;

                // 保存save-method，将"本地"转换为"local"
                config["save-method"] = comboBox1.Text == "本地" ? "local" : comboBox1.Text;

                // 保存listen-port
                config["listen-port"] = $@":{numericUpDown6.Value}";

                // 保存githubproxy
                if (!string.IsNullOrEmpty(comboBox3.Text))
                    config["githubproxy"] = comboBox3.Text;

                // 保存sub-urls列表，将textBox1的文本按行分割为列表
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    var subUrls = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    // 检查并处理 GitHub Raw URLs
                    if (!string.IsNullOrEmpty(comboBox3.Text))
                    {
                        const string githubRawPrefix = "https://raw.githubusercontent.com/";
                        for (int i = 0; i < subUrls.Count; i++)
                        {
                            if (subUrls[i].StartsWith(githubRawPrefix))
                            {
                                // 替换为代理 URL 格式
                                subUrls[i] = "https://" + comboBox3.Text + "/" + githubRawPrefix + subUrls[i].Substring(githubRawPrefix.Length);
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
                SaveConfig();
                button1.Text = "停止";

                // 更新菜单项的启用状态
                startMenuItem.Enabled = false;
                stopMenuItem.Enabled = true;

                // 清空 richTextBox1
                richTextBox1.Clear();

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
                //comboBox1.Enabled = true;
                textBox1.ReadOnly = false;
                groupBox3.Enabled = true;
                button1.Text = "启动";

                // 更新菜单项的启用状态
                startMenuItem.Enabled = true;
                stopMenuItem.Enabled = false;
            }
        }

        private void StartSubsCheckProcess()
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
                    richTextBox1.AppendText("错误: 没有找到 subs-check.exe 文件。\r\n");
                    button1.Text = "启动";
                    button1.Enabled = false;
                    return;
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

                richTextBox1.AppendText("subs-check.exe 已启动...\r\n");
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText($"启动 subs-check.exe 时出错: {ex.Message}\r\n");
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
                    richTextBox1.AppendText("subs-check.exe 已停止\r\n");
                    notifyIcon1.Icon = originalNotifyIcon;
                }
                catch (Exception ex)
                {
                    richTextBox1.AppendText($"停止 subs-check.exe 时出错: {ex.Message}\r\n");
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
                richTextBox1.AppendText("subs-check.exe 已退出\r\n");
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
    }
}
