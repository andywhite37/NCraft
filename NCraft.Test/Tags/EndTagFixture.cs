using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class EndTagFixture
    {
        [Test]
        public void EndTagTest()
        {
            var tag = new EndTag();

            Assert.AreEqual(TagType.End, tag.Type);
        }
    }
}
