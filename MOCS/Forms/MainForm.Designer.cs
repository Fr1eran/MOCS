namespace MOCS
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            groupBox3 = new GroupBox();
            groupBox6 = new GroupBox();
            SysDiagnosticRTB = new RichTextBox();
            groupBox5 = new GroupBox();
            panel2 = new Panel();
            StopMCUComButton = new Button();
            BeginMCUComButton = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            MCURecvPortTextBox = new TextBox();
            label6 = new Label();
            label3 = new Label();
            MCUSendIPTextBox = new TextBox();
            label4 = new Label();
            MCUSendPortTextBox = new TextBox();
            groupBox4 = new GroupBox();
            panel1 = new Panel();
            StopVehicleComButton = new Button();
            BeginVehicleComButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            VCSendIPTextBox = new TextBox();
            label2 = new Label();
            VCSendPortTextBox = new TextBox();
            label5 = new Label();
            VCRecvPortTextBox = new TextBox();
            splitContainer2 = new SplitContainer();
            groupBox2 = new GroupBox();
            RecvMsgRTB = new RichTextBox();
            groupBox1 = new GroupBox();
            SendMsgRTB = new RichTextBox();
            tabPage2 = new TabPage();
            groupBox7 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label7 = new Label();
            textLife = new TextBox();
            label8 = new Label();
            textForward = new TextBox();
            label9 = new Label();
            label10 = new Label();
            textRelativePos = new TextBox();
            textSpeed = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox4.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox7.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.ImeMode = ImeMode.NoControl;
            tabControl1.Location = new Point(2, 3);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1242, 694);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(8, 45);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1226, 641);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "车载网络控制器";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Margin = new Padding(5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1220, 635);
            splitContainer1.SplitterDistance = 503;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox6);
            groupBox3.Controls.Add(groupBox5);
            groupBox3.Controls.Add(groupBox4);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Margin = new Padding(5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5);
            groupBox3.Size = new Size(503, 635);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "通讯配置";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(SysDiagnosticRTB);
            groupBox6.Dock = DockStyle.Fill;
            groupBox6.Location = new Point(5, 533);
            groupBox6.Margin = new Padding(5);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(5);
            groupBox6.Size = new Size(493, 97);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            groupBox6.Text = "诊断信息";
            // 
            // SysDiagnosticRTB
            // 
            SysDiagnosticRTB.Dock = DockStyle.Fill;
            SysDiagnosticRTB.Location = new Point(5, 36);
            SysDiagnosticRTB.Margin = new Padding(5);
            SysDiagnosticRTB.Name = "SysDiagnosticRTB";
            SysDiagnosticRTB.ReadOnly = true;
            SysDiagnosticRTB.Size = new Size(483, 56);
            SysDiagnosticRTB.TabIndex = 0;
            SysDiagnosticRTB.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(panel2);
            groupBox5.Controls.Add(tableLayoutPanel2);
            groupBox5.Dock = DockStyle.Top;
            groupBox5.Location = new Point(5, 275);
            groupBox5.Margin = new Padding(5);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(5);
            groupBox5.Size = new Size(493, 258);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "牵引";
            // 
            // panel2
            // 
            panel2.Controls.Add(StopMCUComButton);
            panel2.Controls.Add(BeginMCUComButton);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(5, 196);
            panel2.Margin = new Padding(5);
            panel2.Name = "panel2";
            panel2.Size = new Size(483, 57);
            panel2.TabIndex = 3;
            // 
            // StopMCUComButton
            // 
            StopMCUComButton.Dock = DockStyle.Right;
            StopMCUComButton.Location = new Point(337, 0);
            StopMCUComButton.Margin = new Padding(5);
            StopMCUComButton.Name = "StopMCUComButton";
            StopMCUComButton.Size = new Size(146, 57);
            StopMCUComButton.TabIndex = 1;
            StopMCUComButton.Text = "关闭";
            StopMCUComButton.UseVisualStyleBackColor = true;
            // 
            // BeginMCUComButton
            // 
            BeginMCUComButton.Dock = DockStyle.Left;
            BeginMCUComButton.Location = new Point(0, 0);
            BeginMCUComButton.Margin = new Padding(5);
            BeginMCUComButton.Name = "BeginMCUComButton";
            BeginMCUComButton.Size = new Size(146, 57);
            BeginMCUComButton.TabIndex = 0;
            BeginMCUComButton.Text = "启动";
            BeginMCUComButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(MCURecvPortTextBox, 1, 2);
            tableLayoutPanel2.Controls.Add(label6, 0, 2);
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(MCUSendIPTextBox, 1, 0);
            tableLayoutPanel2.Controls.Add(label4, 0, 1);
            tableLayoutPanel2.Controls.Add(MCUSendPortTextBox, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(5, 36);
            tableLayoutPanel2.Margin = new Padding(5);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.661972F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.338028F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLayoutPanel2.Size = new Size(483, 163);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // MCURecvPortTextBox
            // 
            MCURecvPortTextBox.Anchor = AnchorStyles.Right;
            MCURecvPortTextBox.Location = new Point(246, 117);
            MCURecvPortTextBox.Margin = new Padding(5);
            MCURecvPortTextBox.Name = "MCURecvPortTextBox";
            MCURecvPortTextBox.Size = new Size(232, 38);
            MCURecvPortTextBox.TabIndex = 6;
            MCURecvPortTextBox.Text = "6002";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(5, 120);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(140, 31);
            label6.TabIndex = 5;
            label6.Text = "接收端口号:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(5, 8);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(138, 31);
            label3.TabIndex = 0;
            label3.Text = "目标IP地址:";
            // 
            // MCUSendIPTextBox
            // 
            MCUSendIPTextBox.Anchor = AnchorStyles.Right;
            MCUSendIPTextBox.Location = new Point(246, 5);
            MCUSendIPTextBox.Margin = new Padding(5);
            MCUSendIPTextBox.Name = "MCUSendIPTextBox";
            MCUSendIPTextBox.Size = new Size(232, 38);
            MCUSendIPTextBox.TabIndex = 1;
            MCUSendIPTextBox.Text = "192.168.43.5";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(5, 63);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(140, 31);
            label4.TabIndex = 2;
            label4.Text = "目标端口号:";
            // 
            // MCUSendPortTextBox
            // 
            MCUSendPortTextBox.Anchor = AnchorStyles.Right;
            MCUSendPortTextBox.Location = new Point(246, 59);
            MCUSendPortTextBox.Margin = new Padding(5);
            MCUSendPortTextBox.Name = "MCUSendPortTextBox";
            MCUSendPortTextBox.Size = new Size(232, 38);
            MCUSendPortTextBox.TabIndex = 3;
            MCUSendPortTextBox.Text = "8008";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(panel1);
            groupBox4.Controls.Add(tableLayoutPanel1);
            groupBox4.Dock = DockStyle.Top;
            groupBox4.Location = new Point(5, 36);
            groupBox4.Margin = new Padding(5);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5);
            groupBox4.Size = new Size(493, 239);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "车载";
            // 
            // panel1
            // 
            panel1.Controls.Add(StopVehicleComButton);
            panel1.Controls.Add(BeginVehicleComButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(5, 181);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new Size(483, 53);
            panel1.TabIndex = 6;
            // 
            // StopVehicleComButton
            // 
            StopVehicleComButton.Dock = DockStyle.Right;
            StopVehicleComButton.Location = new Point(337, 0);
            StopVehicleComButton.Margin = new Padding(5);
            StopVehicleComButton.Name = "StopVehicleComButton";
            StopVehicleComButton.Size = new Size(146, 53);
            StopVehicleComButton.TabIndex = 1;
            StopVehicleComButton.Text = "关闭";
            StopVehicleComButton.UseVisualStyleBackColor = true;
            // 
            // BeginVehicleComButton
            // 
            BeginVehicleComButton.Dock = DockStyle.Left;
            BeginVehicleComButton.Location = new Point(0, 0);
            BeginVehicleComButton.Margin = new Padding(5);
            BeginVehicleComButton.Name = "BeginVehicleComButton";
            BeginVehicleComButton.Size = new Size(146, 53);
            BeginVehicleComButton.TabIndex = 0;
            BeginVehicleComButton.Text = "启动";
            BeginVehicleComButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(VCSendIPTextBox, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(VCSendPortTextBox, 1, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 2);
            tableLayoutPanel1.Controls.Add(VCRecvPortTextBox, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(5, 36);
            tableLayoutPanel1.Margin = new Padding(5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.4587173F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 49.5412827F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.Size = new Size(483, 147);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(5, 8);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(138, 31);
            label1.TabIndex = 0;
            label1.Text = "目标IP地址:";
            // 
            // VCSendIPTextBox
            // 
            VCSendIPTextBox.Anchor = AnchorStyles.Right;
            VCSendIPTextBox.Location = new Point(246, 5);
            VCSendIPTextBox.Margin = new Padding(5);
            VCSendIPTextBox.Name = "VCSendIPTextBox";
            VCSendIPTextBox.Size = new Size(232, 38);
            VCSendIPTextBox.TabIndex = 1;
            VCSendIPTextBox.Text = "192.168.43.10";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(5, 56);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(140, 31);
            label2.TabIndex = 2;
            label2.Text = "目标端口号:";
            // 
            // VCSendPortTextBox
            // 
            VCSendPortTextBox.Anchor = AnchorStyles.Right;
            VCSendPortTextBox.Location = new Point(246, 53);
            VCSendPortTextBox.Margin = new Padding(5);
            VCSendPortTextBox.Name = "VCSendPortTextBox";
            VCSendPortTextBox.Size = new Size(232, 38);
            VCSendPortTextBox.TabIndex = 3;
            VCSendPortTextBox.Text = "8001";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(5, 106);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(140, 31);
            label5.TabIndex = 4;
            label5.Text = "接收端口号:";
            // 
            // VCRecvPortTextBox
            // 
            VCRecvPortTextBox.Anchor = AnchorStyles.Right;
            VCRecvPortTextBox.Location = new Point(246, 102);
            VCRecvPortTextBox.Margin = new Padding(5);
            VCRecvPortTextBox.Name = "VCRecvPortTextBox";
            VCRecvPortTextBox.Size = new Size(232, 38);
            VCRecvPortTextBox.TabIndex = 5;
            VCRecvPortTextBox.Text = "6001";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(5);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(groupBox1);
            splitContainer2.Size = new Size(711, 635);
            splitContainer2.SplitterDistance = 484;
            splitContainer2.SplitterWidth = 6;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(RecvMsgRTB);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Margin = new Padding(5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5);
            groupBox2.Size = new Size(711, 484);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "接收报文";
            // 
            // RecvMsgRTB
            // 
            RecvMsgRTB.Dock = DockStyle.Fill;
            RecvMsgRTB.Location = new Point(5, 36);
            RecvMsgRTB.Margin = new Padding(5);
            RecvMsgRTB.Name = "RecvMsgRTB";
            RecvMsgRTB.ReadOnly = true;
            RecvMsgRTB.Size = new Size(701, 443);
            RecvMsgRTB.TabIndex = 0;
            RecvMsgRTB.Text = "";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SendMsgRTB);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5);
            groupBox1.Size = new Size(711, 145);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "发送报文";
            // 
            // SendMsgRTB
            // 
            SendMsgRTB.Dock = DockStyle.Fill;
            SendMsgRTB.Location = new Point(5, 36);
            SendMsgRTB.Margin = new Padding(5);
            SendMsgRTB.Name = "SendMsgRTB";
            SendMsgRTB.ReadOnly = true;
            SendMsgRTB.Size = new Size(701, 104);
            SendMsgRTB.TabIndex = 0;
            SendMsgRTB.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox7);
            tabPage2.Location = new Point(8, 45);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1226, 641);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "VSPSMS";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(tableLayoutPanel3);
            groupBox7.Location = new Point(3, 6);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(640, 635);
            groupBox7.TabIndex = 1;
            groupBox7.TabStop = false;
            groupBox7.Text = "状态";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(label7, 0, 0);
            tableLayoutPanel3.Controls.Add(textLife, 1, 0);
            tableLayoutPanel3.Controls.Add(label8, 0, 1);
            tableLayoutPanel3.Controls.Add(textForward, 1, 1);
            tableLayoutPanel3.Controls.Add(label9, 0, 2);
            tableLayoutPanel3.Controls.Add(label10, 0, 3);
            tableLayoutPanel3.Controls.Add(textRelativePos, 1, 2);
            tableLayoutPanel3.Controls.Add(textSpeed, 1, 3);
            tableLayoutPanel3.Location = new Point(0, 80);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Size = new Size(620, 350);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Location = new Point(100, 28);
            label7.Name = "label7";
            label7.Size = new Size(110, 31);
            label7.TabIndex = 0;
            label7.Text = "生命信号";
            // 
            // textLife
            // 
            textLife.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            textLife.Location = new Point(313, 3);
            textLife.Multiline = true;
            textLife.Name = "textLife";
            textLife.ReadOnly = true;
            textLife.Size = new Size(304, 81);
            textLife.TabIndex = 1;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Location = new Point(100, 115);
            label8.Name = "label8";
            label8.Size = new Size(110, 31);
            label8.TabIndex = 2;
            label8.Text = "运行方向";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textForward
            // 
            textForward.Anchor = AnchorStyles.None;
            textForward.Location = new Point(313, 90);
            textForward.Multiline = true;
            textForward.Name = "textForward";
            textForward.ReadOnly = true;
            textForward.Size = new Size(304, 81);
            textForward.TabIndex = 3;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.Location = new Point(54, 202);
            label9.Name = "label9";
            label9.Size = new Size(202, 31);
            label9.TabIndex = 4;
            label9.Text = "相对位置（mm）";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.None;
            label10.AutoSize = true;
            label10.Location = new Point(72, 290);
            label10.Name = "label10";
            label10.Size = new Size(165, 31);
            label10.TabIndex = 5;
            label10.Text = "速度（cm/s）";
            // 
            // textRelativePos
            // 
            textRelativePos.Anchor = AnchorStyles.None;
            textRelativePos.Location = new Point(313, 179);
            textRelativePos.Multiline = true;
            textRelativePos.Name = "textRelativePos";
            textRelativePos.ReadOnly = true;
            textRelativePos.Size = new Size(304, 76);
            textRelativePos.TabIndex = 6;
            // 
            // textSpeed
            // 
            textSpeed.Anchor = AnchorStyles.None;
            textSpeed.Location = new Point(313, 267);
            textSpeed.Multiline = true;
            textSpeed.Name = "textSpeed";
            textSpeed.ReadOnly = true;
            textSpeed.Size = new Size(304, 76);
            textSpeed.TabIndex = 7;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1244, 698);
            Controls.Add(tabControl1);
            Margin = new Padding(5);
            Name = "MainForm";
            Text = "MOCS";
            Load += MainForm_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            panel2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private SplitContainer splitContainer1;
        private GroupBox groupBox3;
        private GroupBox groupBox6;
        private RichTextBox SysDiagnosticRTB;
        private GroupBox groupBox5;
        private Panel panel2;
        private Button StopMCUComButton;
        private Button BeginMCUComButton;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox MCURecvPortTextBox;
        private Label label6;
        private Label label3;
        private TextBox MCUSendIPTextBox;
        private Label label4;
        private TextBox MCUSendPortTextBox;
        private GroupBox groupBox4;
        private Panel panel1;
        private Button StopVehicleComButton;
        private Button BeginVehicleComButton;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox VCSendIPTextBox;
        private Label label2;
        private TextBox VCSendPortTextBox;
        private Label label5;
        private TextBox VCRecvPortTextBox;
        private SplitContainer splitContainer2;
        private GroupBox groupBox2;
        private RichTextBox RecvMsgRTB;
        private GroupBox groupBox1;
        private RichTextBox SendMsgRTB;
        private TabPage tabPage2;
        private GroupBox groupBox7;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label7;
        private TextBox textLife;
        private Label label8;
        private TextBox textForward;
        private Label label9;
        private Label label10;
        private TextBox textRelativePos;
        private TextBox textSpeed;
    }
}
