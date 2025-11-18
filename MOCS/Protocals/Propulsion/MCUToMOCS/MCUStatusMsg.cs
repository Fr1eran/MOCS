using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MCUToMOCS
{
    /// <summary>
    /// 通道接收状态
    /// </summary>
    /// <remarks>注意通道编号</remarks>
    public enum MCUStatus_ChannelRecvStatus : byte
    {
        Normal = 0x00,
        CanNotRecv = 1 << 0,
        OnlyRecvFaultMsg = 1 << 1,
        CRCErrorOnce = 1 << 2,
        OnlyMsgIdError = 1 << 3,
        OnlyDesIdError = 1 << 4,
        OnlySrcIdError = 1 << 5,
    }

    /// <summary>
    /// 牵引状态报文发送理由（响应MOCS）
    /// </summary>
    public enum MCUStatus_SendReason : byte
    {
        Response = 0,
        TimeOut = 1,
    }
    ///<summary>
    ///要重发以前MOCS（DCS）报文的数量
    ///</summary>
    public enum MCUStatus_RepeatMOCSMsgCount : byte
    {
        None=0x00,
        Repeat1 = 0x01,
        Repeat2 = 0x02,
        Repeat3 = 0x03,
        Repeat4 = 0x04,
        Repeat5 = 0x05,
        Repeat6 = 0x06,
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

    /// <summary>
    /// 牵引故障状态状态
    /// </summary>

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