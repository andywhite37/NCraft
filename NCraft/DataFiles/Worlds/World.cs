using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCraft.DataFiles.Levels;
using NCraft.DataFiles.Chunks;
using System.IO;
using NCraft.Util;

namespace NCraft.DataFiles.Worlds
{
    public class World
    {
        private const string LEVEL_FILE_NAME = "level.dat";

        public NCraft.DataFiles.Levels.Level Level { get; set; }
        public string RootWorldPath { get; private set; }
        public List<ChunkInfo> ChunkInfos { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinZ { get; set; }
        public int MaxZ { get; set; }

        public World(string rootWorldPath)
        {
            RootWorldPath = rootWorldPath;
            ChunkInfos = new List<ChunkInfo>();
            LoadFromRootWorldPath();
        }

        public static World Load(string rootWorldPath)
        {
            return new World(rootWorldPath);
        }

        public ChunkInfo GetChunkInfo(int x, int z)
        {
            var xDir = CoordinateUtil.GetDirectoryNameFromCoordinate(x);
            var zDir = CoordinateUtil.GetDirectoryNameFromCoordinate(z);

            var chunkInfo = ChunkInfos.Where(cf => cf.XDirectory == xDir && cf.ZDirectory == zDir).SingleOrDefault();

            return chunkInfo;
        }

        private void LoadFromRootWorldPath()
        {
            if (!Directory.Exists(RootWorldPath))
            {
                throw new DirectoryNotFoundException(RootWorldPath);
            }

            Level = NCraft.DataFiles.Levels.Level.Load(Path.Combine(RootWorldPath, LEVEL_FILE_NAME));

            var root = new DirectoryInfo(RootWorldPath);

            foreach (var xDirectory in root.EnumerateDirectories())
            {
                foreach (var zDirectory in xDirectory.EnumerateDirectories())
                {
                    foreach (var file in zDirectory.EnumerateFiles())
                    {
                        ChunkInfos.Add(new ChunkInfo(RootWorldPath, xDirectory.Name, zDirectory.Name, file.Name));
                    }
                }
            }
        }
    }
}
