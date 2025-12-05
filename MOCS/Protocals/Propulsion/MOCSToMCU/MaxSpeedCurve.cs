using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MOCSToMCU
{
    /// <summary>
    /// 是否为本分区内发往MCU的最后一个最大速度曲线报文标识
    /// </summary>
    public enum LastMsgIdentify : byte
    {
        IsLast = 0x00,
        NotLast = 0x01,
    }

    public sealed class MaxSpeedCurve : BaseSendMsg { }
}
