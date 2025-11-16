namespace MOCS.Protocals
{
    internal interface IOutgoingMsg
    {
        /// <summary>
        /// 将当前消息序列化为符合协议的字节数组
        /// </summary>
        /// <returns>序列化后的字节数组</returns>
        byte[] ToByteArray();
    }
}