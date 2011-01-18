using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;

namespace NCraft.Test
{
    [TestFixture]
    class NbtFileFixture
    {
        [Test]
        public void ShouldBeAbleToLoadTestNbtNotGZipped()
        {
            var tag = (CompoundTag)NbtFile.Load(@"..\..\TestFiles\test.nbt", false);

            AssertTestNbt(tag);
        }

        [Test]
        public void ShouldBeAbleToLoadTestNbtGZipped()
        {
            var tag = (CompoundTag)NbtFile.Load(@"..\..\TestFiles\test.nbt.gz");

            AssertTestNbt(tag);
        }

        [Test]
        public void ShouldBeAbleToLoadBigTestNbtNotGZipped()
        {
            var tag = (CompoundTag)NbtFile.Load(@"..\..\TestFiles\bigtest.nbt");

            AssertBigTestNbt(tag);
        }

        private void AssertTestNbt(CompoundTag tag)
        {
            Assert.IsNotNull(tag);

            Assert.AreEqual("hello world", tag.Name);
            Assert.AreEqual(TagType.Compound, tag.Type);
            Assert.AreEqual("TAG_Compound", tag.TypeName);

            Assert.IsNotNull(tag.Value);

            Assert.AreEqual(1, tag.Value.Count);

            var subTag = (StringTag)tag.Value.First();

            Assert.AreEqual("name", subTag.Name);
            Assert.AreEqual(TagType.String, subTag.Type);
            Assert.AreEqual("Bananrama", subTag.Value);
        }

        private void AssertBigTestNbt(CompoundTag tag)
        {
        }
    }
}
