using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class TagTypeFixture
    {
        [Test]
        public void TagTypeValueTest()
        {
            Assert.AreEqual(0x00, TagType.End);
            Assert.AreEqual(0x01, TagType.Byte);
            Assert.AreEqual(0x02, TagType.Short);
            Assert.AreEqual(0x03, TagType.Int);
            Assert.AreEqual(0x04, TagType.Long);
            Assert.AreEqual(0x05, TagType.Float);
            Assert.AreEqual(0x06, TagType.Double);
            Assert.AreEqual(0x07, TagType.ByteArray);
            Assert.AreEqual(0x08, TagType.String);
            Assert.AreEqual(0x09, TagType.List);
            Assert.AreEqual(0x0a, TagType.Compound);
        }

        [Test]
        public void TagTypeNameTest()
        {
            Assert.AreEqual("TAG_End", TagType.GetName(TagType.End));
            Assert.AreEqual("TAG_Byte", TagType.GetName(TagType.Byte));
            Assert.AreEqual("TAG_Short", TagType.GetName(TagType.Short));
            Assert.AreEqual("TAG_Int", TagType.GetName(TagType.Int));
            Assert.AreEqual("TAG_Long", TagType.GetName(TagType.Long));
            Assert.AreEqual("TAG_Float", TagType.GetName(TagType.Float));
            Assert.AreEqual("TAG_Double", TagType.GetName(TagType.Double));
            Assert.AreEqual("TAG_Byte_Array", TagType.GetName(TagType.ByteArray));
            Assert.AreEqual("TAG_String", TagType.GetName(TagType.String));
            Assert.AreEqual("TAG_List", TagType.GetName(TagType.List));
            Assert.AreEqual("TAG_Compound", TagType.GetName(TagType.Compound));
        }

        [Test]
        public void TagTypeCreateTagTest()
        {
            Assert.IsInstanceOf(typeof(EndTag), TagType.CreateTag(TagType.End));
            Assert.IsInstanceOf(typeof(ByteTag), TagType.CreateTag(TagType.Byte));
            Assert.IsInstanceOf(typeof(ShortTag), TagType.CreateTag(TagType.Short));
            Assert.IsInstanceOf(typeof(IntTag), TagType.CreateTag(TagType.Int));
            Assert.IsInstanceOf(typeof(LongTag), TagType.CreateTag(TagType.Long));
            Assert.IsInstanceOf(typeof(FloatTag), TagType.CreateTag(TagType.Float));
            Assert.IsInstanceOf(typeof(DoubleTag), TagType.CreateTag(TagType.Double));
            Assert.IsInstanceOf(typeof(ByteArrayTag), TagType.CreateTag(TagType.ByteArray));
            Assert.IsInstanceOf(typeof(StringTag), TagType.CreateTag(TagType.String));
            Assert.IsInstanceOf(typeof(ListTag), TagType.CreateTag(TagType.List));
            Assert.IsInstanceOf(typeof(CompoundTag), TagType.CreateTag(TagType.Compound));
        }
    }
}
