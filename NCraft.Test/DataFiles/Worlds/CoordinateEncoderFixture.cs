using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NCraft.DataFiles.Worlds;
using NCraft.Util;

namespace NCraft.Test.DataFiles.Worlds
{
    [TestFixture]
    public class CoordinateEncoderFixture
    {
        [Test]
        [Ignore] // Don't think this is possible due to the %63 in the reverse conversion
        public void GetCoordinateFromDirectoryNameTest()
        {
            /*
            Assert.AreEqual(0, CoordinateUtil.GetCoordinateFromDirectoryName("0"));
            Assert.AreEqual(-13, CoordinateUtil.GetCoordinateFromDirectoryName("1f"));
            Assert.AreEqual(44, CoordinateUtil.GetCoordinateFromDirectoryName("18"));
            Assert.AreEqual(-18, CoordinateUtil.GetCoordinateFromDirectoryName("1a"));
            Assert.AreEqual(-3, CoordinateUtil.GetCoordinateFromDirectoryName("1p"));
            */
        }

        [Test]
        public void GetDirectoryNameFromCoordinateTest()
        {
            Assert.AreEqual("0", CoordinateUtil.GetDirectoryNameFromCoordinate(0));
            Assert.AreEqual("1f", CoordinateUtil.GetDirectoryNameFromCoordinate(-13));
            Assert.AreEqual("18", CoordinateUtil.GetDirectoryNameFromCoordinate(44));
            Assert.AreEqual("1a", CoordinateUtil.GetDirectoryNameFromCoordinate(-18));
            Assert.AreEqual("1p", CoordinateUtil.GetDirectoryNameFromCoordinate(-3));
        }
    }
}
