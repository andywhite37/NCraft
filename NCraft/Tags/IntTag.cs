using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class IntTag : ValueTag<int>
    {
        public override byte Type { get { return TagType.Int; } }

        public IntTag()
            : base()
        {
        }
        
        public IntTag(string name)
            : base(name)
        {
        }

        public IntTag(int value)
            : base(value)
        {
        }

        public IntTag(string name, int value)
            : base(name, value)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            Value = stream.ReadInt();
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteInt(Value);
        }
    }
}
