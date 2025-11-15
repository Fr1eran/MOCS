namespace MOCS.Utils
{
    /// <summary>
    /// 采用CCITT-16，生成多项式m(x)= x16+x12+x5+1（即0x1021），
    /// 初始值0x0000，结果异或值0x0000，输入数值反转，输出数值反转。
    /// </summary>
    public static class CRC16CCITT
    {
        // 使用 x^16 + x^12 + x^5 + 1 多项式反转后的 0x8408 计算得到
        private static readonly ushort[] Table = new ushort[256];

        static CRC16CCITT()
        {
            const ushort polynomial = 0x8408;

            for (ushort i = 0; i < Table.Length; i++)
            {
                ushort crc = i;
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                        crc = (ushort)((crc >> 1) ^ polynomial);
                    else
                        crc >>= 1;
                }
                Table[i] = crc;
            }
        }

        public static ushort Compute(ReadOnlySpan<byte> data)
        {
            ushort crc = 0x0000;
            foreach (byte b in data)
            {
                byte idx = (byte)(crc ^ b);
                crc = (ushort)((crc >> 8) ^ Table[idx]);
            }
            return crc;
        }

        public static ushort Compute(byte[] data) => Compute(data.AsSpan());
    }
}