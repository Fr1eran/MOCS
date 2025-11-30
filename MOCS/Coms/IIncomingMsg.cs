using System.Diagnostics.CodeAnalysis;

namespace MOCS.Coms
{
    public interface IIncomingMsg<TBasedMsg>
        where TBasedMsg : class
    {
        /// <summary>
        /// 尝试从原始字节缓冲区解析出消息实例。
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>成功时返回消息实例，失败时返回错误信息</returns>
        static abstract (TBasedMsg? msg, string? error) Parse(ReadOnlyMemory<byte> buffer);
    }
}
