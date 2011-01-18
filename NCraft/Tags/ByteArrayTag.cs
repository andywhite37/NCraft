using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class ByteArrayTag : Tag<byte[]>
    {
        public override byte Type { get { return TagType.ByteArray; } }
        public int Length { get; set; }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Length = stream.ReadInt();

            Value = new byte[Length];

            for (int i = 0; i < Length; ++i)
            {
                Value[i] = (byte)stream.ReadByte();
            }
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteInt(Length);

            for (int i = 0; i < Length; ++i)
            {
                stream.WriteByte(Value[i]);
            }
        }

        public override string ToString(string indent)
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(TypeName);
            if (!string.IsNullOrEmpty(Name))
            {
                sb.AppendFormat("(\"{0}\")", Name);
            }
            sb.AppendFormat(": [{0} bytes]", Length);

            return sb.ToString();
        }
    }
}
