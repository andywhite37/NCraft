using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NCraft.DataFiles.Chunks
{
    public class ChunkFile
    {
        public string RootPath { get; set; }
        public string XDirectory { get; set; }
        public string ZDirectory { get; set; }
        public string FileName { get; set; }
        public string FullPath { get { return Path.Combine(RootPath, XDirectory, ZDirectory, FileName); } }

        public ChunkFile(string rootPath, string xDirectory, string zDirectory, string fileName)
        {
            RootPath = rootPath;
            XDirectory = xDirectory;
            ZDirectory = zDirectory;
            FileName = fileName;
        }

        public Chunk Load()
        {
            return Chunk.Load(FullPath);
        }

        public override string ToString()
        {
            return FullPath;
        }
    }
}
