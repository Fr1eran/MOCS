using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MCUToMOCS
{
    /// <summary>
    /// 牵引状态报文发送理由
    /// </summary>
    public enum SendReason : byte
    {
        Response = 0,
        TimeOut = 1,
    }

    /// <summary>
    /// 牵引状态改变就绪信息
    /// </summary>
    public enum MCUStatusChangeReadinessInfo : byte
    {
        None = 1 << 0,
        AlReady_Activate = 1 << 1,
        AlReady_Normal = 1 << 2,
    }

    /// <summary>
    /// 清除悬浮架就绪信息（由于分区切换）
    /// </summary>
    /// <remarks>注意区分“当前”和“虚拟”</remarks>
    public enum ClearMaglevFrameReadinessInfo : byte
    {
        None = 0x01,
        Ready = 0x10,
    }

    /// <summary>
    /// 当前悬浮架停车点运行状态
    /// </summary>
    public enum CurrentMaglevFrameSPRStatus : byte
    {
        UnDefine = 1 << 0,
        UnComplete = 1 << 1,
        Acurate = 1 << 2,
        UnAcurate = 1 << 3,
    }

    public enum MCUFaultStatus : byte
    {
        Fault0 = 0x01,
        Fault1 = 0x02,
        Fault2 = 0x04,
        Fault3 = 0x08,
        UnDefine = 0xFF,
    }

    public sealed class MCUStatusMsg : BaseMessage, IIncomingMsg
    {
    }
}