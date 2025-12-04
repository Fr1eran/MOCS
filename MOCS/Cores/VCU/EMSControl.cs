using System.Buffers.Binary;

namespace MOCS.Cores.VCU
{
    public class EMSControl
    {
        /// <summary>
        /// 悬浮架运行方向
        /// </summary>
        public TrainDirectionEnum TrainDirection { get; set; } = TrainDirectionEnum.Invalid;

        /// <summary>
        /// 生命周期信号
        /// </summary>
        public byte Life { get; set; } = 0;

        /// <summary>
        /// EMS控制命令
        /// </summary>
        public EMSCmdEnum EMSCmd { get; set; } = EMSCmdEnum.DeEnergize;

        /// <summary>
        /// 制动等级
        /// 0~7
        /// </summary>
        /// <remarks>组装成CAN帧时，需要左移5位</remarks>
        public byte BrakeLevel { get; set; } = 0;

        /// <summary>
        /// 电机牵引状态
        /// </summary>
        public ETStatusEnum ETStatus { get; set; } = ETStatusEnum.Deactivate;

        /// <summary>
        /// 电制动状态
        /// </summary>
        public EBStatusEnum EBStatus { get; set; } = EBStatusEnum.Deactivte;

        /// <summary>
        /// 列车速度, 单位: km/h
        /// </summary>
        public byte Velocity { get; set; } = 0;

        /// <summary>
        /// 列车离站距离, 单位: m
        /// </summary>
        /// <remarks>组装成CAN帧时，按大端序</remarks>
        public ushort Distance { get; set; } = 0;

        /// <summary>
        /// 切除命令
        /// </summary>
        public CutCmdEnum CutCmd { get; set; } = CutCmdEnum.None;

        /// <summary>
        /// EMS控制器编号
        /// </summary>
        public byte ControllerId { get; set; } = 0;

        public void Reset()
        {
            TrainDirection = TrainDirectionEnum.Invalid;
            Life = 0;
            EMSCmd = EMSCmdEnum.DeEnergize;
            BrakeLevel = 0;
            ETStatus = ETStatusEnum.Deactivate;
            EBStatus = EBStatusEnum.Deactivte;
            Velocity = 0;
            Distance = 0;
            CutCmd = CutCmdEnum.None;
            ControllerId = 0;
        }

        public byte[] ToCANMsg()
        {
            Span<byte> data = stackalloc byte[8];

            // 列车方向（高2位） | 生命周期信号（低4位）
            data[0] = (byte)((byte)TrainDirection | Life);

            // 电机牵引状态（bit 1） | 电制动状态（bit 0）
            data[1] = (byte)((byte)ETStatus | (byte)EBStatus);

            // 列车速度
            data[2] = Velocity;

            // EMS命令（高3位） | 控制器编号（低5位）
            data[3] = (byte)((byte)EMSCmd | ControllerId);

            // 制动等级（高3位） | 控制器编号（低5位）
            data[4] = (byte)(BrakeLevel << 5 | ControllerId);

            // 距离（双字节，大端序）
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(5, 2), Distance);

            // 切除命令（高3位） | 控制器编号（低5位）
            data[7] = (byte)((byte)CutCmd | ControllerId);

            return data.ToArray();
        }
    }
}
