﻿using System;
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

        public override void ReadFrom(Stream stream, bool readName)
        {
            if (readName)
            {
                Name = stream.ReadString();
            }
            Value = stream.ReadInt64();
        }
    }
}