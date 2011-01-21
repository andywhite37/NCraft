using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NCraft.Util;

namespace NCraft.Tags
{
    public class ByteArrayTag : Tag
    {
        public override byte Type { get { return TagType.ByteArray; } }
        public int Length { get; set; }
        public byte[] Items { get; set; }

        public ByteArrayTag()
            : base()
        {
        }

        public ByteArrayTag(string name)
            : base(name)
        {
        }

        public ByteArrayTag(byte[] items)
            : base()
        {
            Length = items.Length;
            Items = items;
        }

        public ByteArrayTag(string name, byte[] items)
            : base(name)
        {
            Length = items.Length;
            Items = items;
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Length = stream.ReadInt();

            Items = new byte[Length];

            for (int i = 0; i < Length; ++i)
            {
                Items[i] = (byte)stream.ReadByte();
            }
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteInt(Length);

            for (int i = 0; i < Length; ++i)
            {
                stream.WriteByte(Items[i]);
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
