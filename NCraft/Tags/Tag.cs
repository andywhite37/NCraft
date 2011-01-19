using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    public abstract class Tag
    {
        public abstract byte Type { get; }
        public string TypeName { get { return TagType.GetName(Type); } }
        public string Name { get; set; }

        public void ReadFrom(Stream stream)
        {
            ReadFrom(stream, true);
        }

        public virtual void ReadFrom(Stream stream, bool readName)
        {
            if (readName)
            {
                Name = stream.ReadString();
            }
        }

        public void WriteTo(Stream stream)
        {
            WriteTo(stream, true, true);
        }

        public virtual void WriteTo(Stream stream, bool writeType, bool writeName)
        {
            if (writeType)
            {
                stream.WriteByte(Type);
            }

            if (writeName)
            {
                stream.WriteString(Name);
            }
        }

        public override string ToString()
        {
            return ToString("");
        }

        public abstract string ToString(string indent);
    }

    public abstract class Tag<T> : Tag
    {
        public T Value { get; set; }

        public override string ToString(string indent)
        {
            var sb = new StringBuilder();
            sb.Append(indent);
            sb.Append(TypeName);
            if (!string.IsNullOrEmpty(Name))
            {
                sb.AppendFormat("(\"{0}\")", Name);
            }
            sb.AppendFormat(": {0}", Value);
            
            return sb.ToString();
        }
    }

    public abstract class ArrayTag<T> : Tag
    {
        public T[] Items { get; set; }
        public int Length { get; set; }

        public T Get(int i)
        {
            return Items[i];
        }
    }

    public abstract class ListTag<T> : Tag where T : Tag
    {
        public List<T> Items { get; set; }

        private TTag GetTag<TTag>(int i) where TTag : Tag
        {
            return Items[i] as TTag;
        }

        private TTag GetTag<TTag>(string name) where TTag : Tag
        {
            return Items.Single(t => t.Name == name) as TTag;
        }

        public byte GetByte(string name)
        {
            return GetTag<ByteTag>(name).Value;
        }

        public short GetShort(string name)
        {
            return GetTag<ShortTag>(name).Value;
        }

        public int GetInt(string name)
        {
            return GetTag<IntTag>(name).Value;
        }

        public long GetLong(string name)
        {
            return GetTag<LongTag>(name).Value;
        }

        public float GetFloat(string name)
        {
            return GetTag<FloatTag>(name).Value;
        }

        public double GetDouble(string name)
        {
            return GetTag<DoubleTag>(name).Value;
        }

        public string GetString(string name)
        {
            return GetTag<StringTag>(name).Value;
        }

        public ByteArrayTag GetByteArrayTag(string name)
        {
            return GetTag<ByteArrayTag>(name);
        }

        public ListTag GetListTag(string name)
        {
            return GetTag<ListTag>(name);
        }

        public CompoundTag GetCompoundTag(string name)
        {
            return GetTag<CompoundTag>(name);
        }
    }
}
