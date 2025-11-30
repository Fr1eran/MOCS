using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.VehicleControl.MOCSToVehicle
{
    public class EMSControlMsg : BaseSendMsg, IOutgoingMsg
    {
        /// <summary>
        /// 报文标识号下限
        /// </summary>
        private const byte MsgIdLow = 0x21;

        /// <summary>
        /// 报文标识号上限
        /// </summary>
        private const byte MsgIdHigh = 0x3F;

        /// <summary>
        /// 悬浮控制器控制帧ID
        /// </summary>
        private const byte ControlMsgId = 0x55;
    }
}
