namespace MOCS.Cores.MCU
{
    public enum MCUInterfaceState
    {
        Stop, // 停止状态
        UnConnected, // 未连接状态
        Connected, // 连接状态

        // 连接状态下的牵引系统子状态
        UnKnown, // 未知状态，Connected状态会自动进入此状态

        Initial, // 初始状态
        Basic, // 基本状态
        StandbyWithCharged, // 带电等待状态
        MaglevFrameRunning, // 悬浮架运行状态
        PreparedToTest, // 准备测试状态
        PropulsionTestWithCharged, // 带电牵引测试状态
        PropulsionTestWithNoCharged, // 不带电牵引测试状态
        SimulatedRunning, // 模拟运行状态
    }

    public enum MCUInterfaceTrigger
    {
        Activate, // 启动
        Deactivate, // 关闭
        MOCSLifeCycleMsgSend, // MOCS生命周期（状态）报文发送
        MCULifeCycleMsgRecvTimeOut, // MCU生命周期（状态）报文接收超时
        MCULifeCycleMsgRecvd, // 收到MCU生命周期（状态）报文
        MCUAckRecvTimeOut, // 超时未收到MCU应答报文
        MCUAckRecvd, // 接收到应答报文
        ChangeMCUState, // 改变MCU状态
        MCUStateHasChanged, // 牵引系统状态改变完成

        // 以下触发器只在状态为“悬浮架运行”时有效
        MaglevFrameLoginIn, // 悬浮架登录

        MaglevFrameSignOut, // 悬浮架注销
        TransmitTrackData, // 传输线路数据
        DeleteTrackData, // 删除线路数据
        TransmitMaximumCurve, // 传输最大速度曲线
        DeleteMaximumCurve, // 删除最大速度曲线
        CalBrakeCurve, // 计算制动曲线
    }

    public enum MCUStateChangeCommand
    {
        // 从初始状态转换
        PBS, // 基本状态

        // 从基本状态转换
        PSIM, // 模拟运行状态

        PT, // 不带电牵引测试状态
        PTP, // 带电牵引测试状态
        PWSP, // 带电等待状态

        // 从带电等待状态转换
        PTO, // 悬浮架运行状态

        PPT, // 准备测试状态
    }

    public enum MCUStateChangeMonitorStates
    {
        Stop,
        Unchanged,
        IsChanging,
        Changed,
    }

    public enum MCUStateChangeMonitorTriggers
    {
        Activate,
        Deactivate,
        Executed,
        UnExecute,
    }
}
