using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCraft.Tags
{
    public static class TagType
    {
        public const byte End = 0x00;
        public const byte Byte = 0x01;
        public const byte Short = 0x02;
        public const byte Int = 0x03;
        public const byte Long = 0x04;
        public const byte Float = 0x05;
        public const byte Double = 0x06;
        public const byte ByteArray = 0x07;
        public const byte String = 0x08;
        public const byte List = 0x09;
        public const byte Compound = 0x0a;

        public static string GetName(byte tagType)
        {
            switch (tagType)
            {
                case End:
                    return "TAG_End";

                case Byte:
                    return "TAG_Byte";

                case Short:
                    return "TAG_Short";

                case Int:
                    return "TAG_Int";

                case Long:
                    return "TAG_Long";

                case Float:
                    return "TAG_Float";

                case Double:
                    return "TAG_Double";

                case ByteArray:
                    return "TAG_Byte_Array";

                case String:
                    return "TAG_String";
                
                case List:
                    return "TAG_List";

                case Compound:
                    return "TAG_Compound";

                default:
                    throw new ArgumentException("Unexpected tag type: " + tagType);
            }
        }

        public static Tag CreateTag(byte tagType)
        {
            switch (tagType)
            {
                case End:
                    return new EndTag();

                case Byte:
                    return new ByteTag();

                case Short:
                    return new ShortTag();

                case Int:
                    return new IntTag();

                case Long:
                    return new LongTag();

                case TagType.Float:
                    return new FloatTag();

                case TagType.Double:
                    return new DoubleTag();

                case TagType.ByteArray:
                    return new ByteArrayTag();

                case TagType.String:
                    return new StringTag();

                case TagType.List:
                    return new ListTag();

                case TagType.Compound:
                    return new CompoundTag();

                default:
                    throw new ArgumentException("Unexpected tag type: " + tagType);
            }
        }
    }
}
