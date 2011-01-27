using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class DoubleTagFixture
    {
        [Test]
        public void DoubleTagTest()
        {
            var tag = new DoubleTag();

            Assert.AreEqual(TagType.Double, tag.Type);
        }
    }
}
