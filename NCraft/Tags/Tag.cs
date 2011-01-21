using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using NCraft.Util;

namespace NCraft.Tags
{
    /// <summary>
    /// Base class for all TAG types
    /// </summary>
    public abstract class Tag
    {
        /// <summary>
        /// Type ID of Tag
        /// </summary>
        public abstract byte Type { get; }

        /// <summary>
        /// Type Name of Tag
        /// </summary>
        public string TypeName { get { return TagType.GetName(Type); } }

        /// <summary>
        /// Name of Tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Tag()
        {
        }

        /// <summary>
        /// Construct with Name
        /// </summary>
        /// <param name="name"></param>
        public Tag(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Base method to read Tag from a Stream, including the name.
        /// </summary>
        /// <param name="stream"></param>
        public void ReadFrom(Stream stream)
        {
            ReadFrom(stream, true);
        }

        /// <summary>
        /// Base method to read this Tag from a Stream, with the option of reading the name.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="readName"></param>
        public virtual void ReadFrom(Stream stream, bool readName)
        {
            if (readName)
            {
                Name = stream.ReadString();
            }
        }

        /// <summary>
        /// Base method to write this Tag to a Stream, including the Type and Name.
        /// </summary>
        /// <param name="stream"></param>
        public void WriteTo(Stream stream)
        {
            WriteTo(stream, true, true);
        }

        /// <summary>
        /// Base method to write this Tag to a Stream, with the option of writing the Type and Name
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="writeType"></param>
        /// <param name="writeName"></param>
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

        /// <summary>
        /// Converts this Tag to a String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString("");
        }

        /// <summary>
        /// Converts this Tag to a String, indented by indent
        /// </summary>
        /// <returns></returns>
        public abstract string ToString(string indent);
    }

    /// <summary>
    /// Base class for a tag that contains a single value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueTag<T> : Tag
    {
        /// <summary>
        /// The "payload" value of this Tag.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ValueTag()
            : base()
        {
        }

        /// <summary>
        /// Construct with Name
        /// </summary>
        /// <param name="name"></param>
        public ValueTag(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Construct with Value
        /// </summary>
        /// <param name="value"></param>
        public ValueTag(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Construct with Name and Value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public ValueTag(string name, T value)
            : base(name)
        {
            Value = value;
        }

        /// <summary>
        /// Converts this Tag to an indented string
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
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

    /// <summary>
    /// Base class for a Tag that contains an Array of Tags
    /// </summary>
    public abstract class TagArrayTag : Tag
    {
        /// <summary>
        /// Array of Tags contained by this Tag
        /// </summary>
        public Tag[] Items { get; set; }

        /// <summary>
        /// Length of the Tag Array
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TagArrayTag()
            : base()
        {
        }

        /// <summary>
        /// Construct with Name
        /// </summary>
        /// <param name="name"></param>
        public TagArrayTag(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Construct with Items
        /// </summary>
        /// <param name="items"></param>
        public TagArrayTag(Tag[] items)
        {
            Items = items;
        }

        /// <summary>
        /// Construct with Name and Items
        /// </summary>
        /// <param name="name"></param>
        /// <param name="items"></param>
        public TagArrayTag(string name, Tag[] items)
            : base(name)
        {
            Items = items;
        }

        /// <summary>
        /// Gets the Tag at index i, cast as type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i"></param>
        /// <returns></returns>
        public T GetTag<T>(int i) where T : Tag
        {
            return (T)Items[i];
        }

        /// <summary>
        /// Sets the specified Tag at index i
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i"></param>
        /// <param name="tag"></param>
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

    /// <summary>
    /// Base class for a Tag that contains a List of Tags
    /// </summary>
    public abstract class TagListTag : Tag
    {
        /// <summary>
        /// List of Tags contained by this Tag
        /// </summary>
        public List<Tag> Items { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TagListTag()
            : base()
        {
        }

        /// <summary>
        /// Construct with Name
        /// </summary>
        /// <param name="name"></param>
        public TagListTag(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Construct with Items
        /// </summary>
        /// <param name="items"></param>
        public TagListTag(List<Tag> items)
        {
            Items = items;
        }

        /// <summary>
        /// Construct with Name and Items
        /// </summary>
        /// <param name="name"></param>
        /// <param name="items"></param>
        public TagListTag(string name, List<Tag> items)
            : base(name)
        {
            Items = items;
        }

        /// <summary>
        /// Finds the index of the Tag with Name == name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindIndex(string name)
        {
            return Items.FindIndex(i => i.Name == name);
        }

        /// <summary>
        /// Accesses the tag at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Tag this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        /// <summary>
        /// Accesses the tag with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tag this[string name]
        {
            get { return Items[FindIndex(name)]; }
            set { Items[FindIndex(name)] = value; }
        }

        /// <summary>
        /// Gets the Tag of type T with Name == name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private T GetTag<T>(string name) where T : Tag
        {
            return (T)this[name];
        }

        /// <summary>
        /// Sets the Tag with Name == name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="tag"></param>
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

        /// <summary>
        /// Gets the Tag of type T at index i
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i"></param>
        /// <returns></returns>
        public T GetTag<T>(int i) where T : Tag
        {
            return (T)Items[i];
        }

        /// <summary>
        /// Sets the Tag at index i
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i"></param>
        /// <param name="tag"></param>
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
