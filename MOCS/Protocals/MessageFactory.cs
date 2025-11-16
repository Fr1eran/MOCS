using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static readonly Dictionary<byte, Func<ReadOnlySpan<byte>, (IIncomingMsg?, string?)>> _parsers =
        {
        };

        /// <summary>
        /// 尝试从原始字节缓冲区解析出消息实例。
        /// 自动校验报文帧头、帧尾、长度、CRC等。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool TryParseMessage(ReadOnlySpan<byte> buffer, [NotNullWhen(true)] out IIncomingMsg? message, out string? error)
        {
        }
    }