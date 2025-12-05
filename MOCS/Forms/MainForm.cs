using System.Net;
using MOCS.Cores.MCU;
using MOCS.Cores.VCU;
using NLog;

namespace MOCS
{
    public partial class MainForm : Form
    {
        //public static readonly Logger DiagnosticLogger = LogManager.GetLogger("DiagnosticLogger");
        //public static readonly Logger SendLogger = LogManager.GetLogger("SendLogger");
        //public static readonly Logger ReceiveLogger = LogManager.GetLogger("ReceiveLogger");

        private Logger? DiagnosticLogger;
        private Logger? SendLogger;
        private Logger? ReceiveLogger;
        private VCUInterface? VCIF;

        //private readonly MCUInterface? MCUIF;

        public MainForm()
        {
            InitializeComponent();
            Load += (s, e) =>
            {
                DiagnosticLogger = LogManager.GetLogger("DiagnosticLogger");
                SendLogger = LogManager.GetLogger("SendLogger");
                ReceiveLogger = LogManager.GetLogger("ReceiveLogger");

                VCIF = new VCUInterface(DiagnosticLogger, ReceiveLogger, SendLogger);

                if (VCIF != null)
                {
                    textLife.DataBindings.Clear();
                    textForward.DataBindings.Clear();
                    textRelativePos.DataBindings.Clear();
                    textSpeed.DataBindings.Clear();

                    textLife.DataBindings.Add("Text", VCIF.VSPSInfoCollection, nameof(VSPSInfo.Life));

                    textForward.DataBindings.Add(
                        "Text",
                        VCIF.VSPSInfoCollection,
                        nameof(VSPSInfo.Forward),
                        true,
                        DataSourceUpdateMode.OnPropertyChanged,
                        null,
                        "正向;反向"
                    );

                    textRelativePos.DataBindings.Add(
                        "Text",
                        VCIF.VSPSInfoCollection,
                        nameof(VSPSInfo.RelativePos),
                        true,
                        DataSourceUpdateMode.OnPropertyChanged,
                        null,
                        "{0} mm"
                    );

                    textSpeed.DataBindings.Add(
                        "Text",
                        VCIF.VSPSInfoCollection,
                        nameof(VSPSInfo.Speed),
                        true,
                        DataSourceUpdateMode.OnPropertyChanged,
                        null,
                        "{0} cm/s"
                    );
                };
            };
        }

        private void VCSendIPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(VCSendIPTextBox.Text))
            {
                if (!IPAddress.TryParse(VCSendIPTextBox.Text, out var remoteIP))
                {
                    MessageBox.Show(
                        "请检查车载网络控制器目标IP地址输入是否正确",
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
                if (VCIF != null)
                {
                    VCIF.RemoteIpAddress = remoteIP;
                }
            }
        }

        private void VCSendPortTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(VCSendPortTextBox.Text))
            {
                if (
                    !int.TryParse(VCSendPortTextBox.Text, out var remotePort)
                    || remotePort < 1
                    || remotePort > 65535
                )
                {
                    MessageBox.Show(
                        "请检查车载网络控制器目标端口输入是否正确",
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
                if (VCIF != null)
                {
                    VCIF.RemotePort = remotePort;
                }
            }
        }

        private void VCRecvPortTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(VCRecvPortTextBox.Text))
            {
                if (
                    !int.TryParse(VCRecvPortTextBox.Text, out var localPort)
                    || localPort < 1
                    || localPort > 65535
                )
                {
                    MessageBox.Show(
                        "请检查车载网络控制器接收端口输入是否正确",
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
                if (VCIF != null)
                {
                    VCIF.LocalPort = localPort;
                }
            }
        }

        private async void BeginVehicleComButton_Click(object sender, EventArgs e)
        {
            if (VCIF != null)
            {
                await VCIF.StartAsync();
            }
        }

        private async void StopVehicleComButton_Click(object sender, EventArgs e)
        {
            if (VCIF != null)
            {
                await VCIF.StopAsync();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
