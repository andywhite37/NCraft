using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCraft.Util
{
    public static class ByteArrayExtensions
    {
        public static void MakeBigEndian(this byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
        }

        public static byte[] UnpackWordValues(this byte[] bytes)
        {
            // Each byte in the array corresponds to 2 4-bit values
            var words = new byte[bytes.Length * 2];

            for (int i = 0; i < bytes.Length; ++i)
            {
                var first = (byte)(0x0f & bytes[i]);
                var second = (byte)(0x0f & (bytes[i] >> 4));

                words[i] = first;
                words[i + 1] = second;
            }

            return words;
        }

        public static byte[] PackWordValues(this byte[] words)
        {
            var bytes = new byte[words.Length / 2];

            for (int i = 0; i < words.Length; i += 2)
            {
                var first = 0x0f & words[i];
                var second = 0xf0 & (words[i + 1] << 4);

                var packedByte = (byte)(first & second);

                bytes[i / 2] = packedByte;
            }

            return bytes;
        }
    }
}
