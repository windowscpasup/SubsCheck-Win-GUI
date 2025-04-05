using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace subs_check.win.gui
{
    public partial class CheckUpdates : Form
    {
        // 添加一个属性用于存储和传递文本内容
        public string UrlContent { get; set; }
        public System.Windows.Forms.ComboBox.ObjectCollection githubProxys { get; set; }
        public string githubProxy { get; set; }
        string githubProxyURL;
        public string 当前subsCheck版本号 { get; set; }
        public string 当前GUI版本号 { get; set; }
        public string 最新GUI版本号 { get; set; }

        public CheckUpdates()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            timer1.Enabled = true;

            if (githubProxys != null)
            {
                comboBox1.Items.Clear();
                foreach (var item in githubProxys)
                {
                    comboBox1.Items.Add(item);
                }
            }
            if (!string.IsNullOrEmpty(githubProxy)) comboBox1.Text = githubProxy;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label3.Text = 最新GUI版本号;
            label4.Text = 当前GUI版本号;

            label5.Text = 当前subsCheck版本号;

            if (comboBox1.Text == "自动选择")
            {
                // 创建不包含"自动选择"的代理列表
                List<string> proxyItems = new List<string>();
                for (int j = 0; j < comboBox1.Items.Count; j++)
                {
                    string proxyItem = comboBox1.Items[j].ToString();
                    if (proxyItem != "自动选择")
                        proxyItems.Add(proxyItem);
                }

                // 随机打乱列表顺序
                Random random = new Random();
                proxyItems = proxyItems.OrderBy(x => random.Next()).ToList();

                // 异步检测可用代理
                githubProxyURL = await DetectGitHubProxyAsync(proxyItems);
            }
            else
            {
                githubProxyURL = $"https://{comboBox1.Text}/";
            }

            if (最新GUI版本号 != 当前GUI版本号)
            {
                // 检查当前目录下是否存在 Upgrade.exe
                string upgradeExePath = System.IO.Path.Combine(Application.StartupPath, "Upgrade.exe");
                if (System.IO.File.Exists(upgradeExePath))
                {
                    button1.Text = "立即更新";
                    button1.Enabled = true;
                }
                else
                {
                    button1.Text = "缺少更新程序";
                    button1.Enabled = false;
                }
            }
            else
            {
                button1.Text = "已是最新版本";
                button1.Enabled = false;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");
                    client.Timeout = TimeSpan.FromSeconds(30); // 增加超时时间以适应下载需求

                    string url = "https://api.github.com/repos/beck-8/subs-check/releases/latest";

                    // 使用异步方法
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // 异步读取内容
                        string responseBody = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(responseBody);
                        string latestVersion = json["tag_name"].ToString();
                        label6.Text = latestVersion;
                        if (当前subsCheck版本号 != latestVersion)
                        {
                            button2.Text = "立即更新";
                            button2.Enabled = true;
                        }
                        else
                        {
                            button2.Text = "已是最新版本";
                            button2.Enabled = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"下载 subs-check.exe 时出错: {ex.Message}\n\n请前往 https://github.com/beck-8/subs-check/releases 自行下载！",
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 创建专用方法用于异步检测GitHub代理
        private async Task<string> DetectGitHubProxyAsync(List<string> proxyItems)
        {
            string detectedProxyURL = "";

            // 遍历随机排序后的代理列表
            foreach (string proxyItem in proxyItems)
            {
                string checkUrl = $"https://{proxyItem}/https://raw.githubusercontent.com/cmliu/SubsCheck-Win-GUI/master/packages.config";

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromSeconds(5); // 设置5秒超时
                                                                  // 添加User-Agent头，避免被拒绝访问
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");

                        // 使用异步方式
                        HttpResponseMessage response = await client.GetAsync(checkUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            // 找到可用代理
                            detectedProxyURL = $"https://{proxyItem}/";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 记录错误但继续尝试下一个
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return detectedProxyURL;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 设置对话框结果为OK，表示用户点击了"立即更新"按钮
            this.DialogResult = DialogResult.OK;

            // 关闭窗口
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //下载链接
            string downloadURL = $"{githubProxyURL}https://github.com/cmliu/SubsCheck-Win-GUI/releases/download/{最新GUI版本号}/SubsCheck_Win_GUI.zip";
            //目标文件
            string downloadEXE = "subs-check.win.gui.exe";

            try
            {
                // 获取应用程序目录
                string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                // 创建 Upgrade.ini 文件路径
                string iniFilePath = System.IO.Path.Combine(executablePath, "Upgrade.ini");

                // 准备 INI 文件内容
                string iniContent =
                    "[Upgrade]\r\n" +
                    $"DownloadURL={downloadURL}\r\n" +
                    $"TargetFile={downloadEXE}\r\n";

                // 写入文件（如果文件已存在会被覆盖）
                System.IO.File.WriteAllText(iniFilePath, iniContent);

                DialogResult result = MessageBox.Show(
                    $"发现新版本: {最新GUI版本号}\n\n" +
                    "· 点击【确定】将下载并安装更新\n" +
                    "· 更新过程中程序会自动关闭并重启\n" +
                    "· 更新完成后所有设置将保持不变\n\n" +
                    "是否立即更新到最新版本？",
                    "发现新版本",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    // 检查目标文件是否存在
                    string targetFilePath = System.IO.Path.Combine(Application.StartupPath, "Upgrade.exe");
                    if (System.IO.File.Exists(targetFilePath))
                    {
                        // 使用Process.Start异步启动应用程序
                        System.Diagnostics.Process.Start(targetFilePath);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("更新程序 Upgrade.exe 不存在！",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"写入更新信息时出错: {ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
