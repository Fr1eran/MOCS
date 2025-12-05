namespace MOCS.Cores.VCU
{
    #region 车载控制器接口状态机
    public enum VCInterfaceState
    {
        Stop,
        Running,
    }

    public enum VCInterfaceTrigger
    {
        Activate,
        Deactivate,
        LCUMsgSend,
    }
    #endregion

    #region EMS状态帧
    /// <summary>
    /// 间隙传感器阵列状态
    /// Normal - 正常
    /// OneFailed - 一路间隙传感器故障
    /// TwoFailed - 两路间隙传感器故障
    /// ThreeFailed - 三路间隙传感器故障或间隙传感器1和3同时故障
    /// FourFailed - 四路间隙传感器故障
    /// </summary>
    public enum GapSensorsStatusEnum : byte
    {
        Normal = 0x00,
        OneFailed = 1 << 5,
        TwoFailed = 1 << 6,
        ThreeFailed = 0b_0110_0000,
        FourFailed = 1 << 7,
    }

    /// <summary>
    /// 间隙传感器状态
    /// Normal - 正常
    /// Failed - 故障
    /// </summary>
    public enum GapSensorStatusEnum : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    /// <summary>
    /// EMS控制命令信号值
    /// Energize - 线圈通电
    /// DeEnergize - 线圈断电
    /// </summary>
    public enum EMSCmdStatusEnum : byte
    {
        Energize = 1 << 4,
        DeEnergize = 0x00,
    }

    /// <summary>
    /// EMS系统状态
    /// Normal - 正常
    /// Failed - 故障
    /// </summary>
    /// <remarks>
    /// 系统状态满足以下任意条件时认为故障：
    /// 1.间隙 > 14mm，持续时间大于500ms
    /// 2.电流反馈值 < 5A，持续时间大于500ms
    /// </remarks>
    public enum EMSSysStatusEnum : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    /// <summary>
    /// 过流保护状态
    /// Deactivate - 未过流
    /// Activate - 过流保护
    /// </summary>
    /// <remarks>
    /// 判断条件如下：
    /// 1.60A < 电流平均值 <= 80A，持续60s
    /// 2.80A < 电流平均值 < 110A，持续30s
    /// 3.110A <= 电流平均值, 持续10s
    /// </remarks>
    public enum OverloadStatusEnum : byte
    {
        Deactivate = 0x00,
        Activate = 1 << 6,
    }

    /// <summary>
    /// 加速度传感器阵列状态
    /// Normal - 正常
    /// OneFailed - 一路加速度传感器故障
    /// TwoFailed - 两路加速度传感器故障
    /// </summary>
    public enum AccSensorsStatusEnum : byte
    {
        Normal = 0x00,
        OneFailed = 1 << 4,
        TwoFailed = 1 << 5,
    }

    /// <summary>
    /// 加速度传感器状态
    /// Normal - 正常
    /// Failed - 故障
    /// </summary>
    public enum AccSensorStatusEnum : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    /// <summary>
    /// EMS工作状态
    /// Energized - 线圈已通电
    /// DeEnergized - 线圈已断电
    /// </summary>
    public enum EMSOperationStatusEnum : byte
    {
        Energized = 1 << 3,
        DeEnergized = 0x00,
    }

    /// <summary>
    /// EMS警告
    /// Normal - 正常
    /// Warn - 警告
    /// </summary>
    /// <remarks>
    /// 满足以下任意条件之一，则定义为警告状态
    /// 1.一路间隙传感器故障
    /// 2.两路间隙传感器故障
    /// 3.一路加速度传感器故障
    /// 4.65 < 温度值 < 85
    /// 5.发生过流预警
    /// 6.发生间隙预警
    /// </remarks>
    public enum EMSWarningEnum : byte
    {
        Normal = 0x00,
        Warn = 1 << 2,
    }

    /// <summary>
    /// EMS故障状态
    /// Normal - 正常
    /// Short - 短路
    /// Voltage - 欠压或过压
    /// Other - 其他故障
    /// </summary>
    /// <remarks>
    /// 满足以下任意条件，则定义为其他故障
    /// 1.板卡故障
    /// 2.接触器1故障
    /// 3.接触器2故障
    /// 4.三路间隙传感器故障或间隙传感器1和3同时故障
    /// 5.四路间隙传感器故障
    /// 6.两路加速度传感器故障
    /// 7.发生起浮故障
    /// 8.温度 > 85
    /// 9.发生输出切除
    /// 10.系统失稳
    /// 11.开启过流保护
    /// </remarks>
    public enum EMSFaultStatusEnum : byte
    {
        Normal = 0x00,
        Short = 1 << 0,
        Voltage = 1 << 1,
        Other = 0b_0000_0011,
    }

    /// <summary>
    /// 接触器故障状态
    /// Normal - 正常
    /// Failed - 故障
    /// </summary>
    public enum KMStatusEnum : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    /// <summary>
    /// 接触器闭合断开状态
    /// Closed - 闭合
    /// Open - 断开
    /// </summary>
    public enum KMContactStatusEnum : byte
    {
        Closed = 1 << 7,
        Open = 0x00,
    }

    /// <summary>
    /// EMS控制器板卡状态
    /// Normal - 正常
    /// Failed - 故障
    /// </summary>
    public enum CPUStatusEnum : byte
    {
        Normal = 0x00,
        Failed = 1 << 7,
    }

    /// <summary>
    /// 切除状态
    /// Normal - 正常
    /// Cut - 切除
    /// </summary>
    public enum CutStatusEnum : byte
    {
        Normal = 0,
        Cut = 1,
    }

    /// <summary>
    /// 失稳状态
    /// Normal - 正常
    /// UnStable - 失稳
    /// </summary>
    public enum StabilityStatusEnum : byte
    {
        Normal = 0,
        UnStable = 1 << 1,
    }

    /// <summary>
    /// 过流预警
    /// Normal - 正常
    /// Warn - 警告
    /// </summary>
    public enum OverloadWarningStatusEnum : byte
    {
        Normal = 0,
        Warn = 1 << 2,
    }

    /// <summary>
    /// 间隙预警
    /// Normal - 正常
    /// Warn - 警告
    /// </summary>
    public enum GapWarnningStatusEnum : byte
    {
        Normal = 0,
        Warn = 1 << 3,
    }

    /// <summary>
    /// 控制系统切换状态
    /// NoChange - 未切换
    /// HasChanged - A系统切换至B系统
    /// </summary>
    public enum SysSwitchStatusEnum : byte
    {
        NoChange = 0,
        HasChanged = 1 << 4,
    }

    /// <summary>
    /// 制动状态
    /// NoBrake - 未制动
    /// HasBraked - 已制动
    /// </summary>
    public enum BrakeStatusEnum : byte
    {
        NoBrake = 0,
        HasBraked = 1 << 5,
    }

    #endregion

    #region EMS控制帧
    /// <summary>
    /// 悬浮架运行方向
    /// Invalid - 无效
    /// UpBound - 上行
    /// DownBound - 下行
    /// Error - 错误
    /// </summary>
    public enum TrainDirectionEnum : byte
    {
        Invalid = 0b_1100_0000,
        UpBound = 1 << 7,
        DownBound = 1 << 6,
        Error = 0x00,
    }

    /// <summary>
    /// 电机牵引状态
    /// Activate - 牵引
    /// Deactivate - 未牵引
    /// </summary>
    public enum ETStatusEnum : byte
    {
        Activate = 1 << 1,
        Deactivate = 0x00,
    }

    /// <summary>
    /// 电制动状态
    /// Activate - 制动
    /// Deactivte - 未制动
    /// </summary>
    public enum EBStatusEnum : byte
    {
        Activate = 1,
        Deactivte = 0x00,
    }

    /// <summary>
    /// EMS命令
    /// Energize - 线圈通电
    /// DeEnergize - 线圈断电
    /// </summary>
    public enum EMSCmdEnum : byte
    {
        Energize = 0x00,
        DeEnergize = 1 << 5,
    }

    enum BrakeLevelEnum : byte
    {
        Max = 7,
        Min = 0,
    }

    /// <summary>
    /// 切除命令
    /// None - 无命令
    /// Switch - 冗余控制系统切换（A 系统切换至 B 系统）
    /// Cut - 切除
    /// Reset - 复位
    /// </summary>
    public enum CutCmdEnum : byte
    {
        None = 0x00,
        Switch = 0b_0010_0000,
        Cut = 0b_0110_0000,
        Reset = 0b_0100_0000,
    }
    #endregion

    #region OBC控制

    /// <summary>
    /// 电池控制命令
    /// Enable - 启动
    /// Disable - 关闭
    /// </summary>
    public enum BatteryCmdEnum
    {
        Enable,
        Disable,
    }

    /// <summary>
    /// 电源命令
    /// Close - 合闸
    /// Open - 分闸
    /// </summary>
    public enum PowerCmdEnum
    {
        Close,
        Open,
    }

    /// <summary>
    /// 受流器命令
    /// Extend - 伸出
    /// Retract - 缩回
    /// </summary>
    public enum PantographCmdEnum
    {
        Extend,
        Retract,
    }

    #endregion
}
