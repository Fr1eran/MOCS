using MOCS.Cores.VCU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOCS.Forms
{
    public partial class VSPSMS : Form //warning！使用public还是private？
    {
        public VSPSInfo _vspsInfo = new VSPSInfo();
        public VSPSMS()
        {
            InitializeComponent();
            this.Load += VSPSMS_Load;
        }

        private void VSPSMS_Load(object sender, EventArgs e)
        {
            // 绑定“生命信号”到txtLife
            textLife.DataBindings.Add("Text", _vspsInfo, nameof(VSPSInfo.Life));

            // 绑定“运行方向”（bool转文本：true→正向，false→反向）
            textForward.DataBindings.Add(
                "Text",//控件属性
                _vspsInfo,//绑定源
                nameof(VSPSInfo.Forward),//源属性
                true,                    // formattingEnabled（是否启用格式化）
                DataSourceUpdateMode.OnPropertyChanged, // 数据源更新模式
                null,//空值时显示内容
                "{0:正向;反向}"//格式字符串
            );

            // 绑定“相对位置”（带单位mm）
            textRelativePos.DataBindings.Add(
                "Text",//控件属性
                _vspsInfo,//绑定源
                nameof(VSPSInfo.RelativePos),//源属性
                true,//是否格式化？
                DataSourceUpdateMode.OnPropertyChanged, // 数据源更新模式
                null,//空值时显示内容
                "{0} mm"//格式字符串
            );

            // 绑定“速度”（带单位cm/s）
            textSpeed.DataBindings.Add(
                "Text",//控件属性
                _vspsInfo,//绑定源
                nameof(VSPSInfo.Speed),//源属性
                true,//是否格式化？
                DataSourceUpdateMode.OnPropertyChanged, // 数据源更新模式
                null,//空值时显示内容
                "{0} cm/s"//格式字符串
            );
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void MCUSendIPTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textDirc_TextChanged(object sender, EventArgs e)
        {

        }

        private void textRelativePos_TextChanged(object sender, EventArgs e)
        {

        }

        private void textSpeed_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
