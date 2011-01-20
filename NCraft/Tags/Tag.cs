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

        public Tag()
        {
        }

        public Tag(string name)
        {
            Name = name;
        }

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

        public Tag()
            : base()
        {
        }

        public Tag(string name)
            : base(name)
        {
        }

        public Tag(T value)
        {
            Value = value;
        }

        public Tag(string name, T value)
            : base(name)
        {
            Value = value;
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
            sb.AppendFormat(": {0}", Value);
            
            return sb.ToString();
        }
    }

    public abstract class TagArrayTag : Tag
    {
        public Tag[] Items { get; set; }
        public int Length { get; set; }

        public TagArrayTag()
            : base()
        {
        }

        public TagArrayTag(string name)
            : base(name)
        {
        }

        public TagArrayTag(Tag[] items)
        {
            Items = items;
        }

        public TagArrayTag(string name, Tag[] items)
            : base(name)
        {
            Items = items;
        }

        public T GetTag<T>(int i) where T : Tag
        {
            return (T)Items[i];
        }

        public void SetTag<T>(int i, T tag) where T : Tag
        {
            Items[i] = tag;
        }

        public byte GetByte(int i)
        {
            return GetTag<ByteTag>(i).Value;
        }

        public void SetByte(int i, byte value)
        {
            GetTag<ByteTag>(i).Value = value;
        }

        public int GetShort(int i)
        {
            return GetTag<ShortTag>(i).Value;
        }

        public void SetShort(int i, short value)
        {
            GetTag<ShortTag>(i).Value = value;
        }

        public int GetInt(int i)
        {
            return GetTag<IntTag>(i).Value;
        }

        public void SetInt(int i, int value)
        {
            GetTag<IntTag>(i).Value = value;
        }

        public long GetLong(int i)
        {
            return GetTag<LongTag>(i).Value;
        }

        public void SetLong(int i, long value)
        {
            GetTag<LongTag>(i).Value = value;
        }

        public float GetFloat(int i)
        {
            return GetTag<FloatTag>(i).Value;
        }

        public void SetFloat(int i, float value)
        {
            GetTag<FloatTag>(i).Value = value;
        }

        public double GetDouble(int i)
        {
            return GetTag<DoubleTag>(i).Value;
        }

        public void SetDouble(int i, double value)
        {
            GetTag<DoubleTag>(i).Value = value;
        }

        public string GetString(int i)
        {
            return GetTag<StringTag>(i).Value;
        }

        public void SetString(int i, string value)
        {
            GetTag<StringTag>(i).Value = value;
        }

        public ByteArrayTag GetByteArrayTag(int i)
        {
            return GetTag<ByteArrayTag>(i);
        }

        public void SetByteArrayTag(int i, ByteArrayTag tag)
        {
            SetTag<ByteArrayTag>(i, tag);
        }

        public ListTag GetListTag(int i)
        {
            return GetTag<ListTag>(i);
        }

        public void SetListTag(int i, ListTag tag)
        {
            SetTag<ListTag>(i, tag);
        }

        public CompoundTag GetCompoundTag(int i)
        {
            return GetTag<CompoundTag>(i);
        }

        public void SetCompoundTag(int i, CompoundTag tag)
        {
            SetTag<CompoundTag>(i, tag);
        }
    }

    public abstract class TagListTag : Tag
    {
        public List<Tag> Items { get; set; }

        public TagListTag()
            : base()
        {
        }

        public TagListTag(string name)
            : base(name)
        {
        }

        public TagListTag(List<Tag> items)
        {
            Items = items;
        }

        public TagListTag(string name, List<Tag> items)
            : base(name)
        {
            Items = items;
        }

        public int FindIndex(string name)
        {
            return Items.FindIndex(i => i.Name == name);
        }

        public Tag this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        public Tag this[string name]
        {
            get { return Items[FindIndex(name)]; }
            set { Items[FindIndex(name)] = value; }
        }

        private T GetTag<T>(string name) where T : Tag
        {
            return (T)this[name];
        }

        private void SetTag<T>(string name, T tag) where T : Tag
        {
            this[name] = tag;
        }

        public byte GetByte(string name)
        {
            return GetTag<ByteTag>(name).Value;
        }

        public void SetByte(string name, byte value)
        {
            GetTag<ByteTag>(name).Value = value;
        }

        public short GetShort(string name)
        {
            return GetTag<ShortTag>(name).Value;
        }

        public void SetShort(string name, short value)
        {
            GetTag<ShortTag>(name).Value = value;
        }

        public int GetInt(string name)
        {
            return GetTag<IntTag>(name).Value;
        }

        public void SetInt(string name, int value)
        {
            GetTag<IntTag>(name).Value = value;
        }

        public long GetLong(string name)
        {
            return GetTag<LongTag>(name).Value;
        }

        public void SetLong(string name, long value)
        {
            GetTag<LongTag>(name).Value = value;
        }

        public float GetFloat(string name)
        {
            return GetTag<FloatTag>(name).Value;
        }

        public void SetFloat(string name, float value)
        {
            GetTag<FloatTag>(name).Value = value;
        }

        public double GetDouble(string name)
        {
            return GetTag<DoubleTag>(name).Value;
        }

        public void SetDouble(string name, double value)
        {
            GetTag<DoubleTag>(name).Value = value;
        }

        public string GetString(string name)
        {
            return GetTag<StringTag>(name).Value;
        }

        public void SetString(string name, string value)
        {
            GetTag<StringTag>(name).Value = value;
        }

        public ByteArrayTag GetByteArrayTag(string name)
        {
            return GetTag<ByteArrayTag>(name);
        }

        public void SetByteArrayTag(string name, ByteArrayTag tag)
        {
            SetTag<ByteArrayTag>(name, tag);
        }

        public ListTag GetListTag(string name)
        {
            return GetTag<ListTag>(name);
        }

        public void SetListTag(string name, ListTag tag)
        {
            SetTag<ListTag>(name, tag);
        }

        public CompoundTag GetCompoundTag(string name)
        {
            return GetTag<CompoundTag>(name);
        }

        public void SetCompoundTag(string name, CompoundTag tag)
        {
            SetTag<CompoundTag>(name, tag);
        }

        public T GetTag<T>(int i) where T : Tag
        {
            return (T)Items[i];
        }

        public void SetTag<T>(int i, T tag) where T : Tag
        {
            Items[i] = tag;
        }

        public byte GetByte(int i)
        {
            return GetTag<ByteTag>(i).Value;
        }

        public void SetByte(int i, byte value)
        {
            GetTag<ByteTag>(i).Value = value;
        }

        public int GetShort(int i)
        {
            return GetTag<ShortTag>(i).Value;
        }

        public void SetShort(int i, short value)
        {
            GetTag<ShortTag>(i).Value = value;
        }

        public int GetInt(int i)
        {
            return GetTag<IntTag>(i).Value;
        }

        public void SetInt(int i, int value)
        {
            GetTag<IntTag>(i).Value = value;
        }

        public long GetLong(int i)
        {
            return GetTag<LongTag>(i).Value;
        }

        public void SetLong(int i, long value)
        {
            GetTag<LongTag>(i).Value = value;
        }

        public float GetFloat(int i)
        {
            return GetTag<FloatTag>(i).Value;
        }

        public void SetFloat(int i, float value)
        {
            GetTag<FloatTag>(i).Value = value;
        }

        public double GetDouble(int i)
        {
            return GetTag<DoubleTag>(i).Value;
        }

        public void SetDouble(int i, double value)
        {
            GetTag<DoubleTag>(i).Value = value;
        }

        public string GetString(int i)
        {
            return GetTag<StringTag>(i).Value;
        }

        public void SetString(int i, string value)
        {
            GetTag<StringTag>(i).Value = value;
        }

        public ByteArrayTag GetByteArrayTag(int i)
        {
            return GetTag<ByteArrayTag>(i);
        }

        public void SetByteArrayTag(int i, ByteArrayTag tag)
        {
            SetTag<ByteArrayTag>(i, tag);
        }

        public ListTag GetListTag(int i)
        {
            return GetTag<ListTag>(i);
        }

        public void SetListTag(int i, ListTag tag)
        {
            SetTag<ListTag>(i, tag);
        }

        public CompoundTag GetCompoundTag(int i)
        {
            return GetTag<CompoundTag>(i);
        }

        public void SetCompoundTag(int i, CompoundTag tag)
        {
            SetTag<CompoundTag>(i, tag);
        }
    }
}
