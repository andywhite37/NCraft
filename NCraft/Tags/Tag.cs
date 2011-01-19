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

        public T this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }
    }

    public abstract class ListTag<T> : Tag where T : Tag
    {
        public List<T> Items { get; set; }

        public T this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        public T this[string name]
        {
            get { return Items.Single(t => t.Name == name); }
            set { this[name] = value; }
        }
    }
}
