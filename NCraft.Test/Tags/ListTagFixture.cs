using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class ListTagFixture
    {
        [Test]
        public void ListTagTest()
        {
            var tag = new ListTag();

            Assert.AreEqual(TagType.List, tag.Type);
        }
    }
}
