using System;
using System.Buffers.Binary;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;
using MOCS.Protocals;
using MOCS.Protocals.VehicleControl.MOCSToVehicle;
using MOCS.Protocals.VehicleControl.VehicleToMOCS;
using MOCS.Utils;
using Stateless;

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
        public VCInterface()
        {
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
            _udpMsgSevice?.RegisterRangeParser((byte)0xE1, (byte)0xEF, VSPSStatusMsg.Parse);
            _udpMsgSevice?.RegisterRangeParser((byte)0xF1, (byte)0xFF, OBCStatusMsg.Parse);
        }

        private void ConfigMsgHandlers()
        {
            _udpMsgSevice?.Subscribe<EMSStatusMsgA>(OnRecvEMSStatusMsgA);
            _udpMsgSevice?.Subscribe<EMSStatusMsgB>(OnRecvEMSStatusMsgB);
            _udpMsgSevice?.Subscribe<VSPSStatusMsg>(OnRecvVSPSStatusMsg);
        }

        private void Init()
        {
            // 初始化LCU状态集合
            for (int i = 0; i < _LCUNums; i++)
            {
                LCUStatusCollection[i] = new EMSStatus();
            }

            // 初始化GCU状态集合
            for (int i = 0; i < _GCUNums; i++)
            {
                GCUStatusCollection[i] = new EMSStatus();
            }

            // 初始化VSPS信息集合
            for (int i = 0; i < _VSPSNums; i++)
            {
                VSPSInfoCollection[i] = new VSPSInfo();
            }
        }

        private void BeginCommunicate()
        {
            _udpMsgSevice = new UdpMessageService<BaseMessage>(
                LocalIpAddress,
                LocalPort,
                RemoteIpAddress,
                RemotePort
            );
            ConfigMsgParsers();
            ConfigMsgHandlers();
            ConfigureTimer(true, _EMSControlMsgSendTimer);
            _udpMsgSevice.StartListening();
        }

        private async Task StopCommunicate()
        {
            ConfigureTimer(false, _EMSControlMsgSendTimer);
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.DisposeAsync();
            }
        }

        private void ConfigureTimer(bool active, HighPrecisionTimer timer)
        {
            if (timer != null)
            {
                if (active)
                {
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                }
            }
        }

        private async void EMSControlMsgSendTimeOut()
        {
            await _vcInterfaceSM.FireAsync(VCInterfaceTrigger.LCUMsgSend);
        }

        private async Task SendEMSControlMsgAsync()
        {
            _currentLCUSequenceNum = _sequenceManager.GetNextSequence(PacketCategory.C);
            EMSControlMsg msg = new() { SequenceNumber = _currentLCUSequenceNum };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        #region 消息处理器

        private void OnRecvEMSStatusMsgA(EMSStatusMsgA msg)
        {
            //if (msg.SequenceNumber != _currentLCUSequenceNum)
            //{
            //    // 序列号不一致
            //    return;
            //}

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
            //if (msg.SequenceNumber != _currentLCUSequenceNum)
            //{
            //    // 序列号不一致
            //    return;
            //}

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
            }

            var data = msg.UserData.Span;
            ref VSPSInfo VSPS = ref VSPSInfoCollection[VSPSId];
            VSPS.Life = data[0];
            VSPS.Forward = data[1] == (byte)0x01;
            VSPS.RelativePos = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(2, 2));
            VSPS.Speed = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(4, 2));
        }

        #endregion

        public IPAddress LocalIpAddress { get; set; } = IPAddress.Parse("127.0.0.1");
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
        public EMSStatus[] LCUStatusCollection = new EMSStatus[_LCUNums];
        public EMSStatus[] GCUStatusCollection = new EMSStatus[_GCUNums];

        private const byte _VSPSNums = 2;
        public VSPSInfo[] VSPSInfoCollection = new VSPSInfo[_VSPSNums];
    }
}
