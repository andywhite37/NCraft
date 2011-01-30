using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NCraft.Util;
using NCraft.Chunks;

namespace NCraft.DataFiles.Chunks
{
    public class ChunkInfo
    {
        public string RootPath { get; set; }
        public string XDirectory { get; set; }
        public string ZDirectory { get; set; }
        public string FileName { get; set; }
        public string FullPath { get { return Path.Combine(RootPath, XDirectory, ZDirectory, FileName); } }
        private Chunk chunk = null;

        public ChunkInfo(string rootPath, string xDirectory, string zDirectory, string fileName)
        {
            RootPath = rootPath;
            XDirectory = xDirectory;
            ZDirectory = zDirectory;
            FileName = fileName;
        }

        public Chunk Chunk
        {
            get
            {
                if (chunk == null)
                {
                    chunk = Chunk.Load(FullPath);
                }
                return chunk;
            }
        }

        public int XPos { get { return Chunk.Level.XPos; } }

        public int ZPos { get { return Chunk.Level.ZPos; } }

        public BlockInfo GetBlock(int x, int y, int z)
        {
            var blockId = Chunk.Level.Blocks.GetBlockId(x, y, z);
            var data = Chunk.Level.Data.GetValue(x, y, z);

            return BlockInfo.Get(x, y, z, blockId, data);
        }

        public void SetBlock(BlockInfo blockInfo)
        {
            Chunk.Level.Blocks.SetBlockId(blockInfo.X, blockInfo.Y, blockInfo.Z, blockInfo.Id);
            Chunk.Level.Data.SetValue(blockInfo.X, blockInfo.Y, blockInfo.Z, blockInfo.Data);
        }
    }
}
