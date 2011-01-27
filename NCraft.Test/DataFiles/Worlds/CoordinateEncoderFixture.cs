using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.DataFiles.Worlds;

namespace NCraft.Test.DataFiles.Worlds
{
    [TestFixture]
    public class CoordinateEncoderFixture
    {
        [Test]
        [Ignore] // Don't think this is possible due to the %63 in the reverse conversion
        public void GetCoordinateFromDirectoryNameTest()
        {
            Assert.AreEqual(0, CoordinateEncoder.GetCoordinateFromDirectoryName("0"));
            Assert.AreEqual(-13, CoordinateEncoder.GetCoordinateFromDirectoryName("1f"));
            Assert.AreEqual(44, CoordinateEncoder.GetCoordinateFromDirectoryName("18"));
            Assert.AreEqual(-18, CoordinateEncoder.GetCoordinateFromDirectoryName("1a"));
            Assert.AreEqual(-3, CoordinateEncoder.GetCoordinateFromDirectoryName("1p"));
        }

        [Test]
        public void GetDirectoryNameFromCoordinateTest()
        {
            Assert.AreEqual("0", CoordinateEncoder.GetDirectoryNameFromCoordinate(0));
            Assert.AreEqual("1f", CoordinateEncoder.GetDirectoryNameFromCoordinate(-13));
            Assert.AreEqual("18", CoordinateEncoder.GetDirectoryNameFromCoordinate(44));
            Assert.AreEqual("1a", CoordinateEncoder.GetDirectoryNameFromCoordinate(-18));
            Assert.AreEqual("1p", CoordinateEncoder.GetDirectoryNameFromCoordinate(-3));
        }
    }
}
