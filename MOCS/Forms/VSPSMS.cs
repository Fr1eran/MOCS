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
    public partial class VSPSMS : Form 
    {
        public VCUInterface _vcuInterface;
        public VSPSMS(VCUInterface vcuInterface)
        {
            _vcuInterface = vcuInterface; 
            InitializeComponent();
            this.Load += VSPSMS_Load;
        }

        private void VSPSMS_Load(object sender, EventArgs e)
        {
            textLife.DataBindings.Clear();
            textForward.DataBindings.Clear();
            textRelativePos.DataBindings.Clear();
            textSpeed.DataBindings.Clear();

            textLife.DataBindings.Add("Text", _vcuInterface.VSPSInfoCollection, nameof(VSPSInfo.Life));


            textForward.DataBindings.Add(
                "Text",
                _vcuInterface.VSPSInfoCollection,
                nameof(VSPSInfo.Forward),
                true,
                DataSourceUpdateMode.OnPropertyChanged,
                null,
                "{0:正向;反向}"
            );


            textRelativePos.DataBindings.Add(
                "Text",
                _vcuInterface.VSPSInfoCollection,
                nameof(VSPSInfo.RelativePos),
                true,
                DataSourceUpdateMode.OnPropertyChanged,
                null,
                "{0} mm"
            );


            textSpeed.DataBindings.Add(
                "Text",
                _vcuInterface.VSPSInfoCollection,
                nameof(VSPSInfo.Speed),
                true,
                DataSourceUpdateMode.OnPropertyChanged,
                null,
                "{0} cm/s"
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
