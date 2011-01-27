using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class ByteTagFixture
    {
        [Test]
        public void ByteTagTest()
        {
            var tag = new ByteTag();

            Assert.AreEqual(TagType.Byte, tag.Type);
        }
    }
}
