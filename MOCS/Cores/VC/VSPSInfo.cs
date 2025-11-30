using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Cores.VC
{
    public struct VSPSInfo
    {
        /// <summary>
        /// 生命信号
        /// 0-255
        /// </summary>
        public byte Life { get; set; }

        /// <summary>
        /// 方向
        /// true - 正向
        /// false - 反向
        /// </summary>
        public bool Forward { get; set; }

        /// <summary>
        /// 相对位置
        /// 单位: mm
        /// </summary>
        public ushort RelativePos { get; set; }

        /// <summary>
        /// 速度
        /// 单位: cm/s
        /// </summary>
        public ushort Speed { get; set; }

        public VSPSInfo()
        {
            Life = 0;
            Forward = true;
            RelativePos = 0;
            Speed = 0;
        }
    }
}
