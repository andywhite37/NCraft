using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class ShortTag : ValueTag<short>
    {
        public override byte Type { get { return TagType.Short; } }

        public ShortTag()
            : base()
        {
        }

        public ShortTag(string name)
            : base(name)
        {
        }

        public ShortTag(short value)
            : base(value)
        {
        }

        public ShortTag(string name, short value)
            : base(name, value)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Value = stream.ReadShort();
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteShort(Value);
        }
    }
}
