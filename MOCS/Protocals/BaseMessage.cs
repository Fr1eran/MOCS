using NLog;

namespace MOCS.Protocals
{
    /// <summary>
    /// MOCS与MCU接口协议报文抽象基类
    /// </summary>
    /// <remarks>
    /// 规定了报文结构所要求的数据字段
    /// </remarks>
    public abstract class BaseMessage
    {
        /// <summary>
        /// 报文序列号
        /// </summary>
        public ushort SequenceNumber { get; set; }

        /// <summary>
        /// 重复计数器
        /// </summary>
        public byte RepeatCounter { get; set; } = 0x00;

        /// <summary>
        /// 目的地
        /// </summary>
        public byte Destination { get; set; } = 0x01;

        /// <summary>
        /// 发送方
        /// </summary>
        public byte Source { get; set; } = 0x01;

        /// <summary>
        /// 部件标识
        /// </summary>
        public byte PartId { get; set; } = 0x01;

        /// <summary>
        /// 报文标识
        /// </summary>
        public byte MsgId { get; set; }

        /// <summary>
        /// 用户数据段
        /// </summary>
        public virtual ReadOnlyMemory<byte> UserData { get; set; }

        /// <summary>
        /// 报文长度(word)
        /// </summary>
        /// <remarks>8字节报文帧固定数据段，用户数据段，2字节CRC</remarks>
        public byte LengthInWords => (byte)((8 + UserData.Length + 2) / 2);
    }
}
