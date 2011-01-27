using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class FloatTagFixture
    {
        [Test]
        public void FloatTagTest()
        {
            var tag = new FloatTag();

            Assert.AreEqual(TagType.Float, tag.Type);
        }
    }
}
