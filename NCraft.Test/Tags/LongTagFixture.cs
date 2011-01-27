using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class LongTagFixture
    {
        [Test]
        public void LongTagTest()
        {
            var tag = new LongTag();

            Assert.AreEqual(TagType.Long, tag.Type);
        }
    }
}
