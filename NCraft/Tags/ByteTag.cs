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

        public override void ReadFrom(Stream stream, bool readName)
        {
            if (readName)
            {
                Name = stream.ReadString();
            }

            Value = (byte)stream.ReadByte();
        }
    }
}
