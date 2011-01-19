using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Tags;
using System.IO;

namespace NCraft.Test
{
    [TestFixture]
    class NbtFileFixture
    {
        [Test]
        public void LoadTestNbtNotGZipped()
        {
            var tag = (CompoundTag)NbtFile.Load(@"..\..\TestFiles\test.nbt", false);

            AssertTestNbt(tag);
        }

        [Test]
        public void LoadTestNbtGZipped()
        {
            var tag = (CompoundTag)NbtFile.Load(@"..\..\TestFiles\test.nbt.gz");

            AssertTestNbt(tag);
        }

        [Test]
        public void LoadBigTestNbtNotGZipped()
        {
            var tag = (CompoundTag)NbtFile.Load(@"..\..\TestFiles\bigtest.nbt", false);

            AssertBigTestNbt(tag);
        }

        [Test]
        public void LoadBigTestNbtGZipped()
        {
            var tag = (CompoundTag)NbtFile.Load(@"..\..\TestFiles\bigtest.nbt.gz");

            AssertBigTestNbt(tag);
        }

        [Test]
        public void SaveTestNbtNotGZipped()
        {
            var tag = CreateTestNbtTag();

            var outFile = @"..\..\TestFiles\test.nbt.out";

            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            NbtFile.Save(tag, outFile, false);

            var loadedTag = (CompoundTag)NbtFile.Load(outFile, false);

            AssertTestNbt(loadedTag);
        }

        [Test]
        public void SaveTestNbtGZipped()
        {
            var tag = CreateTestNbtTag();

            var outFile = @"..\..\TestFiles\test.nbt.out.gz";

            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            NbtFile.Save(tag, outFile);

            var loadedTag = (CompoundTag)NbtFile.Load(outFile);

            AssertTestNbt(loadedTag);
        }

        [Test]
        public void SaveBigTestNbtNotGZipped()
        {
            var tag = CreateBigTestNbtTag();

            var outFile = @"..\..\TestFiles\bigtest.nbt.out";

            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            NbtFile.Save(tag, outFile, false);

            var loadedTag = (CompoundTag)NbtFile.Load(outFile, false);

            AssertBigTestNbt(loadedTag);
        }

        [Test]
        public void SaveBigTestNbtGZipped()
        {
            var tag = CreateBigTestNbtTag();

            var outFile = @"..\..\TestFiles\bigtest.nbt.out.gz";

            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            NbtFile.Save(tag, outFile);

            var loadedTag = (CompoundTag)NbtFile.Load(outFile);

            AssertBigTestNbt(loadedTag);
        }

        private void AssertTestNbt(CompoundTag tag)
        {
            Assert.IsNotNull(tag);

            Assert.AreEqual("hello world", tag.Name);
            Assert.AreEqual(TagType.Compound, tag.Type);
            Assert.AreEqual("TAG_Compound", tag.TypeName);

            Assert.IsNotNull(tag.Items);

            Assert.AreEqual(1, tag.Items.Count);

            var subTag = (StringTag)tag.Items.First();

            Assert.AreEqual("name", subTag.Name);
            Assert.AreEqual(TagType.String, subTag.Type);
            Assert.AreEqual("Bananrama", subTag.Value);
        }

        private void AssertBigTestNbt(CompoundTag tag)
        {
            Assert.AreEqual("Level", tag.Name);
            Assert.AreEqual(11, tag.Items.Count);

            var longTest = (LongTag)tag.Items.Single(t => t.Name == "longTest");
            Assert.AreEqual((long)9223372036854775807L, longTest.Value);

            var shortTest = (ShortTag)tag.Items.Single(t => t.Name == "shortTest");
            Assert.AreEqual((short)32767, shortTest.Value);

            var stringTest = (StringTag)tag.Items.Single(t => t.Name == "stringTest");
            Assert.AreEqual("HELLO WORLD THIS IS A TEST STRING ÅÄÖ!", stringTest.Value);

            var floatTest = (FloatTag)tag.Items.Single(t => t.Name == "floatTest");
            Assert.AreEqual(0.49823147F, floatTest.Value);

            var intTest = (IntTag)tag.Items.Single(t => t.Name == "intTest");
            Assert.AreEqual(2147483647, intTest.Value);

            var nestedCompoundTest = (CompoundTag)tag.Items.Single(t => t.Name == "nested compound test");
            Assert.AreEqual(2, nestedCompoundTest.Items.Count);
            var ham = (CompoundTag)nestedCompoundTest.Items.Single(t => t.Name == "ham");
            Assert.AreEqual(2, ham.Items.Count);
            var hamName = (StringTag)ham.Items.Single(t => t.Name == "name");
            Assert.AreEqual("Hampus", hamName.Value);
            var hamValue = (FloatTag)ham.Items.Single(t => t.Name == "value");
            Assert.AreEqual(0.75F, hamValue.Value);
            var egg = (CompoundTag)nestedCompoundTest.Items.Single(t => t.Name == "egg");
            Assert.AreEqual(2, egg.Items.Count);
            var eggName = (StringTag)egg.Items.Single(t => t.Name == "name");
            Assert.AreEqual("Eggbert", eggName.Value);
            var eggValue = (FloatTag)egg.Items.Single(t => t.Name == "value");
            Assert.AreEqual(0.5F, eggValue.Value);

            var listTestLong = (ListTag)tag.Items.Single(t => t.Name == "listTest (long)");
            Assert.AreEqual(5, listTestLong.Length);
            Assert.AreEqual(TagType.Long, listTestLong.ItemType);
            Assert.IsTrue(listTestLong.Items.All(i => i.GetType() == typeof(LongTag)));
            Assert.IsTrue(listTestLong.Items.All(i => i.Name == null)); // ListTag items do not have Names
            Assert.IsTrue(((LongTag)listTestLong.Items[0]).Value == 11);
            Assert.IsTrue(((LongTag)listTestLong.Items[1]).Value == 12);
            Assert.IsTrue(((LongTag)listTestLong.Items[2]).Value == 13);
            Assert.IsTrue(((LongTag)listTestLong.Items[3]).Value == 14);
            Assert.IsTrue(((LongTag)listTestLong.Items[4]).Value == 15);

            var listTestCompound = (ListTag)tag.Items.Single(t => t.Name == "listTest (compound)");
            Assert.AreEqual(2, listTestCompound.Length);
            Assert.AreEqual(TagType.Compound, listTestCompound.ItemType);
            Assert.IsTrue(listTestCompound.Items.All(i => i.GetType() == typeof(CompoundTag)));
            Assert.IsTrue(listTestCompound.Items.All(i => i.Name == null)); // ListTag items do not have Names
            var first = (CompoundTag)listTestCompound.Items[0];
            var firstName = (StringTag)first.Items.Single(t => t.Name == "name");
            Assert.AreEqual("Compound tag #0", firstName.Value);
            var firstCreatedOn = (LongTag)first.Items.Single(t => t.Name == "created-on");
            Assert.AreEqual(1264099775885L, firstCreatedOn.Value);
            var second = (CompoundTag)listTestCompound.Items[1];
            var secondName = (StringTag)second.Items.Single(t => t.Name == "name");
            Assert.AreEqual("Compound tag #1", secondName.Value);
            var secondCreatedOn = (LongTag)second.Items.Single(t => t.Name == "created-on");
            Assert.AreEqual(1264099775885L, secondCreatedOn.Value);

            var byteTest = (ByteTag)tag.Items.Single(t => t.Name == "byteTest");
            Assert.AreEqual((byte)127, byteTest.Value);

            var byteArrayTest = (ByteArrayTag)tag.Items.Single(t => t.Name == "byteArrayTest (the first 1000 values of (n*n*255+n*7)%100, starting with n=0 (0, 62, 34, 16, 8, ...))");
            Assert.AreEqual(1000, byteArrayTest.Length);
            for (int i = 0; i < 1000; ++i)
            {
                var calculated = (i * i * 255 + i * 7) % 100;
                Assert.AreEqual(calculated, byteArrayTest.Items[i]);
            }

            var doubleTest = (DoubleTag)tag.Items.Single(t => t.Name == "doubleTest");
            Assert.AreEqual(0.4931287132182315, doubleTest.Value);
        }

        private CompoundTag CreateTestNbtTag()
        {
            var tag = new CompoundTag()
            {
                Name = "hello world",
            };

            var subTag = new StringTag()
            {
                Name = "name",
                Value = "Bananrama",
            };

            tag.Items.Add(subTag);

            return tag;
        }

        private CompoundTag CreateBigTestNbtTag()
        {
            var tag = new CompoundTag()
            {
                Name = "Level",
            };

            var longTest = new LongTag()
            {
                Name = "longTest",
                Value = 9223372036854775807L,
            };
            tag.Items.Add(longTest);

            var shortTest = new ShortTag()
            {
                Name = "shortTest",
                Value = 32767,
            };
            tag.Items.Add(shortTest);

            var stringTest = new StringTag()
            {
                Name = "stringTest",
                Value = @"HELLO WORLD THIS IS A TEST STRING ÅÄÖ!",
            };
            tag.Items.Add(stringTest);

            var floatTest = new FloatTag()
            {
                Name = "floatTest",
                Value = 0.49823147F,
            };
            tag.Items.Add(floatTest);

            var intTest = new IntTag()
            {
                Name = "intTest",
                Value = 2147483647,
            };
            tag.Items.Add(intTest);

            var nestedCompoundTest = new CompoundTag()
            {
                Name = "nested compound test",
                Items = new List<Tag>()
                {
                    new CompoundTag()
                    {
                        Name = "ham",
                        Items = new List<Tag>()
                        {
                            new StringTag()
                            {
                                Name = "name",
                                Value = "Hampus",
                            },
                            new FloatTag()
                            {
                                Name = "value",
                                Value = 0.75F,
                            }
                        },
                    },
                    new CompoundTag()
                    {
                        Name = "egg",
                        Items = new List<Tag>()
                        {
                            new StringTag()
                            {
                                Name = "name",
                                Value = "Eggbert",
                            },
                            new FloatTag()
                            {
                                Name = "value",
                                Value = 0.5F,
                            }
                        },
                    }
                },
            };
            tag.Items.Add(nestedCompoundTest);

            var listTestLong = new ListTag()
            {
                Name = "listTest (long)",
                ItemType = TagType.Long,
                Length = 5,
                Items = new Tag[]
                {
                    new LongTag()
                    {
                        Value = 11,
                    },
                    new LongTag()
                    {
                        Value = 12,
                    },
                    new LongTag()
                    {
                        Value = 13,
                    },
                    new LongTag()
                    {
                        Value = 14,
                    },
                    new LongTag()
                    {
                        Value = 15,
                    },
                },
            };
            tag.Items.Add(listTestLong);

            var listTestCompound = new ListTag()
            {
                Name = "listTest (compound)",
                ItemType = TagType.Compound,
                Length = 2,
                Items = new Tag[]
                {
                    new CompoundTag()
                    {
                        Items = new List<Tag>()
                        {
                            new StringTag()
                            {
                                Name = "name",
                                Value = "Compound tag #0",
                            },
                            new LongTag()
                            {
                                Name = "created-on",
                                Value = 1264099775885L,
                            },
                        },
                    },
                    new CompoundTag()
                    {
                        Items = new List<Tag>()
                        {
                            new StringTag()
                            {
                                Name = "name",
                                Value = "Compound tag #1",
                            },
                            new LongTag()
                            {
                                Name = "created-on",
                                Value = 1264099775885L,
                            },
                        },
                    },
                },
            };
            tag.Items.Add(listTestCompound);

            var byteTest = new ByteTag()
            {
                Name = "byteTest",
                Value = 127,
            };
            tag.Items.Add(byteTest);

            var byteArrayTest = new ByteArrayTag()
            {
                Name = "byteArrayTest (the first 1000 values of (n*n*255+n*7)%100, starting with n=0 (0, 62, 34, 16, 8, ...))",
                Length = 1000,
                Items = new byte[1000],
            };
            for (int i = 0; i < 1000; ++i)
            {
                byteArrayTest.Items[i] = (byte)((i * i * 255 + i * 7) % 100);
            }
            tag.Items.Add(byteArrayTest);

            var doubleTest = new DoubleTag()
            {
                Name = "doubleTest",
                Value = 0.4931287132182315,
            };
            tag.Items.Add(doubleTest);

            return tag;
        }
    }
}
