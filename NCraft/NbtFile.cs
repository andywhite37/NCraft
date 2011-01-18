using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using NCraft.Tags;

namespace NCraft
{
    public class NbtFile
    {
        public static Tag Load(string filePath)
        {
            return Load(filePath, true);
        }

        public static Tag Load(string filePath, bool isGZipped)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var serializer = new TagSerializer();
                if (isGZipped)
                {
                    using (var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                    {
                        return serializer.Deserialize(gzipStream);
                    }
                }
                else
                {
                    return serializer.Deserialize(fileStream);
                }
            }
        }
    }
}
