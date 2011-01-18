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

    public static class StreamWriteExtensions
    {
        public static void WriteShort(this Stream stream, short value)
        {
            var bytes = BitConverter.GetBytes(value);
            bytes.MakeBigEndian();
            stream.WriteBytes(bytes);
        }

        public static void WriteInt(this Stream stream, int value)
        {
            var bytes = BitConverter.GetBytes(value);
            bytes.MakeBigEndian();
            stream.WriteBytes(bytes);
        }

        public static void WriteLong(this Stream stream, long value)
        {
            var bytes = BitConverter.GetBytes(value);
            bytes.MakeBigEndian();
            stream.WriteBytes(bytes);
        }

        public static void WriteFloat(this Stream stream, float value)
        {
            var bytes = BitConverter.GetBytes(value);
            bytes.MakeBigEndian();
            stream.WriteBytes(bytes);
        }

        public static void WriteDouble(this Stream stream, double value)
        {
            var bytes = BitConverter.GetBytes(value);
            bytes.MakeBigEndian();
            stream.WriteBytes(bytes);
        }

        public static void WriteString(this Stream stream, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            
            stream.WriteShort((short)bytes.Length);
            stream.WriteBytes(bytes);
        }

        public static void WriteTag(this Stream stream, Tag tag)
        {
            stream.WriteTag(tag, true, true);
        }

        public static void WriteTag(this Stream stream, Tag tag, bool writeType, bool writeName)
        {
            tag.WriteTo(stream, writeType, writeName);
        }
        
        public static void WriteBytes(this Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    public static class ByteArrayExtensions
    {
        public static void MakeBigEndian(this byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
        }
    }
}
