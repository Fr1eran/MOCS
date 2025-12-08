namespace MOCS.Forms
{
    partial class MOCS_UI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MOCSmenuStrip = new MenuStrip();
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripTextBox2 = new ToolStripTextBox();
            toolStripTextBox3 = new ToolStripTextBox();
            toolStripTextBox4 = new ToolStripTextBox();
            toolStripTextBox5 = new ToolStripTextBox();
            toolStripTextBox6 = new ToolStripTextBox();
            MOCSpanel = new Panel();
            MOCStabControl = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            splitContainer2 = new SplitContainer();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            MOCSdiagnose = new RichTextBox();
            tabPage2 = new TabPage();
            splitContainer3 = new SplitContainer();
            groupBox4 = new GroupBox();
            MOCSRecvMsg = new RichTextBox();
            groupBox5 = new GroupBox();
            MOCSSendMsg = new RichTextBox();
            MOCSmenuStrip.SuspendLayout();
            MOCSpanel.SuspendLayout();
            MOCStabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox3.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // MOCSmenuStrip
            // 
            MOCSmenuStrip.ImageScalingSize = new Size(32, 32);
            MOCSmenuStrip.Items.AddRange(new ToolStripItem[] { toolStripTextBox1, toolStripTextBox2, toolStripTextBox3, toolStripTextBox4, toolStripTextBox5, toolStripTextBox6 });
            MOCSmenuStrip.Location = new Point(0, 0);
            MOCSmenuStrip.Name = "MOCSmenuStrip";
            MOCSmenuStrip.Size = new Size(1894, 42);
            MOCSmenuStrip.TabIndex = 0;
            MOCSmenuStrip.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(100, 38);
            toolStripTextBox1.Text = "MOCS";
            // 
            // toolStripTextBox2
            // 
            toolStripTextBox2.Name = "toolStripTextBox2";
            toolStripTextBox2.Size = new Size(100, 38);
            toolStripTextBox2.Text = "MCU";
            // 
            // toolStripTextBox3
            // 
            toolStripTextBox3.Name = "toolStripTextBox3";
            toolStripTextBox3.Size = new Size(100, 38);
            toolStripTextBox3.Text = "LCU";
            // 
            // toolStripTextBox4
            // 
            toolStripTextBox4.Name = "toolStripTextBox4";
            toolStripTextBox4.Size = new Size(100, 38);
            toolStripTextBox4.Text = "GCU";
            // 
            // toolStripTextBox5
            // 
            toolStripTextBox5.Name = "toolStripTextBox5";
            toolStripTextBox5.Size = new Size(100, 38);
            toolStripTextBox5.Text = "VSPS";
            // 
            // toolStripTextBox6
            // 
            toolStripTextBox6.Name = "toolStripTextBox6";
            toolStripTextBox6.Size = new Size(100, 38);
            toolStripTextBox6.Text = "OBC";
            // 
            // MOCSpanel
            // 
            MOCSpanel.Controls.Add(MOCStabControl);
            MOCSpanel.Dock = DockStyle.Fill;
            MOCSpanel.Location = new Point(0, 42);
            MOCSpanel.Name = "MOCSpanel";
            MOCSpanel.Size = new Size(1894, 967);
            MOCSpanel.TabIndex = 1;
            // 
            // MOCStabControl
            // 
            MOCStabControl.Alignment = TabAlignment.Bottom;
            MOCStabControl.Controls.Add(tabPage1);
            MOCStabControl.Controls.Add(tabPage2);
            MOCStabControl.Dock = DockStyle.Fill;
            MOCStabControl.Location = new Point(0, 0);
            MOCStabControl.Multiline = true;
            MOCStabControl.Name = "MOCStabControl";
            MOCStabControl.SelectedIndex = 0;
            MOCStabControl.Size = new Size(1894, 967);
            MOCStabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(8, 8);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1878, 914);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "状态监视";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1872, 908);
            splitContainer1.SplitterDistance = 1172;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1172, 908);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "状态信息";
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
            splitContainer2.Panel2.Controls.Add(groupBox3);
            splitContainer2.Size = new Size(696, 908);
            splitContainer2.SplitterDistance = 601;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(696, 601);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作控制";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(MOCSdiagnose);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(696, 303);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "诊断信息";
            // 
            // MOCSdiagnose
            // 
            MOCSdiagnose.BackColor = SystemColors.Control;
            MOCSdiagnose.Dock = DockStyle.Fill;
            MOCSdiagnose.Location = new Point(3, 34);
            MOCSdiagnose.Name = "MOCSdiagnose";
            MOCSdiagnose.Size = new Size(690, 266);
            MOCSdiagnose.TabIndex = 0;
            MOCSdiagnose.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(splitContainer3);
            tabPage2.Location = new Point(8, 8);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1878, 914);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "报文收发";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(3, 3);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(groupBox4);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(groupBox5);
            splitContainer3.Size = new Size(1872, 908);
            splitContainer3.SplitterDistance = 928;
            splitContainer3.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(MOCSRecvMsg);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(928, 908);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "接收报文";
            // 
            // MOCSRecvMsg
            // 
            MOCSRecvMsg.BackColor = SystemColors.Control;
            MOCSRecvMsg.Dock = DockStyle.Fill;
            MOCSRecvMsg.Location = new Point(3, 34);
            MOCSRecvMsg.Name = "MOCSRecvMsg";
            MOCSRecvMsg.Size = new Size(922, 871);
            MOCSRecvMsg.TabIndex = 0;
            MOCSRecvMsg.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(MOCSSendMsg);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(940, 908);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "发送报文";
            // 
            // MOCSSendMsg
            // 
            MOCSSendMsg.BackColor = SystemColors.Control;
            MOCSSendMsg.Dock = DockStyle.Fill;
            MOCSSendMsg.Location = new Point(3, 34);
            MOCSSendMsg.Name = "MOCSSendMsg";
            MOCSSendMsg.Size = new Size(934, 871);
            MOCSSendMsg.TabIndex = 0;
            MOCSSendMsg.Text = "";
            // 
            // MOCS_UI
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1894, 1009);
            Controls.Add(MOCSpanel);
            Controls.Add(MOCSmenuStrip);
            Name = "MOCS_UI";
            Text = "MOCS";
            MOCSmenuStrip.ResumeLayout(false);
            MOCSmenuStrip.PerformLayout();
            MOCSpanel.ResumeLayout(false);
            MOCStabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MOCSmenuStrip;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripTextBox toolStripTextBox2;
        private ToolStripTextBox toolStripTextBox3;
        private ToolStripTextBox toolStripTextBox4;
        private ToolStripTextBox toolStripTextBox5;
        private ToolStripTextBox toolStripTextBox6;
        private Panel MOCSpanel;
        private TabControl MOCStabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RichTextBox MOCSdiagnose;
        private SplitContainer splitContainer3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private RichTextBox MOCSRecvMsg;
        private RichTextBox MOCSSendMsg;
    }
}