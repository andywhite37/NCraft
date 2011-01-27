using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Tags;

namespace NCraft.Util
{
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
}
