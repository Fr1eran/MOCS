using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Protocals.VehicleControl.MOCSToVehicle
{
    public class EMSControlMsg : BaseSendMsg
    {
        /// <summary>
        /// 报文标识号下限
        /// </summary>
        //private const byte MsgIdLow = 0x21;

        /// <summary>
        /// 报文标识号上限
        /// </summary>
        //private const byte MsgIdHigh = 0x3F;

        /// <summary>
        /// EMS控制器控制帧ID
        /// </summary>
        private static readonly byte EMSControlCANMsgId = 0x55;

        //public override ReadOnlyMemory<byte> UserData { get; set; } =
        //    new byte[] { 0x55, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public ReadOnlyMemory<byte> _userData;
        public override ReadOnlyMemory<byte> UserData
        {
            get => this._userData;
            set => this._userData = ToUserData(value);
        }

        private static ReadOnlyMemory<byte> ToUserData(ReadOnlyMemory<byte> buffer)
        {
            var result = new byte[buffer.Length + 2];
            result[0] = EMSControlCANMsgId;
            buffer.CopyTo(result.AsMemory()[1..^1]);
            return result;
        }
    }
}
