using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test.Tags
{
    [TestFixture]
    public class CompoundTagFixture
    {
        [Test]
        public void CompoundTagTest()
        {
            var tag = new CompoundTag();

            Assert.AreEqual(TagType.Compound, tag.Type);
        }
    }
}
