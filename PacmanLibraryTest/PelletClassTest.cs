using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary.Structure;

namespace PacmanLibraryTest
{
    /// <summary>
    /// Author: Amin Manai
    /// version: March 2017
    /// </summary>


    [TestClass]
    public class PelletClassTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            int expectedPoints = 10;
            Pellet pellet = new Pellet();
            int actual = pellet.Points;
            Assert.AreEqual(expectedPoints, actual);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PointsSetterTest_Error()
        {
            Pellet pellet = new Pellet();
            pellet.Points = -1;
        }

        [TestMethod]
        public void PointsSetterTest_Valid()
        {
            Pellet pellet = new Pellet();
            int expected = 110;
            pellet.Points += 100;
            int actual = pellet.Points;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PointGetterTest()
        {
            Pellet pellet = new Pellet();
            pellet.Points = 200;
            int expected = 200;
            int actual = pellet.Points;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollisionEventRaisedTest()
        {
            Boolean actual = false;
            Boolean expected = true;
            Pellet pellet = new Pellet();
            pellet.CollisionEvent += (x) =>
            {
                x.Points += 100;
                actual = true;
            };
            pellet.Collide();
            Assert.AreEqual(expected, actual);
        }
    }
}
