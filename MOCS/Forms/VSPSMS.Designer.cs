namespace MOCS.Forms
{
    partial class VSPSMS
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
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            textLife = new TextBox();
            label2 = new Label();
            textForward = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textRelativePos = new TextBox();
            textSpeed = new TextBox();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(640, 800);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "状态";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(textLife, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(textForward, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(textRelativePos, 1, 2);
            tableLayoutPanel1.Controls.Add(textSpeed, 1, 3);
            tableLayoutPanel1.Location = new Point(0, 80);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(620, 350);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(100, 28);
            label1.Name = "label1";
            label1.Size = new Size(110, 31);
            label1.TabIndex = 0;
            label1.Text = "生命信号";
            // 
            // textLife
            // 
            textLife.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            textLife.Location = new Point(313, 3);
            textLife.Multiline = true;
            textLife.Name = "textLife";
            textLife.Size = new Size(304, 81);
            textLife.TabIndex = 1;
            textLife.TextChanged += textBox1_TextChanged_1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(100, 115);
            label2.Name = "label2";
            label2.Size = new Size(110, 31);
            label2.TabIndex = 2;
            label2.Text = "运行方向";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textForward
            // 
            textForward.Anchor = AnchorStyles.None;
            textForward.Location = new Point(313, 90);
            textForward.Multiline = true;
            textForward.Name = "textForward";
            textForward.Size = new Size(304, 81);
            textForward.TabIndex = 3;
            textForward.TextChanged += textDirc_TextChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(54, 202);
            label3.Name = "label3";
            label3.Size = new Size(202, 31);
            label3.TabIndex = 4;
            label3.Text = "相对位置（mm）";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(72, 290);
            label4.Name = "label4";
            label4.Size = new Size(165, 31);
            label4.TabIndex = 5;
            label4.Text = "速度（cm/s）";
            // 
            // textRelativePos
            // 
            textRelativePos.Anchor = AnchorStyles.None;
            textRelativePos.Location = new Point(313, 179);
            textRelativePos.Multiline = true;
            textRelativePos.Name = "textRelativePos";
            textRelativePos.Size = new Size(304, 76);
            textRelativePos.TabIndex = 6;
            textRelativePos.TextChanged += textRelativePos_TextChanged;
            // 
            // textSpeed
            // 
            textSpeed.Anchor = AnchorStyles.None;
            textSpeed.Location = new Point(313, 267);
            textSpeed.Multiline = true;
            textSpeed.Name = "textSpeed";
            textSpeed.Size = new Size(304, 76);
            textSpeed.TabIndex = 7;
            textSpeed.TextChanged += textSpeed_TextChanged;
            // 
            // VSPSMS
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 824);
            Controls.Add(groupBox1);
            Name = "VSPSMS";
            Text = "VSPSMS";
            Load += VSPSMS_Load;
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox textLife;
        private Label label2;
        private TextBox textForward;
        private Label label3;
        private Label label4;
        private TextBox textRelativePos;
        private TextBox textSpeed;
    }
}