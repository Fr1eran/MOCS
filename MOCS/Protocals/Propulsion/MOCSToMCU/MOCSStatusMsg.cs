using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.Propulsion.MOCSToMCU
{
    /// <summary>
    /// 通道接收状态
    /// </summary>
    /// <remarks>注意通道编号</remarks>
    public enum ChannelRecvStatus : byte
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
    /// 牵引状态报文接收状态
    /// </summary>
    public enum MCUStatusMessageStatus : byte
    {
        HasRecvd = 0x00,
        NoRecv = 1,
    }

    /// <summary>
    /// 与牵引状态改变就绪的请求
    /// </summary>
    public enum RequestForMCUStatusChangeReady : byte
    {
        None = 1 << 0,
        Delete = 1 << 1,
    }

    /// <summary>
    /// 悬浮/落下命令处理状态
    /// </summary>
    public enum MaglevFrameSuspendDropCommandStatus : byte
    {
        UnDefined = 0x00,

        Suspend = 1 << 0,
        Standstill = 1 << 1,
    }

    /// <summary>
    /// 悬浮架悬浮/落下状态
    /// </summary>
    public enum MaglevFrameSuspendDropStatus : byte
    {
        UnDefined = 0x00,
        Standstill = 1 << 2,
        Suspending = 1 << 3,
        Suspended = 1 << 4,
    }

    /// <summary>
    /// 期望速度的默认类型
    /// </summary>
    public enum ExpectedSpeedType : byte
    {
        Direct = 0x00,
        Percent = 0b_0000_0011,
    }

    /// <summary>
    /// 期望运行方向
    /// </summary>
    public enum ExpectedRunningDircetion : byte
    {
        Same = 0x00,
        Oppsite = 0b_0011_0000,
    }

    /// <summary>
    /// 系统诊断信息
    /// </summary>
    public enum DiagnosticInfo : byte
    {
        None = 0x00,
        Error = 1 << 0,
        CanNotHandleResendRequest = 1 << 1,
    }

    public sealed class MOCSStatusMsg : BaseSendMsg { }
}
