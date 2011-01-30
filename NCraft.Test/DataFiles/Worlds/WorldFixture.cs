using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.DataFiles.Worlds;
using NCraft.DataFiles.Chunks;

namespace NCraft.Test.DataFiles.Worlds
{
    [TestFixture]
    public class WorldFixture
    {
        [Test]
        public void TestChunkFiles()
        {
            var world = World.Load(@"..\..\TestFiles\World1");

            var minX = world.ChunkInfos.Min(cf => cf.XPos);
            var maxX = world.ChunkInfos.Max(cf => cf.XPos);

            var minZ = world.ChunkInfos.Min(cf => cf.ZPos);
            var maxZ = world.ChunkInfos.Max(cf => cf.ZPos);

            Console.WriteLine("X: {0} -> {1}", minX, maxX);
            Console.WriteLine("Z: {0} -> {1}", minZ, maxZ);
        }
    }
}
