using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCraft.DataFiles.Levels;
using NCraft.DataFiles.Chunks;
using System.IO;

namespace NCraft.DataFiles.Worlds
{
    public class World
    {
        private const string LEVEL_FILE_NAME = "level.dat";

        public NCraft.DataFiles.Levels.Level Level { get; set; }
        public string RootWorldPath { get; private set; }
        public List<ChunkFile> ChunkFiles { get; set; }

        public static World Load(string rootWorldPath)
        {
            return new World(rootWorldPath);
        }

        public World(string rootWorldPath)
        {
            if (!Directory.Exists(rootWorldPath))
            {
                throw new DirectoryNotFoundException(rootWorldPath);
            }
            RootWorldPath = rootWorldPath;
            ChunkFiles = new List<ChunkFile>();
            LoadFromRootWorldPath();
        }

        private void LoadFromRootWorldPath()
        {
            Level = NCraft.DataFiles.Levels.Level.Load(Path.Combine(RootWorldPath, LEVEL_FILE_NAME));

            var root = new DirectoryInfo(RootWorldPath);

            foreach (var xDirectory in root.GetDirectories())
            {
                foreach (var zDirectory in xDirectory.GetDirectories())
                {
                    foreach (var file in zDirectory.GetFiles())
                    {
                        ChunkFiles.Add(new ChunkFile(RootWorldPath, xDirectory.Name, zDirectory.Name, file.Name));
                    }
                }
            }
        }
    }
}
