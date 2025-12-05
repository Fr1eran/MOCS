using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCS.Coms;
using MOCS.Utils;

namespace MOCS.Protocals
{
    public sealed class MessageFactory<TBaseMsg> : IMessageFactory<TBaseMsg>
        where TBaseMsg : class
    {
        private readonly byte _msgHead;
        private readonly byte _msgTail;

        /// <summary>
        /// 报文标识符唯一时的解析器
        /// </summary>
        private readonly Dictionary<
            byte,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)>
        > _parsers = [];

        /// <summary>
        /// 报文标识符不唯一且在一定范围内的解析器
        /// </summary>
        private readonly List<(
            byte low,
            byte high,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        )> _rangeParsers = [];

        public MessageFactory(byte? msgHead = null, byte? msgTail = null)
        {
            _msgHead = msgHead ?? 0x02;
            _msgTail = msgTail ?? 0x03;
        }

        public byte[] ToTransmitByteArray(ReadOnlySpan<byte> payLoad)
        {
            int totalBytes = 1 + payLoad.Length + 2 + 1;
            byte[] result = new byte[totalBytes];

            result[0] = _msgHead;

            payLoad.CopyTo(result.AsSpan(1, payLoad.Length));

            ushort crc = CRC16CCITT.Compute(payLoad);
            BinaryPrimitives.WriteUInt16LittleEndian(result.AsSpan(1 + payLoad.Length, 2), crc);

            result[^1] = _msgTail;

            return result;
        }

        /// <summary>
        /// 注册单个报文标识符对应的解析器
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="parser"></param>
        public void RegisterParser(
            byte msgId,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        )
        {
            ArgumentNullException.ThrowIfNull(parser);
            _parsers[msgId] = parser;
        }

        /// <summary>
        /// 注册某报文标识范围对应的解析器
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="parser"></param>
        /// <exception cref="ArgumentException"></exception>
        public void RegisterRangeParser(
            byte low,
            byte high,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        )
        {
            ArgumentNullException.ThrowIfNull(parser);
            if (low > high)
                throw new ArgumentException($"low:{low} must <= high:{high}", nameof(low));
            _rangeParsers.Add((low, high, parser));
        }

        /// <summary>
        /// 尝试从原始字节缓冲区解析出消息实例。
        /// 自动校验报文帧头、帧尾、长度、CRC等。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool TryParseMessage(
            ReadOnlyMemory<byte> buffer,
            [NotNullWhen(true)] out TBaseMsg? message,
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

            var span = buffer.Span;
            var head = span[0];
            var tail = span[^1];

            if (head != _msgHead || tail != _msgTail)
            {
                error = "报文帧头尾字节不符合协议规定";
                return false;
            }

            var lenWords = span[4];
            int expectedTotalBytes = lenWords * 2;
            if (span.Length - 2 != expectedTotalBytes)
            {
                error = "报文长度不匹配";
                return false;
            }

            int userdataLen = expectedTotalBytes - 10; // 8 header + 2 CRC
            if (userdataLen < 0 || userdataLen % 2 != 0)
            {
                error = "用户数据段长度有误";
                return false;
            }

            var receiveCrc = BinaryPrimitives.ReadUInt16LittleEndian(
                span.Slice(1 + expectedTotalBytes - 2, 2)
            );

            // 提取报文主体传递给对应报文类型的解析器
            var msgBodyMemory = buffer.Slice(1, expectedTotalBytes - 2);
            var msgBodySpan = msgBodyMemory.Span;

            // 计算 CRC（仅对不含 CRC 的消息体）
            var calcCrc = CRC16CCITT.Compute(msgBodySpan);
            if (calcCrc != receiveCrc)
            {
                error = $"CRC校验失败 期望CRC: {calcCrc:X2} 实际CRC: {receiveCrc:X2}";
                return false;
            }

            var msgId = span[8]; // 读取报文标识符

            // 尝试找到解析器：优先通过匹配精确报文标识符
            if (_parsers.TryGetValue(msgId, out var exactParser))
            {
                try
                {
                    var (parsed, err) = exactParser(msgBodyMemory);
                    if (parsed is null)
                    {
                        error = err ?? "解析器返回空实例";
                        return false;
                    }

                    message = parsed;
                    return true;
                }
                catch (Exception ex)
                {
                    error = $"解析器抛出异常: {ex.Message}";
                    return false;
                }
            }
            // 如果没匹配到，则通过范围确定对应的解析器
            foreach (var (low, high, rangeparser) in _rangeParsers)
            {
                if (msgId >= low && msgId <= high)
                {
                    try
                    {
                        var (parsed, err) = rangeparser(msgBodyMemory);
                        if (parsed is null)
                        {
                            error = err ?? "范围解析器返回空实例";
                            return false;
                        }

                        message = parsed;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = $"范围解析器抛出异常: {ex.Message}";
                        return false;
                    }
                }
            }

            error = $"未注册解析器，msgId=0x{msgId:X2}";
            return false;
        }
    }
}
