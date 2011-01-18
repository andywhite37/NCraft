using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NCraft.Tags
{
    public class EndTag : Tag
    {
        public override byte Type { get { return TagType.End; } }

        public override void ReadFrom(Stream stream, bool readName)
        {
            // No-op
        }

        public override string ToString(string indent)
        {
            throw new InvalidOperationException("Cannot convert EndTag to string.");
        }
    }
}
