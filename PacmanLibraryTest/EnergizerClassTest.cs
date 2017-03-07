using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary.Ghost_classes;
using PacmanLibrary.Structure;

namespace PacmanLibraryTest
{
    [TestClass]
    public class EnergizerClassTest
    {
        [TestMethod]
        public void EnergizerConstructorTest()
        {
            int expectedPoints = 100;
            Energizer e = new Energizer();
            int actual = e.Points;
            Assert.AreEqual(expectedPoints, actual);
        }

        [TestMethod]
        public void EnergizerGetterTest()
        {
            Energizer e = new Energizer();
            e.Points = 400;
            int expected = 400;
            int actual = e.Points;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnergizerSetterTest_InvalidInput()
        {
            Energizer e = new Energizer();
            e.Points = -1;
        }

        [TestMethod]
        public void EnergizerSetterTest_ValidInput()
        {
            Energizer e = new Energizer();
            e.Points = 500;
            int expected = 500;
            int actual = e.Points;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollisionEventRaisedTest()
        {
            Boolean actual = false;
            Boolean expected = true;
            Energizer e = new Energizer();
            e.CollisionEvent += (x) =>
            {
                x.Points += 100;
                actual = true;
            };
            e.Collide();
            Assert.AreEqual(expected, actual);
        }
    }
}
