using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Cores.MCU
{
    public struct MOCSStatus
    {
        /// <summary>
        /// 接收通道1状态
        /// </summary>
        public ChannelRecvStatusEnum Channel1RecvStatus { get; set; }

        /// <summary>
        /// 接收通道2状态
        /// </summary>
        public ChannelRecvStatusEnum Channel2RecvStatus { get; set; }

        /// <summary>
        /// 牵引系统状态报文接收状态
        /// </summary>
        public MCUStatusMessageRecvStatusEnum MCUStatusMessageRecvStatus { get; set; }

        /// <summary>
        /// 与牵引状态改变就绪信息有关的请求
        /// </summary>
        public RequestForMCUStatusChangeReadyEnum RequestForMCUStatusChangeReady { get; set; }

        /// <summary>
        /// 悬浮/落下命令处理状态
        /// </summary>
        public MaglevFrameSuspendDropCommandStatusEnum MaglevFrameSuspendDropCommandStatus { get; set; }
    }
}
