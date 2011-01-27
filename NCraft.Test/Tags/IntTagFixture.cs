using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class IntTagFixture
    {
        [Test]
        public void IntTagTest()
        {
            var tag = new IntTag();

            Assert.AreEqual(TagType.Int, tag.Type);
        }
    }
}
