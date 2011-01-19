using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.Levels;

namespace NCraft.Test.Map
{
    [TestFixture]
    public class LevelFixture
    {
        [Test]
        public void LoadLevelTest()
        {
            var level = Level.Load(@"..\..\TestFiles\level.dat");

            Assert.IsNotNull(level);

            var data = level.Data;
            level.Data = new Data(null);
            Assert.IsNotNull(data);

            Assert.AreEqual(422818L, data.Time);
            Assert.AreEqual(1294594277265L, data.LastPlayed);
            Assert.AreEqual(220, data.SpawnX);
            Assert.AreEqual(64, data.SpawnY);
            Assert.AreEqual(443, data.SpawnZ);
            Assert.AreEqual(6658419L, data.SizeOnDisk);
            Assert.AreEqual(-2451905027594237963L, data.RandomSeed);

            var player = data.Player;

            Assert.IsNotNull(player);
        }
    }
}
