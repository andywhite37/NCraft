using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class ShortTagFixture
    {
        [Test]
        public void ShortTagTest()
        {
            var tag = new ShortTag();

            Assert.AreEqual(TagType.Short, tag.Type);
        }
    }
}
