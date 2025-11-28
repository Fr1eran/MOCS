using System;
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
        public VCInterface(string localIpStr, int localPort, string remoteIpStr, int remotePort)
        {
            var localIp = IPAddress.Parse(localIpStr);
            var remoteIp = IPAddress.Parse(remoteIpStr);
            _udpMsgSevice = new UdpMessageService<BaseMessage>(
                localIp,
                localPort,
                remoteIp,
                remotePort
            );
            _vcInterfaceSM = new StateMachine<VCInterfaceState, VCInterfaceTrigger>(
                VCInterfaceState.Stop
            );
            _lcuControlMsgSendTimer = new HighPrecisionTimer(
                new TimeSpan(100),
                LCUControlMsgSendTimeOut
            );
            _sequenceManager = new SequenceManager<ushort>(ushort.MaxValue);

            ConfigVCInterfaceStateMachine();
        }

        private void ConfigVCInterfaceStateMachine()
        {
            _vcInterfaceSM
                .Configure(VCInterfaceState.Stop)
                .OnEntry(InitStatus)
                .Permit(VCInterfaceTrigger.Activate, VCInterfaceState.Running)
                .OnExit(t => ConfigureTimer(true, _lcuControlMsgSendTimer));

            _vcInterfaceSM
                .Configure(VCInterfaceState.Running)
                .Permit(VCInterfaceTrigger.Deactivate, VCInterfaceState.Stop)
                .InternalTransitionAsync(
                    VCInterfaceTrigger.LCUMsgSend,
                    t => SendLCUControlMsgAsync()
                )
                .OnExit(t => ConfigureTimer(false, _lcuControlMsgSendTimer));
        }

        private void InitStatus() { }

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

        private void LCUControlMsgSendTimeOut()
        {
            _vcInterfaceSM.Fire(VCInterfaceTrigger.LCUMsgSend);
        }

        private async Task SendLCUControlMsgAsync()
        {
            _currentLCUSequenceNum = _sequenceManager.GetNextSequence(PacketCategory.C);
            LCUControlMsg msg = new() { SequenceNumber = _currentLCUSequenceNum };
            await _udpMsgSevice.SendAsync(msg);
        }

        private void OnRecvLCUStatusMsg(LCUStatusMsgA msg) { }

        private readonly StateMachine<VCInterfaceState, VCInterfaceTrigger> _vcInterfaceSM;
        private readonly HighPrecisionTimer _lcuControlMsgSendTimer;
        private readonly UdpMessageService<BaseMessage> _udpMsgSevice;

        private ushort _currentLCUSequenceNum;
        private readonly SequenceManager<ushort> _sequenceManager;
    }
}
