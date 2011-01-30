using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCraft.Chunks
{
    public class BlockInfo
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public byte Id { get; private set; }
        public string Name { get; private set; }
        public byte Data { get; private set; }

        private static Dictionary<int, BlockInfo> blockLookup = new Dictionary<int, BlockInfo>();

        static BlockInfo()
        {
            AddBlockInfo(new BlockInfo(0x00, "Air"));
            AddBlockInfo(new BlockInfo(0x01, "Stone"));
            AddBlockInfo(new BlockInfo(0x02, "Grass"));
            AddBlockInfo(new BlockInfo(0x03, "Dirt"));
            AddBlockInfo(new BlockInfo(0x04, "Cobblestone"));
            AddBlockInfo(new BlockInfo(0x05, "Wooden Plank"));
            AddBlockInfo(new BlockInfo(0x06, "Sapling"));
            AddBlockInfo(new BlockInfo(0x07, "Bedrock"));
            AddBlockInfo(new BlockInfo(0x08, "Water"));
            AddBlockInfo(new BlockInfo(0x09, "Stationary Water"));
            AddBlockInfo(new BlockInfo(0x0a, "Lava"));
            AddBlockInfo(new BlockInfo(0x0b, "Stationary Lava"));
            AddBlockInfo(new BlockInfo(0x0c, "Sand"));
            AddBlockInfo(new BlockInfo(0x0d, "Gravel"));
            AddBlockInfo(new BlockInfo(0x0e, "Gold Ore"));
            AddBlockInfo(new BlockInfo(0x0f, "Iron Ore"));
            AddBlockInfo(new BlockInfo(0x10, "Coal Ore"));
            AddBlockInfo(new BlockInfo(0x11, "Wood"));
            AddBlockInfo(new BlockInfo(0x12, "Leaves"));
            AddBlockInfo(new BlockInfo(0x13, "Sponge"));
            AddBlockInfo(new BlockInfo(0x14, "Glass"));
            AddBlockInfo(new BlockInfo(0x15, "Lapis Lazuli Ore"));
            AddBlockInfo(new BlockInfo(0x16, "Lapis Lazuli Block"));
            AddBlockInfo(new BlockInfo(0x17, "Dispenser"));
            AddBlockInfo(new BlockInfo(0x18, "Sandstone"));
            AddBlockInfo(new BlockInfo(0x19, "Note Block"));
            AddBlockInfo(new BlockInfo(0x23, "Wool"));
            AddBlockInfo(new BlockInfo(0x25, "Yellow Flower"));
            AddBlockInfo(new BlockInfo(0x26, "Red Rose"));
            AddBlockInfo(new BlockInfo(0x27, "Brown Mushroom"));
            AddBlockInfo(new BlockInfo(0x28, "Red Mushroom"));
            AddBlockInfo(new BlockInfo(0x29, "Gold Block"));
            AddBlockInfo(new BlockInfo(0x2a, "Iron Block"));
            AddBlockInfo(new BlockInfo(0x2b, "Double Stone Slab"));
            AddBlockInfo(new BlockInfo(0x2c, "Stone Slab"));
            AddBlockInfo(new BlockInfo(0x2d, "Brick Block"));
            AddBlockInfo(new BlockInfo(0x2e, "TNT"));
            AddBlockInfo(new BlockInfo(0x2f, "Bookshelf"));
            AddBlockInfo(new BlockInfo(0x30, "Moss Stone"));
            AddBlockInfo(new BlockInfo(0x31, "Obsidian"));
            AddBlockInfo(new BlockInfo(0x32, "Torch"));
            AddBlockInfo(new BlockInfo(0x33, "Fire"));
            AddBlockInfo(new BlockInfo(0x34, "Monster Spawner"));
            AddBlockInfo(new BlockInfo(0x35, "Wooden Stairs"));
            AddBlockInfo(new BlockInfo(0x36, "Chest"));
            AddBlockInfo(new BlockInfo(0x37, "Redstone Wire"));
            AddBlockInfo(new BlockInfo(0x38, "Diamond Ore"));
            AddBlockInfo(new BlockInfo(0x39, "Diamond Block"));
            AddBlockInfo(new BlockInfo(0x3a, "Workbench"));
            AddBlockInfo(new BlockInfo(0x3b, "Crops"));
            AddBlockInfo(new BlockInfo(0x3c, "Farmland"));
            AddBlockInfo(new BlockInfo(0x3d, "Furnace"));
            AddBlockInfo(new BlockInfo(0x3e, "Burning Furnace"));
            AddBlockInfo(new BlockInfo(0x3f, "Sign Post"));
            AddBlockInfo(new BlockInfo(0x40, "Wooden Door"));
            AddBlockInfo(new BlockInfo(0x41, "Ladder"));
            AddBlockInfo(new BlockInfo(0x42, "Minecart Tracks"));
            AddBlockInfo(new BlockInfo(0x43, "Cobblestone Stairs"));
            AddBlockInfo(new BlockInfo(0x44, "Wall Sign"));
            AddBlockInfo(new BlockInfo(0x45, "Lever"));
            AddBlockInfo(new BlockInfo(0x46, "Stone Pressure Plate"));
            AddBlockInfo(new BlockInfo(0x47, "Iron Door"));
            AddBlockInfo(new BlockInfo(0x48, "Wooden Pressure Plate"));
            AddBlockInfo(new BlockInfo(0x49, "Redstone Ore"));
            AddBlockInfo(new BlockInfo(0x4a, "Glowing Redstone Ore"));
            AddBlockInfo(new BlockInfo(0x4b, "Redstone Torch (off)"));
            AddBlockInfo(new BlockInfo(0x4c, "Redstone Torch (on)"));
            AddBlockInfo(new BlockInfo(0x4d, "Stone Button"));
            AddBlockInfo(new BlockInfo(0x4e, "Snow"));
            AddBlockInfo(new BlockInfo(0x4f, "Ice"));
            AddBlockInfo(new BlockInfo(0x50, "Snow Block"));
            AddBlockInfo(new BlockInfo(0x51, "Cactus"));
            AddBlockInfo(new BlockInfo(0x52, "Clay"));
            AddBlockInfo(new BlockInfo(0x53, "Sugar Cane"));
            AddBlockInfo(new BlockInfo(0x54, "Jukebox"));
            AddBlockInfo(new BlockInfo(0x55, "Fence"));
            AddBlockInfo(new BlockInfo(0x56, "Pumpkin"));
            AddBlockInfo(new BlockInfo(0x57, "Netherrack"));
            AddBlockInfo(new BlockInfo(0x58, "Soul Sand"));
            AddBlockInfo(new BlockInfo(0x59, "Glowstone"));
            AddBlockInfo(new BlockInfo(0x5a, "Portal"));
            AddBlockInfo(new BlockInfo(0x5b, "Jack-O-Lantern"));
            AddBlockInfo(new BlockInfo(0x5c, "Cake Block"));
        }

        public BlockInfo()
        {
        }

        public BlockInfo(byte id, string name)
        {
            Id = id;
            Name = name;
        }

        private static void AddBlockInfo(BlockInfo b)
        {
            blockLookup.Add(b.Id, b);
        }

        public static BlockInfo Get(int x, int y, int z, byte blockId, byte data)
        {
            var lookup = blockLookup[blockId];
            var result = lookup.Clone();

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.Data = data;

            return result;
        }

        public BlockInfo Clone()
        {
            return new BlockInfo()
            {
                Id = Id,
                Name = Name,
                Data = Data,
                X = X,
                Y = Y,
                Z = Z,
            };
        }

        public override string ToString()
        {
            return string.Format(
                "[Block: Id={0}, Name={1}, Data={2}, ({3}, {4}, {5})]",
                Id, Name, Data, X, Y, Z);
        }
    }
}
