using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MOCSToMCU
{
    /// <summary>
    /// 要求切换到的状态
    /// </summary>
    public enum StatusChangeTo : byte
    {
        MCUCurrentStatus = 0x00,
        Initial = 0x01,
        Basic = 0x02,
        PoweredStandby = 0x04,
        MaglevFrameRunning = 0x08,
        ReadyForTest = 0x10,
        PoweredTractionTest = 0x20,
        UnpoweredTractionTest = 0x40,
        SimulationRunning = 0x80,
    }

    public sealed class TractionStatusChange : BaseSendMsg { }
}
