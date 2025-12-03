namespace MOCS.Cores.MCU
{
    #region 牵引系统通讯接口状态机
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
    #endregion

    #region MOCS状态报文
    /// <summary>
    /// 通道接收状态
    /// Normal - 接收通道正常
    /// CanNotRecv - 此通道接收不到报文
    /// OnlyRecvFaultMsg - 通道只能接收到错误报文
    /// CRCErrorOnce - 发生一次CRC错误
    /// OnlyMsgIdError - 只有报文标识号错误, CRC正确
    /// OnlyDesIdError  - 只有目的方编码错误
    /// OnlySrcIdError - 只有发送方编码错误
    /// </summary>
    /// <remarks>注意通道编号</remarks>
    public enum ChannelRecvStatusEnum : byte
    {
        Normal = 0x00,
        CanNotRecv = 1 << 0,
        OnlyRecvFaultMsg = 1 << 1,
        CRCErrorOnce = 1 << 2,
        OnlyMsgIdError = 1 << 3,
        OnlyDesIdError = 1 << 4,
        OnlySrcIdError = 1 << 5,
    }

    /// <summary>
    /// 牵引系统状态报文接收状态
    /// HasRecvd - 已收到牵引控制系统对上一次DCS状态报文响应的报文
    /// NoRecv - 没有收到牵引控制系统对上一次DCS状态报文响应的报文
    /// </summary>
    public enum MCUStatusMessageRecvStatusEnum : byte
    {
        HasRecvd = 0x00,
        NoRecv = 1,
    }

    /// <summary>
    /// 与牵引状态改变就绪有关的请求
    /// None - 无与就绪信息有关的请求
    /// Delete - 删除牵引状态改变就绪信息
    /// </summary>
    public enum RequestForMCUStatusChangeReadyEnum : byte
    {
        None = 1 << 0,
        Delete = 1 << 1,
    }

    /// <summary>
    /// 悬浮/落下命令处理状态
    /// UnDefined - 未定义/MOCS未发出命令
    /// Suspend - MOCS发出悬浮架悬浮命令并正在处理中
    /// Standstill - MOCS发出悬浮架落下命令并正在处理中
    /// </summary>
    public enum MaglevFrameSuspendDropCommandStatusEnum : byte
    {
        UnDefined = 0x00,
        Suspend = 1 << 0,
        Standstill = 1 << 1,
    }

    /// <summary>
    /// 悬浮架悬浮/落下状态
    /// UnDefined - 未定义
    /// Standstill - 悬浮架安全的处于standstill（落下）
    /// Suspending - 悬浮架未悬浮
    /// Suspended - 悬浮架已悬浮
    /// </summary>
    public enum MaglevFrameSuspendDropStatusEnum : byte
    {
        UnDefined = 0x00,
        Standstill = 1 << 2,
        Suspending = 1 << 3,
        Suspended = 1 << 4,
    }

    /// <summary>
    /// 期望速度的默认类型
    /// </summary>
    public enum ExpectedSpeedTypeEnum : byte
    {
        Direct = 0x00,
        Percent = 0b_0000_0011,
    }

    /// <summary>
    /// 期望运行方向
    /// </summary>
    public enum ExpectedRunningDircetionEnum : byte
    {
        Same = 0x00,
        Oppsite = 0b_0011_0000,
    }

    /// <summary>
    /// 系统诊断信息
    /// </summary>
    public enum DiagnosticInfoEnum : byte
    {
        None = 0x00,
        Error = 1 << 0,
        CanNotHandleResendRequest = 1 << 1,
    }
    #endregion
}
