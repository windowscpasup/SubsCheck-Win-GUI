namespace subs_check.win.gui
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "Subs-Check";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Subs-Check：未运行";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.numericUpDown4);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 484);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数设置";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "通用订阅",
            "Clash"});
            this.comboBox4.Location = new System.Drawing.Point(8, 423);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(74, 20);
            this.comboBox4.TabIndex = 19;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(88, 421);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "复制订阅";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(9, 179);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(154, 236);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.WordWrap = false;
            this.textBox1.Click += new System.EventHandler(this.textBox1_DoubleClick);
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "本地",
            "gist",
            "r2",
            "webdav"});
            this.comboBox1.Location = new System.Drawing.Point(105, 135);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(58, 20);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(105, 106);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            10240,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown4.TabIndex = 13;
            this.numericUpDown4.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(105, 77);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown3.TabIndex = 12;
            this.numericUpDown3.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(105, 48);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown2.TabIndex = 11;
            this.numericUpDown2.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(105, 19);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "保存方法：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "测速下限(KB/s)：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "超时时间(毫秒)：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "检查间隔(分钟)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "并发线程数：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(88, 450);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "高级设置∧";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(7, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "节点池订阅链接(点击编辑)：";
            this.label8.Click += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(281, 19);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown6.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown6.TabIndex = 15;
            this.numericUpDown6.Value = new decimal(new int[] {
            8199,
            0,
            0,
            0});
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(281, 45);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown5.TabIndex = 14;
            this.numericUpDown5.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "HTTP服务端口：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "测速时间(秒)：";
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(441, 19);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown7.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown7.TabIndex = 21;
            this.numericUpDown7.Value = new decimal(new int[] {
            8299,
            0,
            0,
            0});
            this.numericUpDown7.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(345, 21);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(95, 12);
            this.label20.TabIndex = 20;
            this.label20.Text = "Sub-Store端口：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Location = new System.Drawing.Point(193, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 484);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "实时日志";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(515, 450);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "更新内核";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 17);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(594, 464);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown8);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.numericUpDown6);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.numericUpDown5);
            this.groupBox3.Controls.Add(this.comboBox5);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.comboBox3);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.numericUpDown7);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Location = new System.Drawing.Point(13, 503);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(780, 106);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "高级设置";
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Enabled = false;
            this.numericUpDown8.Location = new System.Drawing.Point(122, 45);
            this.numericUpDown8.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown8.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown8.TabIndex = 22;
            this.numericUpDown8.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(9, 48);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(108, 16);
            this.checkBox3.TabIndex = 27;
            this.checkBox3.Text = "节点保存数目：";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(695, 71);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "检查更新";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(9, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "节点地址查询";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(106, 20);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.Text = "流媒体检测";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "[内置]布丁狗的订阅转换",
            "[内置]ACL4SSR_Online_Full",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/布丁狗的订阅转" +
                "换.yaml",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/ACL4SSR" +
                "_Online_Full.yaml",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/ACL4SSR" +
                "_Online_Full_WithIcon.yaml",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/添加直连规则." +
                "yaml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/布丁狗的订阅转换.y" +
                "aml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/ACL4SSR_On" +
                "line_Full.yaml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/ACL4SSR_On" +
                "line_Full_WithIcon.yaml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/添加直连规则.yam" +
                "l"});
            this.comboBox5.Location = new System.Drawing.Point(152, 73);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(538, 20);
            this.comboBox5.TabIndex = 24;
            this.comboBox5.Text = "[内置]布丁狗的订阅转换";
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 76);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(149, 12);
            this.label19.TabIndex = 23;
            this.label19.Text = "Clash订阅 覆写配置文件：";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "自动选择",
            "1.github.010716.xyz",
            "113355.kabaka.xyz",
            "acc.meiqer.com",
            "bakht1.jsdelivr.fyi",
            "bakht2.jsdelivr.fyi",
            "bakht3.jsdelivr.fyi",
            "cc.ikakatoo.us",
            "cf-github.xiaokk.asia",
            "cfghproxy.165683.xyz",
            "choner.eu.org",
            "dh.jeblove.com",
            "dl.github.mirror.shalo.link",
            "dnsvip.uk",
            "docker.bkxhkoo.com",
            "download.ojbk.one",
            "download.serein.cc",
            "f.shenbing.nyc.mn",
            "fast.github.qqbot.icu",
            "fastgithub.starryfun.icu",
            "file.justgame.top",
            "g-p.loli.us",
            "g.jscdn.cn",
            "g.yeyuqiufeng.cn",
            "gh-boost.oneboy.app",
            "gh-proxy.at9.net",
            "gh-proxy.dorz.tech",
            "gh-proxy.iflyelf.com",
            "gh-proxy.jacksixth.top",
            "gh-proxy.jmper.me",
            "gh-proxy.just520.top",
            "gh-proxy.not.icu",
            "gh-proxy.rxliuli.com",
            "gh-proxy.yuntao.me",
            "gh.136361.xyz",
            "gh.19121912.xyz",
            "gh.193.gs",
            "gh.220106.xyz",
            "gh.244224659.xyz",
            "gh.321122.xyz",
            "gh.334433.xyz",
            "gh.654535.xyz",
            "gh.8p.gs",
            "gh.960980.xyz",
            "gh.accn.eu.org",
            "gh.aurzex.top",
            "gh.chewable.eu.org",
            "gh.chillwaytech.com",
            "gh.crond.dev",
            "gh.duang.io",
            "gh.dwsy.link",
            "gh.flyrr.cc",
            "gh.gongyi.tk",
            "gh.gorun.eu.org",
            "gh.gxb.pub",
            "gh.haloless.com",
            "gh.hitcs.cc",
            "gh.ibridge.eu.org",
            "gh.j8.work",
            "gh.jadelive.top",
            "gh.jscdn.cn",
            "gh.kemon.ai",
            "gh.lkwplus.com",
            "gh.lux1983.com",
            "gh.lyh.moe",
            "gh.micedns.cloudns.org",
            "gh.mirror.190211.xyz",
            "gh.mirror.coolfeature.top",
            "gh.moetools.net",
            "gh.momonomi.xyz",
            "gh.mrskye.cn",
            "gh.nekhill.top",
            "gh.nekorect.eu.org",
            "gh.oneproxy.top",
            "gh.pandash.top",
            "gh.qptf.eu.org",
            "gh.qsd.onl",
            "gh.qsq.one",
            "gh.rem.asia",
            "gh.riye.de",
            "gh.someo.top",
            "gh.stewitch.com",
            "gh.tbw.wiki",
            "gh.tou.lu",
            "gh.wglee.org",
            "gh.wowforever.xyz",
            "gh.wuuu.cc",
            "gh.xda.plus",
            "gh.yahool.com.cn",
            "gh.yushum.com",
            "ghac.760710.xyz",
            "ghacc.cpuhk.eu.org",
            "ghb.wglee.org",
            "ghfast.top",
            "ghjs.131412.eu.org",
            "ghp.618032.xyz",
            "ghp.fit2.fun",
            "ghp.imc.re",
            "ghp.jokeme.top",
            "ghp.opendatahub.xyz",
            "ghp.vatery.com",
            "ghproxy.0081024.xyz",
            "ghproxy.943689.xyz",
            "ghproxy.amayakite.xyz",
            "ghproxy.bugungu.top",
            "ghproxy.dsdog.tk",
            "ghproxy.hoshizukimio.top",
            "ghproxy.joylian.com",
            "ghproxy.minge.dev",
            "ghproxy.missfuture.eu.org",
            "ghproxy.moweilong.com",
            "ghproxy.nanakorobi.com",
            "ghproxy.smallfawn.work",
            "ghproxy.sveir.xyz",
            "ghproxy.temoa.fun",
            "ghproxy.thefoxnet.com",
            "ghproxy.viper.pub",
            "ghproxy.weizhiwen.net",
            "ghproxy.wjsphy.top",
            "ghproxy.xiaohei-studio-chatgpt-proxy.com.cn",
            "git.22345678.xyz",
            "git.aaltozz.info",
            "git.anye.in",
            "git.binbow.link",
            "git.blaow.cloudns.org",
            "git.imvery.moe",
            "git.lincloud.pro",
            "git.loushi.site",
            "git.lzzz.ink",
            "git.nyar.work",
            "git.outtw.com",
            "git.txaff.com",
            "git.verynb.org",
            "git.wsl.icu",
            "git.xiny.eu.org",
            "github-proxy.ai-lulu.top",
            "github-proxy.caoayu.top",
            "github-proxy.fjiabinc.cn",
            "github-proxy.sharefree.site",
            "github-quick.1ms.dev",
            "github-raw-download.nekhill.top",
            "github.08050611.xyz",
            "github.170011.xyz",
            "github.197909.xyz",
            "github.19890821.xyz",
            "github.201068.xyz",
            "github.333033.xyz",
            "github.4240333.xyz",
            "github.776884.xyz",
            "github.789056.xyz",
            "github.818668.xyz",
            "github.8void.sbs",
            "github.9394961.xyz",
            "github.abyss.moe",
            "github.atzzz.com",
            "github.axcio.dns-dynamic.net",
            "github.boki.moe",
            "github.boringhex.top",
            "github.c1g.top",
            "github.cf.xihale.top",
            "github.chasun.top",
            "github.chuancey.eu.org",
            "github.computerqwq.top",
            "github.cswklt.top",
            "github.ddlink.asia",
            "github.dockerspeed.site",
            "github.eejsq.net",
            "github.ffffffff0x.com",
            "github.gdzja.site",
            "github.haodiy.xyz",
            "github.hi.edu.rs",
            "github.hostscc.top",
            "github.hx208.top",
            "github.ilxyz.xyz",
            "github.intellisensing.tech",
            "github.jerryliang.win",
            "github.jimmyshjj.top",
            "github.jinenyy.vip",
            "github.kidos.top",
            "github.mayx.eu.org",
            "github.mirror.qlnu-sec.cn",
            "github.mirrors.hikafeng.com",
            "github.mistudio.top",
            "github.nj106.fun",
            "github.orangbus.cn",
            "github.ruxi.org",
            "github.serein.cc",
            "github.space520.eu.org",
            "github.suyijun.top",
            "github.try255.com",
            "github.verynb.org",
            "github.widiazine.top",
            "github.workers.lv10.ren",
            "github.workersnail.com",
            "github.xwb009.xyz",
            "github.xxqq.de",
            "github.ylyhtools.top",
            "github.yunfile.fun",
            "github.zhaolele.top",
            "github.zhou2008.cn",
            "github.zhulin240520.buzz",
            "github.zyhmifan.top",
            "githubacc.caiaiwan.com",
            "githubgo.856798.xyz",
            "githubproxy.unix.do",
            "gitproxy.mrhjx.cn",
            "godlike.ezpull.top",
            "google.c1g.top",
            "gxb.pub",
            "hay.uxio.cn",
            "hh.newhappy.cf",
            "hub.326998.xyz",
            "hub.jeblove.com",
            "jias.ayanjiu.top",
            "jiasu.iwtriptqt1016.eu.org",
            "jiasughapi.lingjun.cc",
            "jiasuqi.167889.xyz",
            "js.wd666.cloudns.biz",
            "l0l0l.cc",
            "m.seafood.loan",
            "mc.cn.eu.org",
            "micromatrix.gq",
            "mygh.api.xiaomao.eu.org",
            "noad.keliyan.top",
            "oleridge.luxuriaignis.com",
            "proxy.191027.xyz",
            "ql.l50.top",
            "raw.nmd.im",
            "rst.567812.xyz",
            "sitefile.xenwayne.top",
            "static.kaixinwu.vip",
            "static.yiwangmeng.com",
            "t.992699.xyz",
            "tube.20140301.xyz",
            "wfgithub.xiaonuomi.ie.eu.org"});
            this.comboBox3.Location = new System.Drawing.Point(590, 19);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(180, 20);
            this.comboBox3.TabIndex = 21;
            this.comboBox3.Text = "自动选择";
            this.comboBox3.Leave += new System.EventHandler(this.comboBox3_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(500, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "Github Proxy：";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "https://github.com/AaronFeng753/Waifu2x-Extension-GUI/releases/download/v2.21.12/" +
                "Waifu2x-Extension-GUI-v2.21.12-Portable.7z",
            "https://github.com/2dust/v2rayN/releases/download/7.10.4/v2rayN-windows-64-deskto" +
                "p.zip",
            "https://github.com/VSCodium/vscodium/releases/download/1.98.0.25067/codium-1.98.0" +
                ".25067-el9.aarch64.rpm"});
            this.comboBox2.Location = new System.Drawing.Point(414, 45);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(356, 20);
            this.comboBox2.TabIndex = 19;
            this.comboBox2.Text = "https://github.com/AaronFeng753/Waifu2x-Extension-GUI/releases/download/v2.21.12/" +
    "Waifu2x-Extension-GUI-v2.21.12-Portable.7z";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(344, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "测速地址：";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(780, 3);
            this.progressBar1.TabIndex = 3;
            // 
            // timer2
            // 
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(13, 615);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(780, 51);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Gist 上传参数";
            this.groupBox4.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(467, 18);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(306, 21);
            this.textBox4.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(359, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "API Mirror(可选)：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(253, 18);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 3;
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(168, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "Github Token：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(62, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "Gist ID：";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 500;
            this.toolTip1.ShowAlways = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox6);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.textBox7);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Location = new System.Drawing.Point(13, 672);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(780, 51);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "R2 上传参数";
            this.groupBox5.Visible = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(467, 18);
            this.textBox6.Name = "textBox6";
            this.textBox6.PasswordChar = '*';
            this.textBox6.Size = new System.Drawing.Size(306, 21);
            this.textBox6.TabIndex = 3;
            this.textBox6.Text = "1234567890";
            this.textBox6.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox6.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(382, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "Worker Token：";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(88, 18);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(285, 21);
            this.textBox7.TabIndex = 1;
            this.textBox7.Text = "https://example.worker.dev";
            this.textBox7.Leave += new System.EventHandler(this.textBox7_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "Worker URL：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox5);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.textBox8);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.textBox9);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Location = new System.Drawing.Point(13, 729);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(780, 51);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Webdav 上传参数";
            this.groupBox6.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(467, 18);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(306, 21);
            this.textBox5.TabIndex = 5;
            this.textBox5.Text = "https://example.com/dav/";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(382, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "Webdav URL：";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(257, 18);
            this.textBox8.Name = "textBox8";
            this.textBox8.PasswordChar = '*';
            this.textBox8.Size = new System.Drawing.Size(110, 21);
            this.textBox8.TabIndex = 3;
            this.textBox8.Text = "admin";
            this.textBox8.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox8.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(186, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 2;
            this.label17.Text = "Password：";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(70, 18);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(110, 21);
            this.textBox9.TabIndex = 1;
            this.textBox9.Text = "admin";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 0;
            this.label18.Text = "Username：";
            // 
            // timer3
            // 
            this.timer3.Interval = 86400000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 789);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SubsCheck Win GUI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}

