using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCraft.Tags;
using NCraft.Util;

namespace NCraft.DataFiles.Chunks
{
    public class Chunk
    {
        public Level Level { get; set; }

        public const int SizeX = 16;
        public const int SizeY = 128;
        public const int SizeZ = 16;

        public Chunk()
        {
        }

        public Chunk(CompoundTag tag)
        {
            LoadFromTag(tag);
        }

        public static Chunk Load(string filePath)
        {
            var tag = (CompoundTag)NbtFile.Load(filePath);
            var chunk = new Chunk(tag);

            return chunk;
        }

        public void Save(string filePath)
        {
            var tag = SaveToTag();
            NbtFile.Save(tag, filePath);
        }

        public void LoadFromTag(CompoundTag tag)
        {
            Level = new Level(tag.GetCompoundTag(Level.LEVEL));
        }

        public CompoundTag SaveToTag()
        {
            return new CompoundTag()
            {
                Name = "",
                Items = new List<Tag>()
                {
                    Level.SaveToTag(),
                },
            };
        }
    }

    public class Level
    {
        public const string LEVEL = "Level";
        private const string LAST_UPDATE = "LastUpdate";
        private const string X_POS = "xPos";
        private const string Z_POS = "zPos";
        private const string TERRAIN_POPULATED = "TerrainPopulated";

        public Blocks Blocks { get; set; }
        public Data Data { get; set; }
        public SkyLight SkyLight { get; set; }
        public BlockLight BlockLight { get; set; }
        public HeightMap HeightMap { get; set; }
        public Entities Entities { get; set; }
        public TileEntities TileEntities { get; set; }
        public long LastUpdate { get; set; }
        public int XPos { get; set; }
        public int ZPos { get; set; }
        public byte TerrainPopulated { get; set; }

        public Level()
        {
        }

        public Level(CompoundTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(CompoundTag tag)
        {
            Blocks = new Blocks(tag.GetByteArrayTag(Blocks.BLOCKS));
            Data = new Data(tag.GetByteArrayTag(Data.DATA));
            SkyLight = new SkyLight(tag.GetByteArrayTag(SkyLight.SKY_LIGHT));
            BlockLight = new BlockLight(tag.GetByteArrayTag(BlockLight.BLOCK_LIGHT));
            HeightMap = new HeightMap(tag.GetByteArrayTag(HeightMap.HEIGHT_MAP));
            Entities = new Entities(tag.GetListTag(Entities.ENTITIES));
            TileEntities = new TileEntities(tag.GetListTag(TileEntities.TILE_ENTITIES));
            LastUpdate = tag.GetLong(LAST_UPDATE);
            XPos = tag.GetInt(X_POS);
            ZPos = tag.GetInt(Z_POS);
            TerrainPopulated = tag.GetByte(TERRAIN_POPULATED);
        }

        public CompoundTag SaveToTag()
        {
            return new CompoundTag()
            {
                Name = LEVEL,
                Items = new List<Tag>()
                {
                    Blocks.SaveToTag(),
                    Data.SaveToTag(),
                    SkyLight.SaveToTag(),
                    BlockLight.SaveToTag(),
                    HeightMap.SaveToTag(),
                    Entities.SaveToTag(),
                    TileEntities.SaveToTag(),
                    new LongTag(LAST_UPDATE, LastUpdate),
                    new IntTag(X_POS, XPos),
                    new IntTag(Z_POS, ZPos),
                    new ByteTag(TERRAIN_POPULATED, TerrainPopulated),
                },
            };
        }
    }

    public class Blocks
    {
        public const string BLOCKS = "Blocks";

        public int Length { get; set; }
        public byte[] RawBlockIds { get; set; }

        public Blocks()
        {
        }

        public Blocks(ByteArrayTag tag)
        {
            LoadFromTag(tag);
        }

        public byte GetBlockId(int x, int y, int z)
        {
            return RawBlockIds[y + (z * Chunk.SizeY + (x * Chunk.SizeY * Chunk.SizeZ))];
        }

        public void LoadFromTag(ByteArrayTag tag)
        {
            RawBlockIds = tag.Items;
            Length = RawBlockIds.Length;
        }

        public ByteArrayTag SaveToTag()
        {
            return new ByteArrayTag(BLOCKS, RawBlockIds);
        }
    }

    public class Data : PackedWordData
    {
        public const string DATA = "Data";

        public Data()
            : base()
        {
        }

        public Data(ByteArrayTag tag)
            : base(tag)
        {
        }

        public ByteArrayTag SaveToTag()
        {
            return base.SaveToTag(DATA);
        }
    }

    public class SkyLight : PackedWordData
    {
        public const string SKY_LIGHT = "SkyLight";

        public SkyLight()
            : base()
        {
        }

        public SkyLight(ByteArrayTag tag)
            : base(tag)
        {
        }

        public ByteArrayTag SaveToTag()
        {
            return base.SaveToTag(SKY_LIGHT);
        }
    }

    public class BlockLight : PackedWordData
    {
        public const string BLOCK_LIGHT = "BlockLight";

        public BlockLight()
            : base()
        {
        }

        public BlockLight(ByteArrayTag tag)
            : base(tag)
        {
        }

        public ByteArrayTag SaveToTag()
        {
            return base.SaveToTag(BLOCK_LIGHT);
        }
    }

    public class HeightMap
    {
        public const string HEIGHT_MAP = "HeightMap";

        public int Length { get; set; }
        public byte[] RawHeightData { get; set; }

        public HeightMap()
        {
        }

        public HeightMap(ByteArrayTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ByteArrayTag tag)
        {
            RawHeightData = tag.Items;
            Length = tag.Length;
        }

        public ByteArrayTag SaveToTag()
        {
            return new ByteArrayTag()
            {
                Name = HEIGHT_MAP,
                Length = Length,
                Items = RawHeightData,
            };
        }
    }

    public class Entities
    {
        public const string ENTITIES = "Entities";

        public List<Entity> EntityList { get; set; }

        public Entities()
        {
        }

        public Entities(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
            EntityList = new List<Entity>();

            foreach (CompoundTag ct in tag.Items)
            {
                EntityList.Add(new Entity(ct));
            }
        }

        public ListTag SaveToTag()
        {
            return new ListTag()
            {
                Name = ENTITIES,
                Length = EntityList.Count,
                ItemType = TagType.Compound,
                Items = EntityList.ConvertAll(e => e.SaveToTag()).ToArray<Tag>(),
            };
        }
    }

    public class Entity
    {
        private const string ID = "id";
        private const string FALL_DISTANCE = "FallDistance";
        private const string FIRE = "Fire";
        private const string AIR = "Air";
        private const string ON_GROUND = "OnGround";

        public string Id { get; set; }
        public Pos Pos { get; set; }
        public Motion Motion { get; set; }
        public Rotation Rotation { get; set; }
        public float FallDistance { get; set; }
        public short Fire { get; set; }
        public short Air { get; set; }
        public byte OnGround { get; set; }

        public Entity()
        {
        }

        public Entity(CompoundTag playerTag)
        {
            LoadFromTag(playerTag);
        }

        public virtual void LoadFromTag(CompoundTag tag)
        {
            Id = tag.GetString(ID);
            Pos = new Pos(tag.GetListTag(Pos.POS));
            Motion = new Motion(tag.GetListTag(Motion.MOTION));
            Rotation = new Rotation(tag.GetListTag(Rotation.ROTATION));
            FallDistance = tag.GetFloat(FALL_DISTANCE);
            Fire = tag.GetShort(FIRE);
            Air = tag.GetShort(AIR);
            OnGround = tag.GetByte(ON_GROUND);
        }

        public virtual CompoundTag SaveToTag()
        {
            return new CompoundTag()
            {
                Name = "",
                Items = new List<Tag>()
                {
                    new StringTag(ID, Id),
                    Pos.SaveToTag(),
                    Motion.SaveToTag(),
                    Rotation.SaveToTag(),
                    new FloatTag(FALL_DISTANCE, FallDistance),
                    new ShortTag(FIRE, Fire),
                    new ShortTag(AIR, Air),
                    new ByteTag(ON_GROUND, OnGround),
                },
            };
        }
    }

    public class Pos
    {
        public const string POS = "Pos";

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Pos()
        {
        }

        public Pos(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
            X = tag.GetDouble(0);
            Y = tag.GetDouble(1);
            Z = tag.GetDouble(2);
        }

        public Tag SaveToTag()
        {
            return new ListTag()
            {
                Name = POS,
                ItemType = TagType.Double,
                Length = 3,
                Items = new Tag[]
                {
                    new DoubleTag(X),
                    new DoubleTag(Y),
                    new DoubleTag(Z),
                },
            };
        }
    }

    public class Rotation
    {
        public const string ROTATION = "Rotation";

        public float YawDegrees { get; set; }
        public float PitchDegrees { get; set; }

        public Rotation()
        {
        }

        public Rotation(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
            YawDegrees = tag.GetFloat(0);
            PitchDegrees = tag.GetFloat(1);
        }

        public Tag SaveToTag()
        {
            return new ListTag()
            {
                Name = ROTATION,
                ItemType = TagType.Float,
                Length = 2,
                Items = new Tag[]
                {
                    new FloatTag(YawDegrees),
                    new FloatTag(PitchDegrees),
                },
            };
        }
    }

    public class Motion
    {
        public const string MOTION = "Motion";

        public double DX { get; set; }
        public double DY { get; set; }
        public double DZ { get; set; }

        public Motion()
        {
        }

        public Motion(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
            DX = tag.GetDouble(0);
            DY = tag.GetDouble(1);
            DZ = tag.GetDouble(2);
        }

        public ListTag SaveToTag()
        {
            return new ListTag()
            {
                Name = MOTION,
                ItemType = TagType.Double,
                Length = 3,
                Items = new Tag[]
                {
                    new DoubleTag(DX),
                    new DoubleTag(DY),
                    new DoubleTag(DZ),
                },
            };
        }
    }

    public class TileEntities
    {
        public const string TILE_ENTITIES = "TileEntities";

        public List<TileEntity> TileEntityList { get; set; }

        public TileEntities()
        {
        }

        public TileEntities(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
            TileEntityList = new List<TileEntity>();

            foreach (CompoundTag t in tag.Items)
            {
                TileEntityList.Add(new TileEntity(t));
            }
        }

        public ListTag SaveToTag()
        {
            return new ListTag()
            {
                Name = TILE_ENTITIES,
            };
        }
    }

    public class TileEntity
    {
        private const string ID = "id";
        private const string X_NAME = "x";
        private const string Y_NAME = "y";
        private const string Z_NAME = "z";

        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public TileEntity()
        {
        }

        public TileEntity(CompoundTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(CompoundTag tag)
        {
            Id = tag.GetString(ID);
            X = tag.GetInt(X_NAME);
            Y = tag.GetInt(Y_NAME);
            Z = tag.GetInt(Z_NAME);
        }

        public CompoundTag SaveToTag()
        {
            return new CompoundTag()
            {
                Name = "",
                Items = new List<Tag>()
                {
                    new StringTag(ID, Id),
                    new IntTag(X_NAME, X),
                    new IntTag(Y_NAME, Y),
                    new IntTag(Z_NAME, Z),
                },
            };
        }
    }

    public abstract class PackedWordData
    {
        public int Length { get; set; }
        public byte[] Values { get; set; }

        public PackedWordData()
        {
        }

        public PackedWordData(ByteArrayTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ByteArrayTag tag)
        {
            Values = tag.Items.UnpackWordValues();
            Length = Values.Length;
        }

        protected ByteArrayTag SaveToTag(string name)
        {
            var bytes = Values.PackWordValues();
            var length = bytes.Length;

            return new ByteArrayTag()
            {
                Name = name,
                Length = bytes.Length,
                Items = bytes,
            };
        }
    }
}
