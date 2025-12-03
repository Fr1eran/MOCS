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
    public class VSPSStatusMsg : BaseMessage, IIncomingMsg<BaseMessage>
    {
        public static (BaseMessage? msg, string? error) Parse(ReadOnlyMemory<byte> buffer)
        {
            VSPSStatusMsg? msg = null;
            string? error = null;

            var span = buffer.Span;

            var CANID = span[8];
            SysWideLogger.Info($"收到VSPS状态报文, CANID: {CANID:X2}");
            if (CANID != 0X5F)
            {
                error = $"车载VSPS的CAN数据帧ID:{CANID:X2}与规定值0X5F不符";
                return (msg, error);
            }

            var statusMsgLen = buffer.Length - 10;
            if (statusMsgLen != 12)
            {
                error = $"车载VSPS的状态报文有用数据段长度:{statusMsgLen}不为12字节";
                return (msg, error);
            }

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(span[..2]);
            var repeat = span[2];
            var dest = span[4];
            var src = span[5];
            var partId = span[6];
            var msgId = span[7];

            msg = new VSPSStatusMsg
            {
                SequenceNumber = seq,
                RepeatCounter = repeat,
                Destination = dest,
                Source = src,
                PartId = partId,
                MsgId = msgId,
                UserData = buffer.Slice(9, 12),
            };

            return (msg, error);
        }
    }
}
