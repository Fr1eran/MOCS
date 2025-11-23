using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.VehicleControl.MOCSToVehicle
{
    enum TrainDirection : byte
    {
        Invalid = 0b_1100_0000,
        UpBound = 1 << 7,
        DownBound = 1 << 6,
        Error = 0x00,
    }

    enum ETStatus : byte
    {
        Activate = 1 << 1,
        Deactivate = 0x00,
    }

    enum EBStatus : byte
    {
        Activate = 1,
        Deactivte = 0x00,
    }

    enum LevCmd : byte
    {
        Land = 0x00,
        Leviate = 1 << 5,
    }

    enum BrakeCmd : byte
    {
        Max = 0b_1110_0000,
        Min = 0x00,
    }

    enum QCQH : byte
    {
        None = 0x00,
        Switch = 0b_0010_0000,
        Cut = 0b_0110_0000,
        Reset = 0b_0100_0000,
    }

    public class LCUControlMsg : BaseSendMsg, IOutgoingMsg
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
