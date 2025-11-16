namespace MOCS.Protocals.Propulsion
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
        public byte Destination { get; set; }

        /// <summary>
        /// 发送方
        /// </summary>
        public byte Source { get; set; }

        /// <summary>
        /// 部件标识
        /// </summary>
        public byte PartId { get; set; }

        /// <summary>
        /// 报文标识
        /// </summary>
        public abstract byte MsgId { get; set; }

        /// <summary>
        /// 用户数据段
        /// </summary>
        public virtual ReadOnlyMemory<byte> Payload { get; set; }

        /// <summary>
        /// 报文长度(word)
        /// </summary>
        /// <remarks>8字节报文帧固定数据段，用户数据段，2字节CRC</remarks>
        public byte LengthInWords => (byte)((8 + Payload.Length + 2) / 2);

        //public static void WriteLittleEndian(BinaryWriter writer, ushort value)
        //{
        //    writer.Write((byte)(value & 0xFF));
        //    writer.Write((byte)(value >> 8 & 0xFF));
        //}

        //public virtual byte[] ToByteArray()
        //{
        //    var stream = new MemoryStream();
        //    using (var writer = new BinaryWriter(stream, Encoding.Latin1, true))
        //    {
        //        WriteLittleEndian(writer, SequenceNumber);
        //        writer.Write(RepeatCounter);
        //        writer.Write(LengthInWords);
        //        writer.Write(Destination);
        //        writer.Write(Source);
        //        writer.Write(PartId);
        //        writer.Write(MsgId);
        //        writer.Write(Payload);

        //        var dataForCrc = stream.ToArray();
        //        var crc = CRC16CCITT.Compute(dataForCrc);
        //        WriteLittleEndian(writer, crc);
        //    }
        //    return stream.ToArray();
        //}

        //public static bool TryParse(byte[] buffer, out BaseMessage? msg, out string? err)
        //{
        //    // 此处用于解析报文
        //    // 校验报文帧头帧尾、长度、CRC、报文ID等

        //    msg = null;
        //    err = null;
        //    if (buffer.Length < 10)
        //    {
        //        err = "Too short";
        //        return false;
        //    }

        //    using var reader = new BinaryReader(new MemoryStream(buffer), Encoding.Latin1, false);

        //    var head = reader.ReadByte();
        //    var seq = reader.ReadUInt16();
        //    var repeat = reader.ReadByte();
        //    var lenWords = reader.ReadByte();
        //    var dest = reader.ReadByte();
        //    var src = reader.ReadByte();
        //    var partId = reader.ReadByte();
        //    var msgId = reader.ReadByte();

        //    int expectedTotalBytes = lenWords * 2;
        //    if (buffer.Length - 2 != expectedTotalBytes)
        //    {
        //        err = "Length mismatch";
        //        return false;
        //    }

        //    int payloadLen = expectedTotalBytes - 12;   // 8 header + 2 CRC + 2 head and tail
        //    if (payloadLen < 0 || payloadLen % 2 != 0)
        //    {
        //        err = "Invalid payload length";
        //        return false;
        //    }

        //    var payload = reader.ReadBytes(payloadLen);
        //    var receiveCrc = reader.ReadUInt16();

        //    var mainMsg = buffer[1..^3];
        //    var calcCrc = CRC16CCITT.Compute(mainMsg);
        //    if (calcCrc != receiveCrc)
        //    {
        //        err = "CRC error";
        //        return false;
        //    }

        //    //msg = new BaseMsg
        //    //{
        //    //    SequenceNumber = seq,
        //    //    RepeatCounter = repeat,
        //    //    Destination = dest,
        //    //    Source = src,
        //    //    PartId = partId,
        //    //    MessageId = msgId,
        //    //    Payload = payload,
        //    //};
        //    return true;
        //}
    }
}