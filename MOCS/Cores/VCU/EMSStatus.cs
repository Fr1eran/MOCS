namespace MOCS.Cores.VCU
{
    /// <summary>
    /// 悬浮控制器状态
    /// </summary>
    public class EMSStatus
    {
        /// <summary>
        /// 间隙传感器阵列状态
        /// </summary>
        public GapSensorsStatusEnum GapSensorsStatus { get; set; } = GapSensorsStatusEnum.Normal;

        /// <summary>
        /// 1号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor1Status { get; set; } = GapSensorStatusEnum.Normal;

        /// <summary>
        /// 2号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor2Status { get; set; } = GapSensorStatusEnum.Normal;

        /// <summary>
        /// 3号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor3Status { get; set; } = GapSensorStatusEnum.Normal;

        /// <summary>
        /// 4号间隙传感器状态
        /// </summary>
        public GapSensorStatusEnum GapSensor4Status { get; set; } = GapSensorStatusEnum.Normal;

        /// <summary>
        /// EMS控制命令
        /// </summary>
        public EMSCmdStatusEnum EMSCmd { get; set; } = EMSCmdStatusEnum.DeEnergize;

        /// <summary>
        /// EMS系统状态
        /// </summary>
        public EMSSysStatusEnum EMSSysStatus { get; set; } = EMSSysStatusEnum.Normal;

        /// <summary>
        /// 过流保护状态
        /// </summary>
        public OverloadStatusEnum OverloadStatus { get; set; } = OverloadStatusEnum.Deactivate;

        /// <summary>
        /// 加速度传感器阵列状态
        /// </summary>
        public AccSensorsStatusEnum AccSensorsStatus { get; set; } = AccSensorsStatusEnum.Normal;

        /// <summary>
        /// 1号加速度传感器状态
        /// </summary>
        public AccSensorStatusEnum AccSensor1Status { get; set; } = AccSensorStatusEnum.Normal;

        /// <summary>
        /// 2号加速度传感器状态
        /// </summary>
        public AccSensorStatusEnum AccSensor2Status { get; set; } = AccSensorStatusEnum.Normal;

        /// <summary>
        /// EMS工作状态
        /// </summary>
        public EMSOperationStatusEnum EMSOperationStatus { get; set; } =
            EMSOperationStatusEnum.DeEnergized;

        /// <summary>
        /// EMS警告状态
        /// </summary>
        public EMSWarningEnum EMSWarning { get; set; } = EMSWarningEnum.Normal;

        /// <summary>
        /// EMS故障状态
        /// </summary>
        public EMSFaultStatusEnum EMSFaultStatus { get; set; } = EMSFaultStatusEnum.Normal;

        /// <summary>
        /// 接触器1状态
        /// </summary>
        public KMStatusEnum KM1Status { get; set; } = KMStatusEnum.Normal;

        /// <summary>
        /// 接触器2状态
        /// </summary>
        public KMStatusEnum KM2Status { get; set; } = KMStatusEnum.Normal;

        /// <summary>
        /// 接触器1闭合断开状态
        /// </summary>
        public KMContactStatusEnum KM1ContactStatus { get; set; } = KMContactStatusEnum.Open;

        /// <summary>
        /// 接触器2闭合断开状态
        /// </summary>
        public KMContactStatusEnum KM2ContactStatus { get; set; } = KMContactStatusEnum.Open;

        /// <summary>
        /// 板卡状态
        /// </summary>
        public CPUStatusEnum CPUStatus { get; set; } = CPUStatusEnum.Normal;

        /// <summary>
        /// 切除状态
        /// </summary>
        public CutStatusEnum CutStatus { get; set; } = CutStatusEnum.Normal;

        /// <summary>
        /// 失稳状态
        /// </summary>
        public StabilityStatusEnum StabilityStatus { get; set; } = StabilityStatusEnum.Normal;

        /// <summary>
        /// 过流预警状态
        /// </summary>
        public OverloadWarningStatusEnum OverloadWarningStatus { get; set; } =
            OverloadWarningStatusEnum.Normal;

        /// <summary>
        /// 间隙预警状态
        /// </summary>
        public GapWarnningStatusEnum GapWarnningStatus { get; set; } = GapWarnningStatusEnum.Normal;

        /// <summary>
        /// 控制系统切换状态
        /// </summary>
        public SysSwitchStatusEnum SysSwitchStatus { get; set; } = SysSwitchStatusEnum.NoChange;

        /// <summary>
        /// 制动状态
        /// </summary>
        public BrakeStatusEnum BrakeStatus { get; set; } = BrakeStatusEnum.NoBrake;

        /// <summary>
        /// EMS控制器生命周期信号
        /// 参考值 0x0~0xF
        /// </summary>
        public byte Life { get; set; } = 0;

        /// <summary>
        /// 温度值
        /// 0~127 表示 -27~100 摄氏度
        /// </summary>
        public int Temp { get; set; } = 20;

        /// <summary>
        /// 气隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap { get; set; } = 0;

        /// <summary>
        /// 第一路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap1 { get; set; } = 0;

        /// <summary>
        /// 第二路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap2 { get; set; } = 0;

        /// <summary>
        /// 第三路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap3 { get; set; } = 0;

        /// <summary>
        /// 第四路间隙值
        /// 0~127 表示 0~20mm
        /// </summary>
        public float Gap4 { get; set; } = 0;

        /// <summary>
        /// EMS主电路电压值
        /// 0~127 表示 0~530V
        /// </summary>
        public float U { get; set; } = 0;

        /// <summary>
        /// 第一路电流传感器值
        /// 0~127 表示 0~160A
        /// </summary>
        public float I1 { get; set; } = 0;

        /// <summary>
        /// 第二路电流传感器值
        /// 0~127 表示 0~160A
        /// </summary>
        public float I2 { get; set; } = 0;

        /// <summary>
        /// 加速度值
        /// 0~127 表示 -50 ~ +50 m/s^2
        /// </summary>
        public float Acc { get; set; } = 0;

        /// <summary>
        /// 第一路加速度值
        /// 0~127 表示 -50 ~ +50 m/s^2
        /// </summary>
        public float Acc1 { get; set; } = 0;

        /// <summary>
        /// 第二路加速度值
        /// 0~127 表示 -50 ~ +50 m/s^2
        /// </summary>
        public float Acc2 { get; set; } = 0;

        public void Reset()
        {
            GapSensorsStatus = GapSensorsStatusEnum.Normal;
            GapSensor1Status = GapSensorStatusEnum.Normal;
            GapSensor2Status = GapSensorStatusEnum.Normal;
            GapSensor3Status = GapSensorStatusEnum.Normal;
            GapSensor4Status = GapSensorStatusEnum.Normal;
            EMSCmd = EMSCmdStatusEnum.DeEnergize;
            EMSSysStatus = EMSSysStatusEnum.Normal;
            OverloadStatus = OverloadStatusEnum.Deactivate;
            AccSensorsStatus = AccSensorsStatusEnum.Normal;
            AccSensor1Status = AccSensorStatusEnum.Normal;
            AccSensor2Status = AccSensorStatusEnum.Normal;
            EMSOperationStatus = EMSOperationStatusEnum.DeEnergized;
            EMSWarning = EMSWarningEnum.Normal;
            EMSFaultStatus = EMSFaultStatusEnum.Normal;
            KM1Status = KMStatusEnum.Normal;
            KM2Status = KMStatusEnum.Normal;
            KM1ContactStatus = KMContactStatusEnum.Open;
            KM2ContactStatus = KMContactStatusEnum.Open;
            CPUStatus = CPUStatusEnum.Normal;
            CutStatus = CutStatusEnum.Normal;
            StabilityStatus = StabilityStatusEnum.Normal;
            OverloadWarningStatus = OverloadWarningStatusEnum.Normal;
            GapWarnningStatus = GapWarnningStatusEnum.Normal;
            SysSwitchStatus = SysSwitchStatusEnum.NoChange;
            BrakeStatus = BrakeStatusEnum.NoBrake;
            Life = 0;
            Temp = 0;
            Gap = 0;
            Gap1 = 0;
            Gap2 = 0;
            Gap3 = 0;
            Gap4 = 0;
            U = 0;
            I1 = 0;
            I2 = 0;
            Acc = 0;
            Acc1 = 0;
            Acc2 = 0;
        }
    }
}
