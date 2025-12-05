namespace MOCS.Cores.VCU
{
    public class VSPSInfo
    {
        /// <summary>
        /// 生命信号
        /// </summary>
        public ushort Life { get; set; } = 0;

        /// <summary>
        /// 方向
        /// true - 正向
        /// false - 反向
        /// </summary>
        public bool Forward { get; set; } = true;

        /// <summary>
        /// 相对位置
        /// 单位: mm
        /// </summary>
        public ushort RelativePos { get; set; } = 0;

        /// <summary>
        /// 速度
        /// 单位: cm/s
        /// </summary>
        public ushort Speed { get; set; } = 0;

        public void Reset()
        {
            Life = 0;
            Forward = true;
            RelativePos = 0;
            Speed = 0;
        }
    }
}
