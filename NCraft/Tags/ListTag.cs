using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    /// <summary>
    /// Represents a TAG_List.
    /// Note: this inherits from TagArrayTag rather than TagListTag because the way it's stored
    /// in an NBT file is actually more array-like than list-like (length is fixed, etc.)
    /// </summary>
    public class ListTag : TagArrayTag
    {
        public override byte Type { get { return TagType.List; } }
        public byte ItemType { get; set; }

        public ListTag()
            : base()
        {
        }

        public ListTag(string name)
            : base(name)
        {
        }

        public ListTag(Tag[] items)
            : base(items)
        {
        }

        public ListTag(string name, Tag[] items)
            : base(name, items)
        {
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            base.ReadFrom(stream, readName);

            ItemType = (byte)stream.ReadByte();
            Length = stream.ReadInt();
            Items = new Tag[Length];

            for (int i = 0; i < Length; ++i)
            {
                var tag = TagType.CreateTag(ItemType);
                tag.ReadFrom(stream, false);
                Items[i] = tag;
            }
        }

        public override void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            base.WriteTo(stream, writeType, writeName);

            stream.WriteByte(ItemType);
            stream.WriteInt(Length);

            for (int i = 0; i < Length; ++i)
            {
                Items[i].WriteTo(stream, false, false);
            }
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
            sb.AppendFormat(": {0} entries of type {1}{2}", Length, TagType.GetName(ItemType), Environment.NewLine);
            sb.AppendLine(indent + "{");
            foreach (var item in Items)
            {
                sb.AppendLine(item.ToString(indent + "    "));
            }
            sb.Append(indent + "}");

            return sb.ToString();
        }
    }
}
