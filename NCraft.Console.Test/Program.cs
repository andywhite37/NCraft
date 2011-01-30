using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.DataFiles.Worlds;
using NCraft.DataFiles.Chunks;

namespace NCraft.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadNbtFile();

            var world = World.Load(@"..\..\..\NCraft.Test\TestFiles\World1");

            var chunkInfo = world.GetChunkInfo(0, 0);

            for (int y = Chunk.SizeY - 1; y >= 0; --y)
            {
                System.Console.WriteLine("Level {0}", y);
                for (int x = Chunk.SizeX - 1; x >= 0; --x)
                {
                    for (int z = Chunk.SizeZ - 1; z >= 0; --z)
                    {
                        System.Console.Write("[{0}]", chunkInfo.GetBlock(x, y, z).Name);
                    }
                    System.Console.WriteLine();
                }

                System.Console.ReadLine();
            }

            /*
            for (int y = 0; y < Chunk.SizeY; y++)
            {
                for (int x = 0; x < Chunk.SizeX; x++)
                {
                    for (int z = 0; z < Chunk.SizeZ; z++)
                    {
                        System.Console.WriteLine("{0} ,{1}, {2}: {3}", x, y, z, world.GetChunkInfo(0, 0).Chunk.Level.Blocks.GetBlockId(x, y, z));
                    }
                    System.Console.ReadLine();
                }
            }
            */
        }

        private static void LoadNbtFile()
        {
            try
            {
                var directory = @"..\..\..\NCraft.Test\TestFiles\";

                //var file = @"bigtest.nbt.gz";
                //var file = @"level.dat";
                var file = @"c.0.0.dat";

                var path = Path.Combine(directory, file);
                //var path = Path.Combine(directory, "World1", "1a", "1p", "c.-i.-3.dat");


                var tag = NbtFile.Load(path);

                System.Console.WriteLine(tag.ToString());
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
