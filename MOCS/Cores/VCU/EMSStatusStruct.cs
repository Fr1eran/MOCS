namespace MOCS.Cores.VC
{
    /// <summary>
    /// 悬浮控制器状态
    /// </summary>
    public struct EMSStatusStruct
    {
        /// <summary>
        /// 间隙传感器阵列状态
        /// </summary>
        public GapSensorsStatusEnum GapSensorsStatus { get; set; }

        /// <summary>
        /// 1号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor1Status { get; set; }

        /// <summary>
        /// 2号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor2Status { get; set; }

        /// <summary>
        /// 3号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor3Status { get; set; }

        /// <summary>
        /// 4号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor4Status { get; set; }

        /// <summary>
        /// EMS控制命令
        /// </summary>
        public EMSCmdStatusEnum EMSCmd { get; set; }

        /// <summary>
        /// EMS系统状态
        /// </summary>
        public EMSSysStatusEnum EMSSysStatus { get; set; }

        /// <summary>
        /// 过流保护状态
        /// </summary>
        public OverloadStatusEnum OverloadStatus { get; set; }

        /// <summary>
        /// 加速度传感器阵列状态
        /// </summary>
        public AccSensorsStatusEnum AccSensorsStatus { get; set; }

        /// <summary>
        /// 1号加速度传感器状态
        /// </summary>
        public AccSensorStatusEnum AccSensor1Status { get; set; }

        /// <summary>
        /// 2号加速度传感器状态
        /// </summary>
        public AccSensorStatusEnum AccSensor2Status { get; set; }

        /// <summary>
        /// EMS工作状态
        /// </summary>
        public EMSOperationStatusEnum EMSOperationStatus { get; set; }

        /// <summary>
        /// EMS警告状态
        /// </summary>
        public EMSWarningEnum EMSWarning { get; set; }

        /// <summary>
        /// EMS故障状态
        /// </summary>
        public EMSFaultStatusEnum EMSFaultStatus { get; set; }

        /// <summary>
        /// 接触器1状态
        /// </summary>
        public KMStatusEnum KM1Status { get; set; }

        /// <summary>
        /// 接触器2状态
        /// </summary>
        public KMStatusEnum KM2Status { get; set; }

        /// <summary>
        /// 接触器1闭合断开状态
        /// </summary>
        public KMContactStatusEnum KM1ContactStatus { get; set; }

        /// <summary>
        /// 接触器2闭合断开状态
        /// </summary>
        public KMContactStatusEnum KM2ContactStatus { get; set; }

        /// <summary>
        /// 板卡状态
        /// </summary>
        public CPUStatusEnum CPUStatus { get; set; }

        /// <summary>
        /// 切除状态
        /// </summary>
        public CutStatusEnum CutStatus { get; set; }

        /// <summary>
        /// 失稳状态
        /// </summary>
        public StabilityStatusEnum StabilityStatus { get; set; }

        /// <summary>
        /// 过流预警状态
        /// </summary>
        public OverloadWarningStatusEnum OverloadWarningStatus { get; set; }

        /// <summary>
        /// 间隙预警状态
        /// </summary>
        public GapWarnningStatusEnum GapWarnningStatus { get; set; }

        /// <summary>
        /// 控制系统切换状态
        /// </summary>
        public SysSwitchStatusEnum SysSwitchStatus { get; set; }

        /// <summary>
        /// 制动状态
        /// </summary>
        public BrakeStatusEnum BrakeStatus { get; set; }

        /// <summary>
        /// EMS控制器生命周期信号
        /// 参考值 0x0~0xF
        /// </summary>
        public byte Life { get; set; }

        /// <summary>
        /// 温度值
        /// 0~127 表示 -27~100 摄氏度
        /// </summary>
        public int Temp { get; set; }

        /// <summary>
        /// 气隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap { get; set; }

        /// <summary>
        /// 第一路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap1 { get; set; }

        /// <summary>
        /// 第二路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap2 { get; set; }

        /// <summary>
        /// 第三路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap3 { get; set; }

        /// <summary>
        /// 第四路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap4 { get; set; }

        /// <summary>
        /// EMS主电路电压值
        /// 0~127 表示 0~530V
        /// </summary>
        public float U { get; set; }

        /// <summary>
        /// 第一路电流传感器值
        /// 0~127 表示 0~160A
        /// </summary>
        public float I1 { get; set; }

        /// <summary>
        /// 第二路电流传感器值
        /// 0~127 表示 0~160A
        /// </summary>
        public float I2 { get; set; }

        /// <summary>
        /// 加速度值
        /// 0~127 表示 -50 ~ +50 m/s^2
        /// </summary>
        public float Acc { get; set; }

        /// <summary>
        /// 第一路加速度值
        /// 0~127 表示 -50 ~ +50 m/s^2
        /// </summary>
        public float Acc1 { get; set; }

        /// <summary>
        /// 第二路加速度值
        /// 0~127 表示 -50 ~ +50 m/s^2
        /// </summary>
        public float Acc2 { get; set; }
    }
}
