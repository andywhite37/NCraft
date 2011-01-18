﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public class ListTag : Tag<List<Tag>>
    {
        public override byte Type { get { return TagType.List; } }

        public byte ItemType { get; set; }
        public int Length { get; set; }

        public ListTag()
        {
            Value = new List<Tag>();
        }

        public override void ReadFrom(Stream stream, bool readName)
        {
            if (readName)
            {
                Name = stream.ReadString();
            }

            ItemType = (byte)stream.ReadByte();
            Length = stream.ReadInt32();

            var serializer = new TagSerializer();
            for (int i = 0; i < Length; ++i)
            {
                var tag = TagType.CreateTag(ItemType);
                tag.ReadFrom(stream, false);
                Value.Add(tag);
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
            foreach (var item in Value)
            {
                sb.AppendLine(item.ToString(indent + "    "));
            }
            sb.Append(indent + "}");

            return sb.ToString();
        }
    }
}