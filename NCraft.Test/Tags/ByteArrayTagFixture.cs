using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class ByteArrayTagFixture
    {
        [Test]
        public void ByteArrayTagTest()
        {
            var tag = new ByteArrayTag();

            Assert.AreEqual(TagType.ByteArray, tag.Type);
            Assert.AreEqual(TagType.GetName(TagType.ByteArray), tag.TypeName);
            Assert.IsNull(tag.Items);
            Assert.AreEqual(0, tag.Length);
        }
    }
}
