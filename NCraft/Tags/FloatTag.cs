using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class FloatTag : ValueTag<float>
    {
        public override byte Type { get { return TagType.Float; } }

        public FloatTag()
            : base()
        {
        }
        
        public FloatTag(string name)
            : base(name)
        {
        }

        public FloatTag(float value)
            : base(value)
        {
        }

        public FloatTag(string name, float value)
            : base(name, value)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Value = stream.ReadFloat();
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteFloat(Value);
        }
    }
}
