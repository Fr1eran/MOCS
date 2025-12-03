using System;
using System.Buffers.Binary;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;
using MOCS.Protocals;
using MOCS.Protocals.VehicleControl.MOCSToVehicle;
using MOCS.Protocals.VehicleControl.VehicleToMOCS;
using MOCS.Utils;
using NLog;
using Stateless;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MOCS.Cores.VC
{
    /// <summary>
    /// 车载控制器接口类
    /// </summary>
    public class VCInterface
    {
        #region 面向界面层的公共接口

        public async Task StartAsync()
        {
            if (_vcInterfaceSM.CanFire(VCInterfaceTrigger.Activate))
            {
                await _vcInterfaceSM.FireAsync(VCInterfaceTrigger.Activate);
            }
        }

        public async Task StopAsync()
        {
            if (_vcInterfaceSM.CanFire(VCInterfaceTrigger.Deactivate))
            {
                await _vcInterfaceSM.FireAsync(VCInterfaceTrigger.Deactivate);
            }
        }

        #endregion

        public VCInterface(ILogger syslogger, ILogger recvlogger, ILogger sendlogger)
        {
            SysLogger = syslogger;
            RecvLogger = recvlogger;
            SendLogger = sendlogger;
            _vcInterfaceSM = new StateMachine<VCInterfaceState, VCInterfaceTrigger>(
                VCInterfaceState.Stop
            );
            _EMSControlMsgSendTimer = new HighPrecisionTimer(
                new TimeSpan(0, 0, 0, 0, 100),
                EMSControlMsgSendTimeOut
            );
            _sequenceManager = new SequenceManager<ushort>(ushort.MaxValue);

            ConfigVCInterfaceStateMachine();
        }

        private void Init()
        {
            // 初始化LCU状态集合
            for (int i = 0; i < _LCUNums - 1; i++)
            {
                LCUStatusCollection[i] = default;
            }

            // 初始化GCU状态集合
            for (int i = 0; i < _GCUNums - 1; i++)
            {
                GCUStatusCollection[i] = default;
            }

            // 初始化VSPS信息集合
            for (int i = 0; i < _VSPSNums - 1; i++)
            {
                VSPSInfoCollection[i] = default;
            }
        }

        #region 私有配置方法

        private void ConfigVCInterfaceStateMachine()
        {
            _vcInterfaceSM
                .Configure(VCInterfaceState.Stop)
                .OnEntry(Init)
                .Permit(VCInterfaceTrigger.Activate, VCInterfaceState.Running)
                .OnExit(BeginCommunicate);

            _vcInterfaceSM
                .Configure(VCInterfaceState.Running)
                .Permit(VCInterfaceTrigger.Deactivate, VCInterfaceState.Stop)
                .InternalTransitionAsync(
                    VCInterfaceTrigger.LCUMsgSend,
                    t => SendEMSControlMsgAsync()
                )
                .OnExitAsync(StopCommunicate);
        }

        private void ConfigMsgParsers()
        {
            _udpMsgSevice?.RegisterParser((byte)0x81, EMSStatusMsgA.Parse);
            _udpMsgSevice?.RegisterParser((byte)0x82, EMSStatusMsgB.Parse);
            _udpMsgSevice?.RegisterParser((byte)0xE1, VSPSStatusMsg.Parse);
            _udpMsgSevice?.RegisterRangeParser((byte)0xF1, (byte)0xFF, OBCStatusMsg.Parse);
        }

        private void ConfigMsgHandlers()
        {
            _udpMsgSevice?.Subscribe<EMSStatusMsgA>(OnRecvEMSStatusMsgA);
            _udpMsgSevice?.Subscribe<EMSStatusMsgB>(OnRecvEMSStatusMsgB);
            _udpMsgSevice?.Subscribe<VSPSStatusMsg>(OnRecvVSPSStatusMsg);
        }

        private void ConfigureTimer(bool active, HighPrecisionTimer timer)
        {
            if (timer != null)
            {
                if (active)
                {
                    SysLogger.Info("启动EMS控制器生命周期报文发送定时器");
                    timer.Start();
                }
                else
                {
                    SysLogger.Info("关闭EMS控制器生命周期报文发送定时器");
                    timer.Stop();
                }
            }
        }

        #endregion

        private void BeginCommunicate()
        {
            byte[] data =
            [
                0x00,
                0x94,
                0x00,
                0x0A,
                0x01,
                0x0D,
                0x01,
                0x81,
                0x2E,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
            ];
            ushort crc = CRC16CCITT.Compute(data);
            SysLogger.Debug(
                $"0x00 94 00 0A 01 0D 01 81 2E 00 00 00 00 00 00 00 00 00 CRC计算结果: {crc:X2}"
            );
            _udpMsgSevice = new UdpMessageService<BaseMessage>(
                LocalIpAddress,
                LocalPort,
                RemoteIpAddress,
                RemotePort,
                RecvLogger,
                SendLogger
            );
            ConfigMsgParsers();
            ConfigMsgHandlers();
            ConfigureTimer(true, _EMSControlMsgSendTimer);
            _udpMsgSevice.StartListening();
            SysLogger.Info($"开始监听端口: {LocalPort}");
        }

        private async Task StopCommunicate()
        {
            ConfigureTimer(false, _EMSControlMsgSendTimer);
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.DisposeAsync();
            }
            SysLogger.Info($"停止监听端口: {LocalPort}");
        }

        private async void EMSControlMsgSendTimeOut()
        {
            await _vcInterfaceSM.FireAsync(VCInterfaceTrigger.LCUMsgSend);
        }

        private async Task SendEMSControlMsgAsync()
        {
            _currentLCUSequenceNum = _sequenceManager.GetNextSequence(PacketCategory.C);
            EMSControlMsg msg = new() { SequenceNumber = _currentLCUSequenceNum, MsgId = 0x21 };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        #region 消息处理器

        private void OnRecvEMSStatusMsgA(EMSStatusMsgA msg)
        {
            if (msg.Destination != 0x01)
            {
                // 目标MOCS编号与当前MOCS编号不一致
                return;
            }

            var EMSId = msg.PartId;
            if (EMSId < 0x01 | EMSId > _LCUNums)
            {
                // 悬浮控制器标识号超出范围
                return;
            }

            var CANData = msg.UserData.Span;
            ref EMSStatus EMS = ref LCUStatusCollection[EMSId];
            EMS.GapSensorsStatus = (GapSensorsStatusEnum)(CANData[0] & (byte)0xE0);
            EMS.EMSCmd = (EMSCmdStatusEnum)(CANData[0] & (byte)0x10);
            EMS.Life = (byte)(CANData[0] & (byte)0x0F);
            EMS.EMSSysStatus = (EMSSysStatusEnum)(CANData[1] & (byte)0x80);
            EMS.OverloadStatus = (OverloadStatusEnum)(CANData[1] & (byte)0x40);
            EMS.AccSensorsStatus = (AccSensorsStatusEnum)(CANData[1] & (byte)0x30);
            EMS.EMSOperationStatus = (EMSOperationStatusEnum)(CANData[1] & (byte)0x08);
            EMS.EMSWarning = (EMSWarningEnum)(CANData[1] & (byte)0x04);
            EMS.EMSFaultStatus = (EMSFaultStatusEnum)(CANData[1] & (byte)0x03);
            EMS.KM2Status = (KMStatusEnum)(CANData[2] & (byte)0x80);
            EMS.Temp = CANData[2] & (byte)0x7F - 27;
            EMS.KM1ContactStatus = (KMContactStatusEnum)(CANData[3] & (byte)0x80);
            // y = (20 / 127) * x
            EMS.Gap = (float)((CANData[3] & (byte)0x7F) * 0.157480);
            EMS.KM2ContactStatus = (KMContactStatusEnum)(CANData[4] & (byte)0x80);
            // y = (530 / 127) * x
            EMS.U = (float)((CANData[4] & (byte)0x7F) * 4.173228);
            EMS.CPUStatus = (CPUStatusEnum)(CANData[5] & (byte)(0x80));
            // y = (160 / 127) * x
            EMS.I1 = (float)((CANData[5] & (byte)0x7F) * 1.259842);
            EMS.KM1Status = (KMStatusEnum)(CANData[6] & (byte)0x80);
            // y = (100 / 127) * x - 50
            EMS.Acc = (float)((CANData[6] & (byte)0x7F) * 0.7874016 - 50.0);
            EMS.BrakeStatus = (BrakeStatusEnum)(CANData[7] & (byte)0x20);
            EMS.SysSwitchStatus = (SysSwitchStatusEnum)(CANData[7] & (byte)0x10);
            EMS.GapWarnningStatus = (GapWarnningStatusEnum)(CANData[7] & (byte)0x08);
            EMS.OverloadWarningStatus = (OverloadWarningStatusEnum)(CANData[7] & (byte)0x04);
            EMS.StabilityStatus = (StabilityStatusEnum)(CANData[7] & (byte)0x02);
            EMS.CutStatus = (CutStatusEnum)(CANData[7] & (byte)0x01);
        }

        private void OnRecvEMSStatusMsgB(EMSStatusMsgB msg)
        {
            if (msg.Destination != 0x01)
            {
                // 目标MOCS编号与当前MOCS编号不一致
                return;
            }

            var EMSId = msg.PartId;
            if (EMSId < 0x01 | EMSId > _LCUNums)
            {
                // 悬浮控制器标识号超出范围
                return;
            }

            var CANData = msg.UserData.Span;
            ref EMSStatus EMS = ref LCUStatusCollection[EMSId];
            EMS.GapSensor1Status = (GapSensorStatusEnum)(CANData[1] & (byte)0x80);
            EMS.Gap1 = (float)((CANData[1] & (byte)0x7F) * 0.157480);
            EMS.GapSensor2Status = (GapSensorStatusEnum)(CANData[2] & (byte)0x80);
            EMS.Gap2 = (float)((CANData[2] & (byte)0x7F) * 0.157480);
            EMS.GapSensor3Status = (GapSensorStatusEnum)(CANData[3] & (byte)0x80);
            EMS.Gap3 = (float)((CANData[3] & (byte)0x7F) * 0.157480);
            EMS.GapSensor4Status = (GapSensorStatusEnum)(CANData[4] & (byte)0x80);
            EMS.Gap4 = (float)((CANData[4] & (byte)0x7F) * 0.157480);
            EMS.AccSensor1Status = (AccSensorStatusEnum)(CANData[5] & (byte)0x80);
            EMS.Acc1 = (float)((CANData[5] & (byte)0x7F) * 0.7874016 - 50.0);
            EMS.AccSensor2Status = (AccSensorStatusEnum)(CANData[6] & (byte)0x80);
            EMS.Acc2 = (float)((CANData[6] & (byte)0x7F) * 0.7874016 - 50.0);
            EMS.I2 = (float)((CANData[7] & (byte)0x7F) * 1.259842);
        }

        private void OnRecvVSPSStatusMsg(VSPSStatusMsg msg)
        {
            var VSPSId = msg.PartId;
            if (VSPSId < 0x01 | VSPSId > _VSPSNums)
            {
                // 涡流传感定位测速系统标识号超出范围
                return;
            }

            var data = msg.UserData.Span;
            // VSPSId 取值范围为 1~_VSPSNums，InlineArray 索引应为 0~(_VSPSNums-1)
            ref VSPSInfo VSPS = ref VSPSInfoCollection[VSPSId - 1];
            VSPS.Life = data[0];
            VSPS.Forward = data[1] == (byte)0x01;
            VSPS.RelativePos = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(2, 2));
            VSPS.Speed = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(4, 2));
        }

        #endregion

        public IPAddress LocalIpAddress { get; set; } = IPAddress.Parse("192.168.43.1");
        public int LocalPort { get; set; } = 6001;
        public IPAddress RemoteIpAddress { get; set; } = IPAddress.Parse("192.168.43.10");
        public int RemotePort { get; set; } = 8001;

        private readonly StateMachine<VCInterfaceState, VCInterfaceTrigger> _vcInterfaceSM;
        private readonly HighPrecisionTimer _EMSControlMsgSendTimer;
        private UdpMessageService<BaseMessage>? _udpMsgSevice;

        private ushort _currentLCUSequenceNum = 0;
        private readonly SequenceManager<ushort> _sequenceManager;

        private const byte _LCUNums = 6;
        private const byte _GCUNums = 6;
        public LCUStatusArray LCUStatusCollection;
        public GCUStatusArray GCUStatusCollection;

        private const byte _VSPSNums = 2;
        public VSPSInfoArray VSPSInfoCollection;

        // 日志记录器
        private readonly ILogger SysLogger;
        private readonly ILogger RecvLogger;
        private readonly ILogger SendLogger;
    }

    // 声明内联数组，强制分配在栈上

    /// <summary>
    /// 悬浮控制器状态数组
    /// </summary>
    [InlineArray(6)]
    public struct LCUStatusArray
    {
        private EMSStatus _element0;
    }

    /// <summary>
    /// 导向控制器状态数组
    /// </summary>
    [InlineArray(6)]
    public struct GCUStatusArray
    {
        private EMSStatus _element0;
    }

    /// <summary>
    /// VSPS信息数组
    /// </summary>
    [InlineArray(2)]
    public struct VSPSInfoArray
    {
        private VSPSInfo _element0;
    }
}
