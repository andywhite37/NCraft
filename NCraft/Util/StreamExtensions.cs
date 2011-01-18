using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NCraft.Util
{
    public static class StreamExtensions
    {
        public static short ReadInt16(this Stream stream)
        {
            var bytes = ReadBytes(stream, 2);
            MakeBigEndian(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        public static int ReadInt32(this Stream stream)
        {
            var bytes = ReadBytes(stream, 4);
            MakeBigEndian(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static long ReadInt64(this Stream stream)
        {
            var bytes = ReadBytes(stream, 8);
            MakeBigEndian(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        public static float ReadFloat(this Stream stream)
        {
            var bytes = ReadBytes(stream, 4);
            MakeBigEndian(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public static double ReadDouble(this Stream stream)
        {
            var bytes = ReadBytes(stream, 8);
            MakeBigEndian(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public static string ReadString(this Stream stream)
        {
            var length = stream.ReadInt16();
            var bytes = ReadBytes(stream, length);
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] ReadBytes(this Stream stream, int length)
        {
            var buffer = new byte[length];
            var bytesRead = 0;

            while ((bytesRead += stream.Read(buffer, 0, length)) < length);

            return buffer;
        }

        public static void MakeBigEndian(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
        }
    }
}
