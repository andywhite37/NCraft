using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using NCraft.Tags;
using NCraft.Util;

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
                if (isGZipped)
                {
                    using (var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                    {
                        return gzipStream.ReadTag();
                    }
                }
                else
                {
                    return fileStream.ReadTag();
                }
            }
        }

        public static void Save(Tag tag, string filePath)
        {
            Save(tag, filePath, true);
        }

        public static void Save(Tag tag, string filePath, bool isGZipped)
        {
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                if (isGZipped)
                {
                    using (var gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
                    {
                        gzipStream.WriteTag(tag);
                    }
                }
                else
                {
                    fileStream.WriteTag(tag);
                }
            }
        }
    }
}
