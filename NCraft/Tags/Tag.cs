using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

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

        public abstract void ReadFrom(Stream stream, bool readName);

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
}
