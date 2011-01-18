using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using NCraft.Tags;

namespace NCraft.Tags
{
    public class TagSerializer
    {
        public void Serialize(Tag tag)
        {
        }

        public Tag Deserialize(Stream stream)
        {
            return Deserialize(stream, true);
        }

        public Tag Deserialize(Stream stream, bool readName)
        {
            var tagType = (byte)stream.ReadByte();

            Tag tag = TagType.CreateTag(tagType);

            tag.ReadFrom(stream, readName);
            return tag;
        }
    }
}
