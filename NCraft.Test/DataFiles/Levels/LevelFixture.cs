using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.DataFiles.Levels;
using NCraft.Tags;

namespace NCraft.Test.DataFiles.Levels
{
    [TestFixture]
    public class LevelFixture
    {
        [Test]
        public void LoadLevelTest()
        {
            var level = Level.Load(@"..\..\TestFiles\level.dat");

            AssertLevel(level);
        }

        [Test]
        public void SaveLevelTest()
        {
            var level = new Level()
            {
                Data = new Data()
                {
                    Time = 422818L,
                    LastPlayed = 1294594277265L,
                    Player = new Player()
                    {
                        Motion = new Motion()
                        {
                            DX = 3.61053074341395E-07,
                            DY = -0.0784000015258789,
                            DZ = -1.43072612962421E-08,
                        },
                        OnGround = 1,
                        HurtTime = 0,
                        Health = 20,
                        Dimension = 0,
                        Air = 300,
                        Inventory = new Inventory()
                        {
                            Items = new List<InventoryItem>()
                            {
                                new InventoryItem()
                                {
                                    Id = 274,
                                    Damage = 25,
                                    Count = 1,
                                    Slot = 0,
                                },
                                new InventoryItem()
                                {
                                    Id = 273,
                                    Damage = 27,
                                    Count = 1,
                                    Slot = 1,
                                },
                                new InventoryItem()
                                {
                                    Id = 272,
                                    Damage = 26,
                                    Count = 1,
                                    Slot = 3,
                                },
                                new InventoryItem()
                                {
                                    Id = 291,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 4,
                                },
                                new InventoryItem()
                                {
                                    Id = 4,
                                    Damage = 0,
                                    Count = 40,
                                    Slot = 5,
                                },
                                new InventoryItem()
                                {
                                    Id = 326,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 6,
                                },
                                new InventoryItem()
                                {
                                    Id = 320,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 7,
                                },
                                new InventoryItem()
                                {
                                    Id = 3,
                                    Damage = 0,
                                    Count = 4,
                                    Slot = 8,
                                },
                                new InventoryItem()
                                {
                                    Id = 274,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 9,
                                },
                                new InventoryItem()
                                {
                                    Id = 274,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 10,
                                },
                                new InventoryItem()
                                {
                                    Id = 273,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 11,
                                },
                                new InventoryItem()
                                {
                                    Id = 274,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 18,
                                },
                                new InventoryItem()
                                {
                                    Id = 273,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 19,
                                },
                                new InventoryItem()
                                {
                                    Id = 291,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 22,
                                },
                                new InventoryItem()
                                {
                                    Id = 263,
                                    Damage = 0,
                                    Count = 14,
                                    Slot = 26,
                                },
                                new InventoryItem()
                                {
                                    Id = 274,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 27,
                                },
                                new InventoryItem()
                                {
                                    Id = 273,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 28,
                                },
                                new InventoryItem()
                                {
                                    Id = 272,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 30,
                                },
                                new InventoryItem()
                                {
                                    Id = 291,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 31,
                                },
                                new InventoryItem()
                                {
                                    Id = 326,
                                    Damage = 0,
                                    Count = 1,
                                    Slot = 33,
                                },
                            },
                        },
                        Pos = new Pos()
                        {
                            X = 292.952445946266,
                            Y = 78.6200000047684,
                            Z = 364.287147669336
                        },
                        AttackTime = 0,
                        Fire = -20,
                        FallDistance = 0,
                        Rotation = new Rotation()
                        {
                            YawDegrees = -11565.46F,
                            PitchDegrees = 8.249827F,
                        },
                        Score = 0,
                        DeathTime = 0,
                    },
                    SpawnX = 220,
                    SpawnY = 64,
                    SpawnZ = 443,
                    SizeOnDisk = 6658419,
                    RandomSeed = -2451905027594237963,
                },
            };

            var tag = level.SaveToTag();
            NbtFile.Save(tag, @"..\..\TestFiles\level.dat.out", false);

            var loadLevel = new Level((CompoundTag)NbtFile.Load(@"..\..\TestFiles\level.dat.out", false));

            AssertLevel(loadLevel);
        }

        private void AssertLevel(Level level)
        {
            Assert.IsNotNull(level);

            AssertData(level.Data);
        }

        private void AssertData(Data data)
        {
            Assert.IsNotNull(data);

            Assert.AreEqual(422818L, data.Time);
            Assert.AreEqual(1294594277265L, data.LastPlayed);
            Assert.AreEqual(220, data.SpawnX);
            Assert.AreEqual(64, data.SpawnY);
            Assert.AreEqual(443, data.SpawnZ);
            Assert.AreEqual(6658419L, data.SizeOnDisk);
            Assert.AreEqual(-2451905027594237963L, data.RandomSeed);

            AssertPlayer(data.Player);
        }

        private void AssertPlayer(Player player)
        {
            Assert.IsNotNull(player);

            Assert.AreEqual(1, player.OnGround);
            Assert.AreEqual(0, player.HurtTime);
            Assert.AreEqual(20, player.Health);
            Assert.AreEqual(0, player.Dimension);
            Assert.AreEqual(300, player.Air);
            Assert.AreEqual(0, player.AttackTime);
            Assert.AreEqual(-20, player.Fire);
            Assert.AreEqual(0, player.FallDistance);
            Assert.AreEqual(0, player.Score);
            Assert.AreEqual(0, player.DeathTime);

            AssertPos(player.Pos);
            AssertRotation(player.Rotation);
            AssertMotion(player.Motion);
            AssertInventory(player.Inventory);
        }

        private static void AssertPos(Pos pos)
        {
            Assert.IsNotNull(pos);
            Assert.AreEqual(292.952445946266, pos.X, 0.000000001);
            Assert.AreEqual(78.6200000047684, pos.Y, 0.000000001);
            Assert.AreEqual(364.287147669336, pos.Z, 0.000000001);
        }

        private static void AssertRotation(Rotation rotation)
        {
            Assert.IsNotNull(rotation);
            Assert.AreEqual(-11565.46, rotation.YawDegrees, 0.01);
            Assert.AreEqual(8.249827, rotation.PitchDegrees, 0.01);
        }

        private void AssertMotion(Motion motion)
        {
            Assert.IsNotNull(motion);

            Assert.AreEqual(3.61053074341395E-07, motion.DX, 0.00000001);
            Assert.AreEqual(-0.0784000015258789, motion.DY, 0.00000001);
            Assert.AreEqual(-1.43072612962421E-08, motion.DZ, 0.00000001);
        }

        private static void AssertInventory(Inventory inventory)
        {
            Assert.IsNotNull(inventory);
            Assert.IsNotNull(inventory.Items);
            Assert.AreEqual(20, inventory.Items.Count);
            // TODO: check each item?
        }
    }
}
