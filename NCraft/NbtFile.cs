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
    /// <summary>
    /// Contains methods to load and save an NBT (Named Binary Tag) file.
    /// </summary>
    public class NbtFile
    {
        /// <summary>
        /// Loads a Tag from the given file.
        /// Note: according to the spec, an NBT file should contain exactly one "TAG_Compound"
        /// tag, so this could probably return CompoundTag.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
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
