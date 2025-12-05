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
        public static (BaseMessage? msg, string? error) Parse(ReadOnlyMemory<byte> buffer)
        {
            OBCMsg? msg = null;
            string? error = null;

            var span = buffer.Span;

            var statusMsgLen = buffer.Length - 8;
            if (statusMsgLen != 16)
            {
                error = $"车载OBC的状态报文有用数据段长度:{statusMsgLen}不为16字节";
                return (msg, error);
            }

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(span[..2]);
            var repeat = span[2];
            var dest = span[4];
            var src = span[5];
            var partId = span[6];
            var msgId = span[7];

            msg = new OBCMsg
            {
                SequenceNumber = seq,
                RepeatCounter = repeat,
                Destination = dest,
                Source = src,
                PartId = partId,
                MsgId = msgId,
                UserData = buffer.Slice(9, 16),
            };

            return (msg, error);
        }
    }
}
