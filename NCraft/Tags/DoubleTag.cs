using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class DoubleTag : Tag<double>
    {
        public override byte Type { get { return TagType.Double; } }

        public DoubleTag()
            : base()
        {
        }

        public DoubleTag(string name)
            : base(name)
        {
        }

        public DoubleTag(double value)
            : base(value)
        {
        }

        public DoubleTag(string name, double value)
            : base(name, value)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Value = stream.ReadDouble();
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteDouble(Value);
        }
    }
}
