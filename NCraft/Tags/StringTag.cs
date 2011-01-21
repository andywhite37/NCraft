using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class StringTag : ValueTag<string>
    {
        public override byte Type { get { return TagType.String; } }

        public StringTag()
            : base()
        {
        }

        public StringTag(string name)
            : base(name)
        {
        }

        public StringTag(string name, string value)
            : base(name, value)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Value = stream.ReadString();
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteString(Value);
        }
    }
}
