using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.DataFiles.Chunks;

namespace NCraft.Test.DataFiles.Chunks
{
    [TestFixture]
    public class ChunkFixture
    {
        [Test]
        public void LoadChunkTest()
        {
            var chunk = Chunk.Load(@"..\..\TestFiles\c.0.0.dat");

            AssertChunk(chunk);
        }

        [Test]
        public void SaveChunkTest()
        {
            var chunk = new Chunk()
            {
                Level = new Level()
                {
                    BlockLight = new BlockLight()
                    {
                        Length = 32768,
                        Values = new byte[32768],
                    },
                    Blocks = new Blocks()
                    {
                        Length = 32768,
                        RawBlockIds = new byte[32768],
                    },
                    Data = new Data()
                    {
                        Length = 32768,
                        Values = new byte[32768],
                    },
                    Entities = new Entities()
                    {
                        EntityList = new List<Entity>(),
                    },
                    HeightMap = new HeightMap()
                    {
                        Length = 256,
                        RawHeightData = new byte[256],
                    },
                    LastUpdate = 327730,
                    SkyLight = new SkyLight()
                    {
                        Length = 32768,
                        Values = new byte[32768],
                    },
                    TerrainPopulated = 1,
                    TileEntities = new TileEntities()
                    {
                        TileEntityList = new List<TileEntity>(),
                    },
                    XPos = 0, 
                    ZPos = 0, 
                },
            };

            chunk.Save(@"..\..\TestFiles\c.0.0.dat.out");

            var loaded = Chunk.Load(@"..\..\TestFiles\c.0.0.dat.out");

            AssertChunk(loaded);
        }

        private void AssertChunk(Chunk chunk)
        {
            Assert.IsNotNull(chunk);

            AssertLevel(chunk.Level);
        }

        private void AssertLevel(Level level)
        {
            Assert.IsNotNull(level);

            AssertData(level.Data);
            AssertEntities(level.Entities);
            Assert.AreEqual(327730, level.LastUpdate);
            Assert.AreEqual(0, level.XPos);
            Assert.AreEqual(0, level.ZPos);
            AssertTileEntities(level.TileEntities);
            Assert.AreEqual(1, level.TerrainPopulated);
            AssertSkyLight(level.SkyLight);
            AssertHeightMap(level.HeightMap);
            AssertBlockLight(level.BlockLight);
            AssertBlocks(level.Blocks);
        }

        public void AssertData(Data data)
        {
            Assert.IsNotNull(data);

            Assert.AreEqual(32768, data.Length);
            Assert.AreEqual(32768, data.Values.Length);

            // 4-bit words are unpacked into bytes
            //Assert.AreEqual(16384, data.Values.Length);
        }

        public void AssertEntities(Entities entities)
        {
            Assert.IsNotNull(entities);

            Assert.AreEqual(0, entities.EntityList.Count);
        }

        public void AssertTileEntities(TileEntities te)
        {
            Assert.IsNotNull(te);
            Assert.AreEqual(0, te.TileEntityList.Count);
        }

        public void AssertSkyLight(SkyLight sl)
        {
            Assert.IsNotNull(sl);
            // 4-bit words unpacked into bytes
            Assert.AreEqual(32768, sl.Length);
            Assert.AreEqual(32768, sl.Values.Length);
        }

        public void AssertHeightMap(HeightMap hm)
        {
            Assert.IsNotNull(hm);
            Assert.AreEqual(256, hm.Length);
            Assert.AreEqual(256, hm.RawHeightData.Length);
        }

        public void AssertBlockLight(BlockLight bl)
        {
            Assert.IsNotNull(bl);
            // 4-bit words unpacked into bytes
            Assert.AreEqual(32768, bl.Length);
            Assert.AreEqual(32768, bl.Values.Length);
        }

        public void AssertBlocks(Blocks b)
        {
            Assert.IsNotNull(b);
            Assert.AreEqual(32768, b.Length);
            Assert.AreEqual(32768, b.RawBlockIds.Length);
        }
    }
}
