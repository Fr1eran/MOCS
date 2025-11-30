using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCS.Coms
{
    public interface IMessageFactory<TBaseMsg>
        where TBaseMsg : class
    {
        /// <summary>
        /// 尝试从原始报文解析出消息对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool TryParseMessage(
            ReadOnlyMemory<byte> buffer,
            [NotNullWhen(true)] out TBaseMsg? message,
            out string? error
        );

        /// <summary>
        /// 注册单个报文标识符对应的解析器
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="parser"></param>
        void RegisterParser(
            byte msgId,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        );

        /// <summary>
        /// 注册某报文标识符范围对应的解析器
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="parser"></param>
        void RegisterRangeParser(
            byte low,
            byte high,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        );

        /// <summary>
        /// 将报文主体封装为目标传输报文
        /// </summary>
        /// <param name="payLoad"></param>
        /// <returns></returns>
        byte[] ToTransmitByteArray(ReadOnlySpan<byte> payLoad);
    }
}
