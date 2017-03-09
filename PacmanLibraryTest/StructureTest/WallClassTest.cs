using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary;
using PacmanLibrary.Structure;
using Microsoft.Xna.Framework;

namespace PacmanLibraryTest
{
    [TestClass]
    public class WallClassTest
    {
        [TestMethod]
        public void ConstructorTest_ValidData()
        {
            Vector2 expectedPosition = new Vector2(26, 26);
            Wall wall = new Wall(26, 26);
            Vector2 actual = wall.Position;
            Assert.AreEqual(expectedPosition, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
           "The x and y position of a Tile object must " +
                    "have a value of 0 or above.")]
        public void ConstructorTest_InvalidData()
        {
            Wall wall = new Wall(-26, 26);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void MemberPropertyTest_Set()
        {
            Wall wall = new Wall(26, 26);
            wall.Member = new Pellet();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void MemberPropertyTest_Get()
        {
            Wall wall = new Wall(26, 26);
            ICollidable member = wall.Member;
        }

        [TestMethod]
        public void CanEnterMethodTest()
        {
            bool expected = false;
            Wall wall = new Wall(26, 26);
            bool actual = wall.CanEnter();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CollideMethodTest()
        {
            Wall wall = new Wall(26, 26);
            wall.Collide();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void IsEmptyMethodTest()
        {
            Wall wall = new Wall(26, 26);
            wall.IsEmpty();
        }

        [TestMethod]
        public void PositionPropertyTestGet_ValidInput()
        {
            Vector2 expected = new Vector2(10, 10);
            Wall wall = new Wall(10, 10);
            Vector2 actual = wall.Position;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PositionPropertyTestSet_ValidInput()
        {
            Vector2 expected = new Vector2(15, 15);
            Wall wall = new Wall(10, 10);
            wall.Position = new Vector2(15, 15);
            Vector2 actual = wall.Position;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
           "The Tile object's position x and y must be positive")]
        public void PositionPropertyTestSet_InvalidInput()
        {
            Wall wall = new Wall(10, 10);
            wall.Position = new Vector2(-15, 15);
        }

        [TestMethod]
        public void GetDistanceMethodTest()
        {
            Vector2 v = new Vector2(10, 10);
            Wall wall = new Wall(20, 20);
            float expected = Vector2.Distance(v, new Vector2(20,20));
            float actual = wall.GetDistance(v);
            Assert.AreEqual(expected, actual);
        }
    }
}
