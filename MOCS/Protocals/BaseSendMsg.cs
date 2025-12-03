using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Utils;

namespace MOCS.Protocals
{
    /// <summary>
    /// MOCS发往MCU的报文基类
    /// </summary>
    public class BaseSendMsg : BaseMessage, IOutgoingMsg
    {
        public byte[] ToByteArray()
        {
            int payloadLength = UserData.Length;
            if (payloadLength % 2 != 0)
            {
                throw new InvalidOperationException("用户数据段长度必须为偶数！");
            }
            int totalLength = 8 + payloadLength;
            byte[] buffer = new byte[totalLength];
            var span = buffer.AsSpan();

            // 写入数据
            BinaryPrimitives.WriteUInt16LittleEndian(span, SequenceNumber);
            span[2] = RepeatCounter;
            span[3] = LengthInWords;
            span[4] = Destination;
            span[5] = Source;
            span[6] = PartId;
            span[7] = MsgId;
            UserData.Span.CopyTo(span[8..]);

            return buffer;
        }
    }
}
