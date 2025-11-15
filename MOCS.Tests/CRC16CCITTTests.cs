using MOCS.Utils;
using System.Text;

namespace MOCS.Tests
{
    public class CRC16CCITTTests
    {
        [Fact]
        public void Compute_ReturnsZero_OnEmptyInput()
        {
            ushort crc = CRC16CCITT.Compute(ReadOnlySpan<byte>.Empty);
            Assert.Equal((ushort)0x0000, crc);
        }

        [Fact]
        public void Compute_Kermit_123456789()
        {
            // CRC-16/KERMIT (poly 0x1021, init 0x0000, reflect in/out, xorout 0x0000)
            byte[] data = Encoding.ASCII.GetBytes("123456789");
            ushort crc = CRC16CCITT.Compute(data);
            Assert.Equal((ushort)0x2189, crc);
        }

        [Fact]
        public void Compute_ArrayAndSpan_Equivalent()
        {
            var arr = new byte[] { 0x10, 0x20, 0x30, 0x40 };
            ushort crcFromArray = CRC16CCITT.Compute(arr);
            ushort crcFromSpan = CRC16CCITT.Compute(arr.AsSpan());
            Assert.Equal(crcFromArray, crcFromSpan);
        }

        [Fact]
        public void Compute_DifferentInputs_ProduceDifferentCrcs()
        {
            var a = new byte[] { 0x01, 0x02 };
            var b = new byte[] { 0x02, 0x01 };
            Assert.NotEqual(CRC16CCITT.Compute(a), CRC16CCITT.Compute(b));
        }
    }
}