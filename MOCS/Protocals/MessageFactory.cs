using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Utils;

namespace MOCS.Protocals
{
    public static class MessageFactory
    {
        /// <summary>
        /// 报文帧头
        /// </summary>
        public static byte MsgHead { get; } = 0x02;

        /// <summary>
        /// 报文帧尾
        /// </summary>
        public static byte MsgTail { get; } = 0x03;

        /// <summary>
        /// 悬浮控制器数据帧1报文标识号
        /// </summary>
        public static byte LeviControllerStatusMsgAId { get; } = 0x81;

        /// <summary>
        /// 悬浮控制器数据帧2报文标识号
        /// </summary>
        public static byte LeviControllerStatusMsgBId { get; } = 0x82;

        /// <summary>
        /// 涡流定位测速系统状态报文标识符下界
        /// </summary>
        public static byte VSPSStatusMsgIdLow { get; } = 0xE1;

        /// <summary>
        /// 涡流定位测速系统状态报文标识符上界
        /// </summary>
        public static byte VSPSStatusMsgIdHigh { get; } = 0xEF;

        /// <summary>
        /// 车载OBC状态报文标识符下界
        /// </summary>
        public static byte OBCStatusMsgIdLow { get; } = 0xF1;

        /// <summary>
        /// 车载OBC状态报文标识符下界
        /// </summary>
        public static byte OBCStatusMsgIdHigh { get; } = 0xFF;

        //private static readonly Dictionary<
        //    byte,
        //    Func<ReadOnlySpan<byte>, (IIncomingMsg?, string?)>
        //> _mcuMsgParsers = [];

        /// <summary>
        /// 将发送报文类转换为字节数组
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(IOutgoingMsg msg)
        {
            var msgBody = msg.ToByteArray() ?? Array.Empty<byte>();

            var result = new byte[msgBody.Length + 2];
            result[0] = MsgHead;

            Buffer.BlockCopy(msgBody, 0, result, 1, msgBody.Length);

            result[result.Length - 1] = MsgTail;
            return result;
        }

        /// <summary>
        /// 尝试从原始字节缓冲区解析出消息实例。
        /// 自动校验报文帧头、帧尾、长度、CRC等。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool TryParseMessage(
            ReadOnlySpan<byte> buffer,
            [NotNullWhen(true)] out IIncomingMsg? message,
            out string? error
        )
        {
            message = null;
            error = null;
            if (buffer.Length < 10)
            {
                error = "报文长度过短";
                return false;
            }

            var head = buffer[0];
            var tail = buffer[^1];

            if (head != MsgHead | tail != MsgTail)
            {
                error = "报文帧头尾不符合协议规定";
                return false;
            }

            var msgBody = buffer[1..^1];

            var seq = BinaryPrimitives.ReadUInt16LittleEndian(msgBody.Slice(0, 2));
            var repeat = msgBody[2];
            var lenWords = msgBody[3];
            var dest = msgBody[4];
            var src = msgBody[5];
            var partId = msgBody[6];
            var msgId = msgBody[7];
            var receiveCrc = BinaryPrimitives.ReadUInt16LittleEndian(msgBody[^2..]);

            int expectedTotalBytes = lenWords * 2;
            if (buffer.Length - 2 != expectedTotalBytes)
            {
                error = "报文长度不匹配";
                return false;
            }

            int payloadLen = expectedTotalBytes - 10; // 8 header + 2 CRC
            if (payloadLen < 0 || payloadLen % 2 != 0)
            {
                error = "用户数据段长度有误";
                return false;
            }

            var calcCrc = CRC16CCITT.Compute(msgBody);
            if (calcCrc != receiveCrc)
            {
                error = "CRC校验失败";
                return false;
            }

            var payload = msgBody.Slice(8, payloadLen);

            return true;
        }
    }
}
