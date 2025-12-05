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
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 330;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox6);
            groupBox3.Controls.Add(groupBox5);
            groupBox3.Controls.Add(groupBox4);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(330, 450);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "通讯配置";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(SysDiagnosticRTB);
            groupBox6.Dock = DockStyle.Fill;
            groupBox6.Location = new Point(3, 377);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(324, 70);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            groupBox6.Text = "诊断信息";
            // 
            // SysDiagnosticRTB
            // 
            SysDiagnosticRTB.Dock = DockStyle.Fill;
            SysDiagnosticRTB.Location = new Point(3, 23);
            SysDiagnosticRTB.Name = "SysDiagnosticRTB";
            SysDiagnosticRTB.ReadOnly = true;
            SysDiagnosticRTB.Size = new Size(318, 44);
            SysDiagnosticRTB.TabIndex = 0;
            SysDiagnosticRTB.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(panel2);
            groupBox5.Controls.Add(tableLayoutPanel2);
            groupBox5.Dock = DockStyle.Top;
            groupBox5.Location = new Point(3, 192);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(324, 185);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "牵引";
            // 
            // panel2
            // 
            panel2.Controls.Add(StopMCUComButton);
            panel2.Controls.Add(BeginMCUComButton);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(3, 145);
            panel2.Name = "panel2";
            panel2.Size = new Size(318, 37);
            panel2.TabIndex = 3;
            // 
            // StopMCUComButton
            // 
            StopMCUComButton.Dock = DockStyle.Right;
            StopMCUComButton.Location = new Point(224, 0);
            StopMCUComButton.Name = "StopMCUComButton";
            StopMCUComButton.Size = new Size(94, 37);
            StopMCUComButton.TabIndex = 1;
            StopMCUComButton.Text = "关闭";
            StopMCUComButton.UseVisualStyleBackColor = true;
            // 
            // BeginMCUComButton
            // 
            BeginMCUComButton.Dock = DockStyle.Left;
            BeginMCUComButton.Location = new Point(0, 0);
            BeginMCUComButton.Name = "BeginMCUComButton";
            BeginMCUComButton.Size = new Size(94, 37);
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
            tableLayoutPanel2.Location = new Point(3, 23);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.661972F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.338028F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel2.Size = new Size(318, 110);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // MCURecvPortTextBox
            // 
            MCURecvPortTextBox.Anchor = AnchorStyles.Right;
            MCURecvPortTextBox.Location = new Point(162, 79);
            MCURecvPortTextBox.Name = "MCURecvPortTextBox";
            MCURecvPortTextBox.Size = new Size(153, 27);
            MCURecvPortTextBox.TabIndex = 6;
            MCURecvPortTextBox.Text = "6002";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(3, 82);
            label6.Name = "label6";
            label6.Size = new Size(88, 20);
            label6.TabIndex = 5;
            label6.Text = "接收端口号:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(3, 6);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 0;
            label3.Text = "目标IP地址:";
            // 
            // MCUSendIPTextBox
            // 
            MCUSendIPTextBox.Anchor = AnchorStyles.Right;
            MCUSendIPTextBox.Location = new Point(162, 3);
            MCUSendIPTextBox.Name = "MCUSendIPTextBox";
            MCUSendIPTextBox.Size = new Size(153, 27);
            MCUSendIPTextBox.TabIndex = 1;
            MCUSendIPTextBox.Text = "192.168.43.5";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(3, 44);
            label4.Name = "label4";
            label4.Size = new Size(88, 20);
            label4.TabIndex = 2;
            label4.Text = "目标端口号:";
            // 
            // MCUSendPortTextBox
            // 
            MCUSendPortTextBox.Anchor = AnchorStyles.Right;
            MCUSendPortTextBox.Location = new Point(162, 40);
            MCUSendPortTextBox.Name = "MCUSendPortTextBox";
            MCUSendPortTextBox.Size = new Size(153, 27);
            MCUSendPortTextBox.TabIndex = 3;
            MCUSendPortTextBox.Text = "8008";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(panel1);
            groupBox4.Controls.Add(tableLayoutPanel1);
            groupBox4.Dock = DockStyle.Top;
            groupBox4.Location = new Point(3, 23);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(324, 169);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "车载";
            // 
            // panel1
            // 
            panel1.Controls.Add(StopVehicleComButton);
            panel1.Controls.Add(BeginVehicleComButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(3, 132);
            panel1.Name = "panel1";
            panel1.Size = new Size(318, 34);
            panel1.TabIndex = 6;
            // 
            // StopVehicleComButton
            // 
            StopVehicleComButton.Dock = DockStyle.Right;
            StopVehicleComButton.Location = new Point(224, 0);
            StopVehicleComButton.Name = "StopVehicleComButton";
            StopVehicleComButton.Size = new Size(94, 34);
            StopVehicleComButton.TabIndex = 1;
            StopVehicleComButton.Text = "关闭";
            StopVehicleComButton.UseVisualStyleBackColor = true;
            StopVehicleComButton.Click += StopVehicleComButton_Click;
            // 
            // BeginVehicleComButton
            // 
            BeginVehicleComButton.Dock = DockStyle.Left;
            BeginVehicleComButton.Location = new Point(0, 0);
            BeginVehicleComButton.Name = "BeginVehicleComButton";
            BeginVehicleComButton.Size = new Size(94, 34);
            BeginVehicleComButton.TabIndex = 0;
            BeginVehicleComButton.Text = "启动";
            BeginVehicleComButton.UseVisualStyleBackColor = true;
            BeginVehicleComButton.Click += BeginVehicleComButton_Click;
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
            tableLayoutPanel1.Location = new Point(3, 23);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.4587173F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 49.5412827F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.Size = new Size(318, 99);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 6);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 0;
            label1.Text = "目标IP地址:";
            // 
            // VCSendIPTextBox
            // 
            VCSendIPTextBox.Anchor = AnchorStyles.Right;
            VCSendIPTextBox.Location = new Point(162, 3);
            VCSendIPTextBox.Name = "VCSendIPTextBox";
            VCSendIPTextBox.Size = new Size(153, 27);
            VCSendIPTextBox.TabIndex = 1;
            VCSendIPTextBox.Text = "192.168.43.10";
            VCSendIPTextBox.TextChanged += VCSendIPTextBox_TextChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(3, 39);
            label2.Name = "label2";
            label2.Size = new Size(88, 20);
            label2.TabIndex = 2;
            label2.Text = "目标端口号:";
            // 
            // VCSendPortTextBox
            // 
            VCSendPortTextBox.Anchor = AnchorStyles.Right;
            VCSendPortTextBox.Location = new Point(162, 36);
            VCSendPortTextBox.Name = "VCSendPortTextBox";
            VCSendPortTextBox.Size = new Size(153, 27);
            VCSendPortTextBox.TabIndex = 3;
            VCSendPortTextBox.Text = "8001";
            VCSendPortTextBox.TextChanged += VCSendPortTextBox_TextChanged;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(3, 72);
            label5.Name = "label5";
            label5.Size = new Size(88, 20);
            label5.TabIndex = 4;
            label5.Text = "接收端口号:";
            // 
            // VCRecvPortTextBox
            // 
            VCRecvPortTextBox.Anchor = AnchorStyles.Right;
            VCRecvPortTextBox.Location = new Point(162, 69);
            VCRecvPortTextBox.Name = "VCRecvPortTextBox";
            VCRecvPortTextBox.Size = new Size(153, 27);
            VCRecvPortTextBox.TabIndex = 5;
            VCRecvPortTextBox.Text = "6001";
            VCRecvPortTextBox.TextChanged += VCRecvPortTextBox_TextChanged;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
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
            splitContainer2.Size = new Size(466, 450);
            splitContainer2.SplitterDistance = 344;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(RecvMsgRTB);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(466, 344);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "接收报文";
            // 
            // RecvMsgRTB
            // 
            RecvMsgRTB.Dock = DockStyle.Fill;
            RecvMsgRTB.Location = new Point(3, 23);
            RecvMsgRTB.Name = "RecvMsgRTB";
            RecvMsgRTB.ReadOnly = true;
            RecvMsgRTB.Size = new Size(460, 318);
            RecvMsgRTB.TabIndex = 0;
            RecvMsgRTB.Text = "";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(SendMsgRTB);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(466, 102);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "发送报文";
            // 
            // SendMsgRTB
            // 
            SendMsgRTB.Dock = DockStyle.Fill;
            SendMsgRTB.Location = new Point(3, 23);
            SendMsgRTB.Name = "SendMsgRTB";
            SendMsgRTB.ReadOnly = true;
            SendMsgRTB.Size = new Size(460, 76);
            SendMsgRTB.TabIndex = 0;
            SendMsgRTB.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "MOCS";
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
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RichTextBox RecvMsgRTB;
        private RichTextBox SendMsgRTB;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private GroupBox groupBox3;
        private TextBox VCSendIPTextBox;
        private Label label2;
        private TextBox VCSendPortTextBox;
        private GroupBox groupBox5;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label3;
        private TextBox MCUSendIPTextBox;
        private Label label4;
        private TextBox MCUSendPortTextBox;
        private GroupBox groupBox4;
        private Panel panel1;
        private Button StopVehicleComButton;
        private Button BeginVehicleComButton;
        private Label label5;
        private TextBox VCRecvPortTextBox;
        private Panel panel2;
        private Button StopMCUComButton;
        private Button BeginMCUComButton;
        private TextBox MCURecvPortTextBox;
        private Label label6;
        private GroupBox groupBox6;
        private RichTextBox SysDiagnosticRTB;
    }
}
