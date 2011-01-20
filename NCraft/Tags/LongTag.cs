using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class LongTag : Tag<Int64>
    {
        public override byte Type { get { return TagType.Long; } }

        public LongTag()
            : base()
        {
        }

        public LongTag(string name)
            : base(name)
        {
        }

        public LongTag(long value)
            : base(value)
        {
        }

        public LongTag(string name, long value)
            : base(name, value)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Value = stream.ReadLong();
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteLong(Value);
        }
    }
}
