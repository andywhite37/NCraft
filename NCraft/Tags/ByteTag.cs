using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class ByteTag : Tag<byte>
    {
        public override byte Type { get { return TagType.Byte; } }

        public ByteTag()
            : base()
        {
        }

        public ByteTag(string name)
            : base(name)
        {
        }

        public ByteTag(byte value)
            : base(value)
        {
        }

        public ByteTag(string name, byte value)
            : base(name, value)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Value = (byte)stream.ReadByte();
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteByte(Value);
        }
    }
}
