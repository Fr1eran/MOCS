using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MOCSToMCU
{
    /// <summary>
    /// 停车点类型
    /// </summary>
    public enum StoppingPointType : byte
    {
        Station = 1 << 0,
        StoppingPoint = 1 << 1,
        ServiceArea = 1 << 2,
    }

    public sealed class LineData : BaseSendMsg { }
}
