using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Tags;

namespace NCraft.Util
{
    public static class StreamReadExtensions
    {
        public static short ReadShort(this Stream stream)
        {
            var bytes = ReadBytes(stream, 2);
            bytes.MakeBigEndian();
            return BitConverter.ToInt16(bytes, 0);
        }

        public static int ReadInt(this Stream stream)
        {
            var bytes = ReadBytes(stream, 4);
            bytes.MakeBigEndian();
            return BitConverter.ToInt32(bytes, 0);
        }

        public static long ReadLong(this Stream stream)
        {
            var bytes = ReadBytes(stream, 8);
            bytes.MakeBigEndian();
            return BitConverter.ToInt64(bytes, 0);
        }

        public static float ReadFloat(this Stream stream)
        {
            var bytes = ReadBytes(stream, 4);
            bytes.MakeBigEndian();
            return BitConverter.ToSingle(bytes, 0);
        }

        public static double ReadDouble(this Stream stream)
        {
            var bytes = ReadBytes(stream, 8);
            bytes.MakeBigEndian();
            return BitConverter.ToDouble(bytes, 0);
        }

        public static string ReadString(this Stream stream)
        {
            var length = stream.ReadShort();

            var bytes = ReadBytes(stream, length);
            return Encoding.UTF8.GetString(bytes);
        }

        public static Tag ReadTag(this Stream stream)
        {
            return stream.ReadTag(true);
        }

        public static Tag ReadTag(this Stream stream, bool readName)
        {
            var tagType = (byte)stream.ReadByte();

            Tag tag = TagType.CreateTag(tagType);

            tag.ReadFrom(stream, readName);
            return tag;
        }

        public static byte[] ReadBytes(this Stream stream, int length)
        {
            var buffer = new byte[length];
            var bytesRead = 0;

            while ((bytesRead += stream.Read(buffer, 0, length)) < length);

            return buffer;
        }   
    }
}
