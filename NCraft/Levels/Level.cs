﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCraft.Tags;

namespace NCraft.Levels
{
    public class Level
    {
        public Data Data { get; set; }

        public Level(CompoundTag tag)
        {
            LoadFromTag(tag);
        }

        public static Level Load(string filePath)
        {
            var tag = (CompoundTag)NbtFile.Load(filePath);
            var level = new Level(tag);

            return level;
        }

        public static void Save(Level level, string filePath)
        {
            var tag = level.SaveToTag();
            NbtFile.Save(tag, filePath);
        }

        public void LoadFromTag(CompoundTag tag)
        {
            Data = new Data(tag.GetCompoundTag(Data.DATA));
        }

        public CompoundTag SaveToTag()
        {
            return new CompoundTag()
            {
                Items = new List<Tag>()
                {
                    Data.SaveToTag(),
                },
            };
        }
    }

    public class Data
    {
        public const string DATA = "Data";

        private const string TIME = "Time";
        private const string LAST_PLAYED = "LastPlayed";
        private const string SPAWN_X = "SpawnX";
        private const string SPAWN_Y = "SpawnY";
        private const string SPAWN_Z = "SpawnZ";
        private const string SIZE_ON_DISK = "SizeOnDisk";
        private const string RANDOM_SEED = "RandomSeed";

        public long Time { get; set; }
        public long LastPlayed { get; set; }
        public Player Player { get; set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnZ { get; set; }
        public long SizeOnDisk { get; set; }
        public long RandomSeed { get; set; }

        public Data(CompoundTag dataTag)
        {
            LoadFromTag(dataTag);
        }

        public void LoadFromTag(CompoundTag tag)
        {
            Time = tag.GetLong(TIME);
            LastPlayed = tag.GetLong(LAST_PLAYED);
            Player = new Player(tag.GetCompoundTag(Player.PLAYER));
            SpawnX = tag.GetInt(SPAWN_X);
            SpawnY = tag.GetInt(SPAWN_Y);
            SpawnZ = tag.GetInt(SPAWN_Z);
            SizeOnDisk = tag.GetLong(SIZE_ON_DISK);
            RandomSeed = tag.GetLong(RANDOM_SEED);
        }

        public CompoundTag SaveToTag()
        {
            var tag = new CompoundTag()
            {
                Name = DATA,
                Items = new List<Tag>()
                {
                    new LongTag() { Name = TIME, Value = Time, },
                    new LongTag() { Name = LAST_PLAYED, Value = LastPlayed, },
                    Player.SaveToTag(),
                    new IntTag() { Name = SPAWN_X, Value = SpawnX, },
                    new IntTag() { Name = SPAWN_Y, Value = SpawnY, },
                    new IntTag() { Name = SPAWN_Z, Value = SpawnZ, },
                    new LongTag() { Name = SIZE_ON_DISK, Value = SizeOnDisk, },
                    new LongTag() { Name = RANDOM_SEED, Value = RandomSeed, },
                },
            };

            return tag;
        }

    }

    public class Player
    {
        public const string PLAYER = "Player";

        private const string ON_GROUND = "OnGround";
        private const string HURT_TIME = "HurtTime";
        private const string HEALTH = "Health";
        private const string DIMENSION = "Dimension";
        private const string AIR = "Air";
        private const string ATTACK_TIME = "AttackTime";
        private const string FIRE = "Fire";
        private const string FALL_DISTANCE = "FallDistance";
        private const string SCORE = "Score";
        private const string DEATH_TIME = "DeathTime";

        public Motion Motion { get; set; }
        public byte OnGround { get; set; }
        public short HurtTime { get; set; }
        public short Health { get; set; }
        public int Dimension { get; set; }
        public short Air { get; set; }
        public Inventory Inventory { get; set; }
        public Pos Pos { get; set; }
        public short AttackTime { get; set; }
        public short Fire { get; set; }
        public float FallDistance { get; set; }
        public Rotation Rotation { get; set; }
        public int Score { get; set; }
        public short DeathTime { get; set; }

        public Player(CompoundTag playerTag)
        {
            LoadFromTag(playerTag);
        }

        public void LoadFromTag(CompoundTag tag)
        {
            Motion = new Motion(tag.GetListTag(Motion.MOTION));
            OnGround = tag.GetByte(ON_GROUND);
            HurtTime = tag.GetShort(HURT_TIME);
            Health = tag.GetShort(HEALTH);
            Dimension = tag.GetInt(DIMENSION);
            Air = tag.GetByte(AIR);
            Inventory = new Inventory(tag.GetListTag(Inventory.INVENTORY));
            Pos = new Pos(tag.GetListTag(Pos.POS));
            AttackTime = tag.GetShort(ATTACK_TIME);
            Fire = tag.GetShort(FIRE);
            FallDistance = tag.GetFloat(FALL_DISTANCE);
            Rotation = new Rotation(tag.GetListTag(Rotation.ROTATION));
            Score = tag.GetInt(SCORE);
            DeathTime = tag.GetShort(DEATH_TIME);
        }

        public CompoundTag SaveToTag()
        {
            return new CompoundTag()
            {
                Name = Player.PLAYER,
                Items = new List<Tag>()
                {
                    Motion.SaveToTag(),
                    new ByteTag() { Name = ON_GROUND, Value = OnGround, },
                },
            };
        }
    }

    public class Pos
    {
        public const string POS = "Pos";

        public Pos(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
        }

        public Tag SaveToTag()
        {
            return new ListTag()
            {
            };
        }
    }

    public class Rotation
    {
        public const string ROTATION = "Rotation";

        public Rotation(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
        }

        public Tag SaveToTag()
        {
            return new ListTag()
            {
            };
        }
    }

    public class Motion
    {
        public const string MOTION = "Motion";

        public List<double> Values { get; set; }

        public Motion(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
            Values = tag.Items.Cast<DoubleTag>().Select(t => t.Value).ToList();
        }

        public ListTag SaveToTag()
        {
            return new ListTag()
            {
                Name = MOTION,
                ItemType = TagType.Double,
                Length = Values.Count,
                Items = Values.ConvertAll(i => new DoubleTag() { Value = i }).ToArray<Tag>(),
            };
        }
    }

    public class Inventory
    {
        public const string INVENTORY = "Inventory";

        public List<InventoryItem> InventoryItems { get; set; }

        public Inventory(ListTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(ListTag tag)
        {
            InventoryItems = tag.Items.Cast<CompoundTag>().Select(t => new InventoryItem(t)).ToList();
        }

        public ListTag SaveToTag()
        {
            return new ListTag()
            {
                Name = INVENTORY,
                ItemType = TagType.Compound,
                Length = InventoryItems.Count,
                Items = InventoryItems.ConvertAll(i => i.SaveToTag()).ToArray<Tag>(),
            };
        }
    }

    public class InventoryItem
    {
        private const string ID = "id";
        private const string DAMAGE = "Damage";
        private const string COUNT = "Count";
        private const string SLOT = "Slot";

        public short Id { get; set; }
        public short Damage { get; set; }
        public byte Count { get; set; }
        public byte Slot { get; set; }

        public InventoryItem(CompoundTag tag)
        {
            LoadFromTag(tag);
        }

        public void LoadFromTag(CompoundTag tag)
        {
            Id = tag.GetShort(ID);
            Damage = tag.GetShort(DAMAGE);
            Count = tag.GetByte(COUNT);
            Slot = tag.GetByte(SLOT);
        }

        public Tag SaveToTag()
        {
            return new CompoundTag()
            {
                Items = new List<Tag>()
                {
                    new ShortTag() { Name = ID, Value = Id, },
                    new ShortTag() { Name = DAMAGE, Value = Damage, },
                    new ByteTag() { Name = COUNT, Value = Count, },
                    new ByteTag() { Name = SLOT, Value = Slot, },
                },
            };
        }
    }
}
