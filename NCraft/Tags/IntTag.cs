using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class IntTag : Tag<Int32>
    {
        public override byte Type { get { return TagType.Int; } }

        public override void ReadFrom(Stream stream, bool readName)
        {
            if (readName)
            {
                Name = stream.ReadString();
            }

            Value = stream.ReadInt32();
        }
    }
}
