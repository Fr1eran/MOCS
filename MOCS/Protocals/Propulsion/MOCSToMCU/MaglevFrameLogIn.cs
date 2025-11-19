using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MOCSToMCU
{
    /// <summary>
    /// 悬浮架类型
    /// </summary>   
    /// <remarks>第1位~第7位有预留</remarks>
    public enum MaglevFrameType : byte
    {
        MaglevFrame=0,
    }

    /// <summary>
    /// 悬浮架方向
    /// </summary>   
    /// <remarks>第2位~第7位有预留</remarks>
    public enum MaglevFrameDir : byte
    {
        UnknownDir = 0x00,
        HeadV1DirIncreasing = 1<<0,
        HeadV1DirDecreasing = 1<<1,
    }


    public sealed class MaglevFrameLogIn : BaseSendMsg
    {
    }
}
