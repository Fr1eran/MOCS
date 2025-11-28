using System;
using MOCS.Utils;
using Stateless;

namespace MOCS.Cores.MCU
{
    public class MCUInterface
    {
        public bool IsLifeCycleRecTimeOutBeyondLimits { get; set; } = false;

        #region 面向界面层的公共接口

        public void RunPBSCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PBS);
        }

        public void RunPSIMCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PSIM);
        }

        public void RunPTPCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PTP);
        }

        public void RunPWSPCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PWSP);
        }

        public void RunPTOCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PTO);
        }

        public void RunPPTCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PPT);
        }

        public void LoginMaglevFrame() { }

        public void SignOutMaglevFrame() { }

        public void TransmitTrackData() { }

        public void DeleteTrackData() { }

        public void TransmitMaximumCurve() { }

        public void DeleteMaximumCurve() { }

        #endregion 面向界面层的公共接口

        public MCUInterface()
        {
            _mcuInterfaceSM = new StateMachine<MCUInterfaceState, MCUInterfaceTrigger>(
                MCUInterfaceState.Stop
            );
            _mcuStateChangeMonitorSM = new StateMachine<
                MCUStateChangeMonitorStates,
                MCUStateChangeMonitorTriggers
            >(MCUStateChangeMonitorStates.Stop);
            _changeMCUStateTrigger = _mcuInterfaceSM.SetTriggerParameters<MCUStateChangeCommand>(
                MCUInterfaceTrigger.ChangeMCUState
            );
            _mcuLifeCycleSendTimer = new HighPrecisionTimer(
                new TimeSpan(3000),
                MCULifeCycleSendTimeOut
            );
            _mcuLifeCycleRecTimer = new HighPrecisionTimer(
                new TimeSpan(5000),
                MCULifeCycleRevTimeOut
            );
            _mcuDesState = MCUInterfaceState.Initial;
            ConfigMCUInterfaceStateMachine();
            ConfigPCSMonitorStateMachine();
        }

        private void ConfigMCUInterfaceStateMachine()
        {
            _changeMCUStateTrigger = _mcuInterfaceSM.SetTriggerParameters<MCUStateChangeCommand>(
                MCUInterfaceTrigger.ChangeMCUState
            );

            // -----------停止状态-------------
            // 进入时，初始化和接口有关的所有状态
            // 启动事件触发时，转移至“未连接状态”
            // 离开时，启动生命周期报文发送定时器
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Stop)
                .OnEntry(InitStatus)
                .Permit(MCUInterfaceTrigger.Activate, MCUInterfaceState.UnConnected)
                .OnExit(t => ConfigureTimer(true, _mcuLifeCycleSendTimer));

            // -----------未连接状态-------------
            // 进入时，发送MOCS状态报文
            // 生命周期报文发送事件触发时，发送MOCS状态报文
            // 生命周期报文接收事件触发时，转移至“连接状态”
            // 关闭事件触发时，转移至“关闭状态”
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.UnConnected)
                .OnEntry(SendLifeCycleMessage)
                .InternalTransition(MCUInterfaceTrigger.MOCSLifeCycleMsgSend, SendLifeCycleMessage)
                .Permit(MCUInterfaceTrigger.MCULifeCycleMsgRecvd, MCUInterfaceState.Connected)
                .Permit(MCUInterfaceTrigger.Deactivate, MCUInterfaceState.Stop);

            // -----------连接状态-------------
            // 进入时，启动生命周期报文接收超时定时器
            // 自动转移至子状态“未知状态”
            // 生命周期报文发送事件触发时，发送MOCS状态报文
            // 生命周期报文接收超时事件触发时，判断是否超出上限
            // 如果为真，则转移至“未连接状态”
            // 关闭事件触发时，转移至“停止状态”
            // 离开时，关闭生命周期报文接收超时定时器
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Connected)
                .OnEntry(t => ConfigureTimer(true, _mcuLifeCycleRecTimer))
                .InitialTransition(MCUInterfaceState.UnKnown)
                .InternalTransition(MCUInterfaceTrigger.MOCSLifeCycleMsgSend, SendLifeCycleMessage)
                .PermitIf(
                    MCUInterfaceTrigger.MCULifeCycleMsgRecvTimeOut,
                    MCUInterfaceState.UnConnected,
                    () => IsLifeCycleRecTimeOutBeyondLimits
                )
                .Permit(MCUInterfaceTrigger.Deactivate, MCUInterfaceState.Stop)
                .OnExit(t => ConfigureTimer(false, _mcuLifeCycleRecTimer));

            // -----------连接状态-------------
            // 配置为“连接状态”的子状态
            // 进入时，启动牵引系统状态转换监视
            // 牵引系统状态转换完成事件触发时，转移至“初始状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.UnKnown)
                .SubstateOf(MCUInterfaceState.Connected)
                .OnEntry(ActivateMCUStateChangeMonitior)
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Initial)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------初始状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，响应相应的转换命令
            // 牵引系统状态转换完成事件触发时，转移至“基本状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Initial)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    _changeMCUStateTrigger,
                    (command, t) => ChangeMCUInitialState(command)
                )
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Basic)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------基本状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，响应相应的转换命令
            // 牵引系统状态转换完成事件触发时，转移至与转换命令对应的状态
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Basic)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    _changeMCUStateTrigger,
                    (command, t) => ChangeMCUBasicState(command)
                )
                .PermitDynamic(MCUInterfaceTrigger.MCUStateHasChanged, () => _mcuDesState)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------带电等待状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，响应相应的转换命令
            // 牵引系统状态转换完成事件触发时，转移至与转换命令对应的状态
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.StandbyWithCharged)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    _changeMCUStateTrigger,
                    (command, t) => ChangeMCUStandbyWithChargedState(command)
                )
                .PermitDynamic(MCUInterfaceTrigger.MCUStateHasChanged, () => _mcuDesState)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------准备测试状态-------------
            // 配置为“连接状态”的子状态
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.PreparedToTest)
                .SubstateOf(MCUInterfaceState.Connected);

            // -----------带电牵引测试状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，开始状态转移
            // 牵引系统状态转换完成事件触发时，转移至“基本状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.PropulsionTestWithCharged)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    MCUInterfaceTrigger.ChangeMCUState,
                    ChangeMCUPropulsionTestWithChargedState
                )
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Basic)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------不带电牵引测试状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，开始状态转移
            // 牵引系统状态转换完成事件触发时，转移至“基本状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.PropulsionTestWithNoCharged)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    MCUInterfaceTrigger.ChangeMCUState,
                    ChangeMCUPropulsionTestWithNoChargedState
                )
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Basic)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------模拟运行状态-------------
            // 配置为“连接状态”的子状态
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.SimulatedRunning)
                .SubstateOf(MCUInterfaceState.Connected);
            //.InternalTransition(MCUInterfaceTrigger.ChangeMCUState, changeMCUSimulatedRunningState)
            //.OnExit(DeactivateMCUStateChangeMonitor);

            // -----------悬浮架运行状态-------------
            // 配置为“连接状态”的子状态
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.MaglevFrameRunning)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(MCUInterfaceTrigger.MaglevFrameLoginIn, SendMaglevFrameLoginMsg)
                .InternalTransition(
                    MCUInterfaceTrigger.MaglevFrameSignOut,
                    SendMaglevFrameSignOutMsg
                )
                .InternalTransition(MCUInterfaceTrigger.TransmitTrackData, SendTransmitTrackDataMsg)
                .InternalTransition(MCUInterfaceTrigger.DeleteTrackData, SendDeleteTrackDataMsg)
                .InternalTransition(
                    MCUInterfaceTrigger.TransmitMaximumCurve,
                    SendTransmitTrackDataMsg
                )
                .InternalTransition(
                    MCUInterfaceTrigger.DeleteMaximumCurve,
                    SendDeleteTranmitMaximumCurveMsg
                );

            // 注册状态转移期间调用的回调函数
            _mcuInterfaceSM.OnTransitioned(OnMCUInterfaceSMTransition);
        }

        private void ConfigPCSMonitorStateMachine()
        {
            _mcuStateChangeMonitorSM
                .Configure(MCUStateChangeMonitorStates.Stop)
                .Permit(
                    MCUStateChangeMonitorTriggers.Activate,
                    MCUStateChangeMonitorStates.Unchanged
                );

            _mcuStateChangeMonitorSM
                .Configure(MCUStateChangeMonitorStates.Unchanged)
                .OnEntry(t => SendChangeMCUStateMsg(_mcuDesState))
                .Permit(
                    MCUStateChangeMonitorTriggers.Executed,
                    MCUStateChangeMonitorStates.Changed
                );

            _mcuStateChangeMonitorSM
                .Configure(MCUStateChangeMonitorStates.Changed)
                .OnEntry(OnMCUStateHasChanged);
        }

        private void InitStatus()
        {
            ConfigureTimer(false, _mcuLifeCycleSendTimer);
        }

        private void SendLifeCycleMessage() { }

        private void SendChangeMCUStateMsg(MCUInterfaceState destinationState) { }

        //private void setDeleteMCUStateChangeAlreadyFlag(bool flag)
        //{
        //}

        private void OnMCUStateHasChanged()
        {
            //setDeleteMCUStateChangeAlreadyFlag(false);
            if (_mcuInterfaceSM.CanFire(MCUInterfaceTrigger.MCUStateHasChanged))
            {
                _mcuInterfaceSM.Fire(MCUInterfaceTrigger.MCUStateHasChanged);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Current MCUInterface State: {_mcuInterfaceSM.State.ToString()} "
                        + $"cannot deal with trigger: {MCUInterfaceTrigger.MCUStateHasChanged.ToString()}"
                );
            }
        }

        private void ChangeMCUInitialState(MCUStateChangeCommand command)
        {
            switch (command)
            {
                case MCUStateChangeCommand.PBS:
                    _mcuDesState = MCUInterfaceState.Basic;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Current State is {_mcuInterfaceSM.State.ToString()}."
                            + $"Cannot process command: {command.ToString()} "
                    );
            }
            ActivateMCUStateChangeMonitior();
        }

        private void ChangeMCUBasicState(MCUStateChangeCommand command)
        {
            switch (command)
            {
                case MCUStateChangeCommand.PSIM:
                    _mcuDesState = MCUInterfaceState.SimulatedRunning;
                    break;

                case MCUStateChangeCommand.PT:
                    _mcuDesState = MCUInterfaceState.PropulsionTestWithNoCharged;
                    break;

                case MCUStateChangeCommand.PTP:
                    _mcuDesState = MCUInterfaceState.PropulsionTestWithCharged;
                    break;

                case MCUStateChangeCommand.PWSP:
                    _mcuDesState = MCUInterfaceState.StandbyWithCharged;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Current State is {_mcuInterfaceSM.State.ToString()}."
                            + $"Cannot process command: {command.ToString()} "
                    );
            }
            ActivateMCUStateChangeMonitior();
        }

        private void ChangeMCUStandbyWithChargedState(MCUStateChangeCommand command)
        {
            switch (command)
            {
                case MCUStateChangeCommand.PTO:
                    _mcuDesState = MCUInterfaceState.MaglevFrameRunning;
                    break;

                case MCUStateChangeCommand.PPT:
                    _mcuDesState = MCUInterfaceState.PreparedToTest;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Current State is {_mcuInterfaceSM.State.ToString()}."
                            + $"Cannot process command: {command.ToString()} "
                    );
            }
            ActivateMCUStateChangeMonitior();
        }

        //private void changeMCUSimulatedRunningState()
        //{
        //    _mcuDesState = MCUInterfaceState.Basic;
        //    ActivateMCUStateChangeMonitior();
        //}

        private void ChangeMCUPropulsionTestWithChargedState()
        {
            _mcuDesState = MCUInterfaceState.Basic;
            ActivateMCUStateChangeMonitior();
        }

        private void ChangeMCUPropulsionTestWithNoChargedState()
        {
            _mcuDesState = MCUInterfaceState.Basic;
            ActivateMCUStateChangeMonitior();
        }

        private void OnMCUInterfaceSMTransition(
            StateMachine<MCUInterfaceState, MCUInterfaceTrigger>.Transition transition
        )
        {
            // Debug状态下输出状态转移过程中的原状态、触发器、目标状态信息
#if DEBUG
            var source = transition.Source.ToString();
            var trigger = transition.Trigger.ToString();
            var destination = transition.Destination.ToString();
            var message = $"MCUInterface SM transition: {source} --({trigger})-> {destination}";
            System.Diagnostics.Debug.WriteLine(message);
#endif
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

        private void ActivateMCUStateChangeMonitior()
        {
            _mcuStateChangeMonitorSM.Fire(MCUStateChangeMonitorTriggers.Activate);
        }

        private void DeactivateMCUStateChangeMonitor()
        {
            if (_mcuStateChangeMonitorSM.CanFire(MCUStateChangeMonitorTriggers.Deactivate))
            {
                _mcuStateChangeMonitorSM.Fire(MCUStateChangeMonitorTriggers.Deactivate);
            }
        }

        private void MCULifeCycleSendTimeOut()
        {
            _mcuInterfaceSM.Fire(MCUInterfaceTrigger.MOCSLifeCycleMsgSend);
        }

        private void MCULifeCycleRevTimeOut()
        {
            _mcuLifeCycleRecTimeOutCounts++;
            if (_mcuLifeCycleRecTimeOutCounts >= 2)
            {
                IsLifeCycleRecTimeOutBeyondLimits = true;
                _mcuInterfaceSM.Fire(MCUInterfaceTrigger.MCULifeCycleMsgRecvTimeOut);
            }
        }

        private void SendMaglevFrameLoginMsg() { }

        private void SendMaglevFrameSignOutMsg() { }

        private void SendTransmitTrackDataMsg() { }

        private void SendDeleteTrackDataMsg() { }

        private void SendTransmitMaximumCurveMsg() { }

        private void SendDeleteTranmitMaximumCurveMsg() { }

        private readonly StateMachine<MCUInterfaceState, MCUInterfaceTrigger> _mcuInterfaceSM;
        private readonly StateMachine<
            MCUStateChangeMonitorStates,
            MCUStateChangeMonitorTriggers
        > _mcuStateChangeMonitorSM;

        private StateMachine<
            MCUInterfaceState,
            MCUInterfaceTrigger
        >.TriggerWithParameters<MCUStateChangeCommand> _changeMCUStateTrigger;
        private MCUInterfaceState _mcuDesState;

        private HighPrecisionTimer _mcuLifeCycleSendTimer;
        private HighPrecisionTimer _mcuLifeCycleRecTimer;

        private int _mcuLifeCycleRecTimeOutCounts = 0;
    }
}
