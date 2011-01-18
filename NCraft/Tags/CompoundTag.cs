using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class CompoundTag : Tag<List<Tag>>
    {
        public override byte Type { get { return TagType.Compound; } }

        public CompoundTag()
        {
            Value = new List<Tag>();
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            while (true)
            {
                var tag = stream.ReadTag();

                if (tag.Type == TagType.End)
                {
                    break;
                }

                Value.Add(tag);
            }
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            foreach (var tag in Value)
            {
                tag.WriteTo(stream);
            }

            var endTag = TagType.CreateTag(TagType.End);
            endTag.WriteTo(stream, true, false);
        }

        public override string ToString(string indent)
        {
            var sb = new StringBuilder();

            sb.Append(indent);
            sb.Append(TypeName);
            if (!string.IsNullOrEmpty(Name))
            {
                sb.AppendFormat("(\"{0}\")", Name);
            }
            sb.AppendFormat(": {0} entries{1}", Value.Count, Environment.NewLine);
            sb.AppendLine(indent + "{");
            foreach (var item in Value)
            {
                sb.AppendLine(item.ToString(indent + "    "));
            }
            sb.Append(indent + "}");

            return sb.ToString();
        }
    }
}
