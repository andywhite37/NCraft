using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class StringTagFixture
    {
        [Test]
        public void StringTagTest()
        {
            var tag = new StringTag();

            Assert.AreEqual(TagType.String, tag.Type);
        }
    }
}
