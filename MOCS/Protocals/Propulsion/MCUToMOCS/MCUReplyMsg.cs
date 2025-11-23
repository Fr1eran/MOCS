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
    public enum MCUReply_ChannelRecvStatus : byte
    {
        Normal = 0x00,
        CanNotRecv = 1 << 0,
        OnlyRecvFaultMsg = 1 << 1,
        CRCErrorOnce = 1 << 2,
        OnlyMsgIdError = 1 << 3,
        OnlyDesIdError = 1 << 4,
        OnlySrcIdError = 1 << 5,
    }

    ///<summary>
    ///要重发以前MOCS（DCS）报文的数量
    ///</summary>
    public enum MCUReply_RepeatMOCSMsgCount : byte
    {
        None = 0x00,
        Repeat1 = 0x01,
        Repeat2 = 0x02,
        Repeat3 = 0x03,
        Repeat4 = 0x04,
        Repeat5 = 0x05,
        Repeat6 = 0x06,
    }

    ///<summary>
    ///牵引应答报文错误标识号
    ///</summary>
    public enum MCUReply_ErrorIdentifier : byte
    {
        NoError = 0,
        NoError_AlreadyInExpectedState = 1,
        SequenceNumberInvalid = 2,
        ///其他错误标识号待定
    }

    ///<summary>
    ///牵引应答报文请求当前管理的牵引状态
    ///</summary>
    public enum MCUReply_TractionStatusRequest : byte
    {
        Initial = 0x01,
        Basic = 0x02,
        PoweredStandby = 0x04,
        MaglevFrameRunning = 0x08,
        ReadyForTest = 0x10,
        PoweredTractionTest = 0x20,
        UnpoweredTractionTest = 0x40,
        SimulationRunning = 0x80,
    }

    public sealed class MCUReplyMsg : BaseMessage, IOutgoingMsg { }
}
