using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;

namespace MOCS.Protocals.VehicleControl.VehicleToMOCS
{
    public class EMSStatusMsgB : BaseMessage, IIncomingMsg<BaseMessage>
    {
        public static (BaseMessage? msg, string? error) Parse(ReadOnlyMemory<byte> buffer)
        {
            EMSStatusMsgB? msg = null;
            string? error = null;

            var span = buffer.Span;
            var CANID = span[8];
            if (CANID < 0X61 | CANID > 0X74)
            {
                error = $"悬浮控制器的2类CAN数据帧ID:{CANID:X2}与规定目标范围0X61~0X74不符";
                return (msg, error);
            }

            var statusMsgLen = buffer.Length - 10;
            if (statusMsgLen != 8)
            {
                error = $"悬浮控制器的CAN数据帧长度:{statusMsgLen}不为8字节";
                return (msg, error);
            }

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(span[..2]);
            var repeat = span[2];
            var dest = span[4];
            var src = span[5];
            var partId = span[6];
            var msgId = span[7];

            msg = new EMSStatusMsgB
            {
                SequenceNumber = seq,
                RepeatCounter = repeat,
                Destination = dest,
                Source = src,
                PartId = partId,
                MsgId = msgId,
                UserData = buffer.Slice(9, 8),
            };

            return (msg, error);
        }
    }
}
