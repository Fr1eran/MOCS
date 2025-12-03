using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;

namespace MOCS.Protocals.VehicleControl.VehicleToMOCS
{
    public class EMSStatusMsgA : BaseMessage, IIncomingMsg<BaseMessage>
    {
        public static (BaseMessage? msg, string? error) Parse(ReadOnlyMemory<byte> buffer)
        {
            EMSStatusMsgA? msg = null;
            string? error = null;

            var span = buffer.Span;
            var CANID = span[8];
            SysWideLogger.Info($"收到EMS控制器第一类状态报文, CANID: {CANID:X2}");
            if (CANID < 0X21 | CANID > 0X34)
            {
                error = $"悬浮控制器的1类CAN数据帧ID:{CANID:X2}与规定目标范围0x21~0x34不符";
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

            msg = new EMSStatusMsgA
            {
                SequenceNumber = seq,
                RepeatCounter = repeat,
                Destination = dest,
                Source = src,
                PartId = partId,
                MsgId = msgId,
                UserData = buffer.Slice(8, 9),
            };

            return (msg, error);
        }
    }
}
