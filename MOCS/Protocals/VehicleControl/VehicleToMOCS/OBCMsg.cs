using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MOCS.Protocals.VehicleControl.VehicleToMOCS
{
    public class OBCMsg : BaseSendMsg, IIncomingMsg<BaseMessage>
    {
        private ReadOnlyMemory<byte> _userData;
        public override ReadOnlyMemory<byte> UserData
        {
            get => this._userData;
            set { this._userData = ToUserData(value); }
        }

        public static (BaseMessage? msg, string? error) Parse(ReadOnlyMemory<byte> buffer)
        {
            OBCMsg? msg = null;
            string? error = null;

            var span = buffer.Span;

            var statusMsgLen = buffer.Length - 9;
            if (statusMsgLen != 15)
            {
                error = $"车载OBC的状态报文有用数据段长度:{statusMsgLen}不为15字节";
                return (msg, error);
            }

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(span[..2]);
            var repeat = span[2];
            var dest = span[4];
            var src = span[5];
            var partId = span[6];
            var msgId = span[7];
            SysWideLogger.Info($"收到VSPS状态报文, MsgID: {msgId:X2}");

            msg = new OBCMsg
            {
                SequenceNumber = seq,
                RepeatCounter = repeat,
                Destination = dest,
                Source = src,
                PartId = partId,
                MsgId = msgId,
                UserData = buffer.Slice(9, 15),
            };

            return (msg, error);
        }

        private static readonly byte PaddingBytesNum = 1;

        private static ReadOnlyMemory<byte> ToUserData(ReadOnlyMemory<byte> buffer)
        {
            var result = new byte[buffer.Length + PaddingBytesNum];
            buffer.CopyTo(result);
            result[buffer.Length] = 0x00;
            return result;
        }
    }
}
