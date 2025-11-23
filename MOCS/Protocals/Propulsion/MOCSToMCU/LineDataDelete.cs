using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MOCSToMCU
{
    /// <summary>
    /// 删除范围标识
    /// </summary>
    public enum DeleteAreaType : byte
    {
        SpecifiedKIStoppingPoint = 1 << 0,
        AllFromSpecifiedPosition_SPosExcept = 1 << 1,
        AllLineData = 1 << 2,
    }

    public sealed class LineDataDelete : BaseSendMsg { }
}
