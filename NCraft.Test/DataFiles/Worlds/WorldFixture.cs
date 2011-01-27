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

            foreach (var file in world.ChunkFiles)
            {
                var chunk = file.Load();

                Console.WriteLine("{0}: x = {1}, z = {2}", file, chunk.Level.XPos, chunk.Level.ZPos);
            }
        }
    }
}
