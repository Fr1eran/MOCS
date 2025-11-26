using System.Diagnostics.CodeAnalysis;

namespace MOCS.Coms
{
    public interface IIncomingMsg<TBasedMsg>
        where TBasedMsg : class
    {
        /// <summary>
        /// 尝试从原始字节缓冲区解析出消息实例。
        /// </summary>
        /// <param name="buffer">原始字节数据</param>
        /// <param name="msg">成功时返回消息实例</param>
        /// <param name="error">失败时返回错误描述</param>
        /// <returns>是否解析成功</returns>
        static abstract bool TryParse(
            ReadOnlyMemory<byte> buffer,
            [NotNullWhen(true)] out TBasedMsg? msg,
            out string? error
        );
    }
}
