using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary.Structure;
using Microsoft.Xna.Framework;
using PacmanLibrary;

namespace PacmanLibraryTest.StructureTest
{
    [TestClass]
    public class PathClassTest
    {
        [TestMethod]
        public void PathConstructorTest_ValidPositionInput()
        {
            Pellet member = new Pellet();
            Path path = new Path(1, 1, member);
            Vector2 expected = new Vector2(1, 1);
            Vector2 actual = path.Position;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PathConstructorTest_InvalidPositionInput()
        {
            Pellet member = new Pellet();
            Path path = new Path(-1, -1, member);
        }

        [TestMethod]
        public void PathConstructorTest_ValidMemberInputNull()
        {
            Path path = new Path(1, 1, null);
            ICollidable expected = null;
            ICollidable actual = path.Member;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PathConstructorTest_ValidMemberInputNotNull()
        {
            Pellet expected = new Pellet();
            Path path = new Path(1, 1, expected);
            ICollidable actual = path.Member;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void MemberPropertyTestGet()
        {
            Energizer expected = new Energizer();
            Path path = new Path(1, 1, expected);
            ICollidable actual = path.Member;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MemberPropertyTestSet()
        {
            Energizer e = new Energizer();
            Path path = new Path(1, 1, e);
            Pellet expected = new Pellet();
            path.Member = expected;
            ICollidable actual = path.Member;
            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void CanEnterMethodTest()
        {
            bool expected = true;
            Pellet member = new Pellet();
            Path path = new Path(26, 26, member);
            bool actual = path.CanEnter();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsEmptyMethodTest_Null()
        {
            Pellet member = null;
            Path path = new Path(26, 26, member);
            bool expected = true;
            bool actual = path.IsEmpty();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsEmptyMethodTest_NotNull()
        {
            Pellet member = new Pellet();
            Path path = new Path(26, 26, member);
            bool expected = false;
            bool actual = path.IsEmpty();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PositionPropertyTestGet()
        {
            Pellet member = new Pellet();
            Path path = new Path(26, 26, member);
            Vector2 expected = new Vector2(26, 26);
            Vector2 actual = path.Position;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PositionPropertyTestSet_ValidInput()
        {
            Pellet member = new Pellet();
            Path path = new Path(26, 26, member);
            Vector2 expected = new Vector2(30, 30);
            path.Position = expected;
            Vector2 actual = path.Position;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PositionPropertyTestSet_InvalidInput()
        {
            Pellet member = new Pellet();
            Path path = new Path(26, 26, member);
            Vector2 expected = new Vector2(-30, 30);
            path.Position = expected;
        }

        [TestMethod]
        public void GetDistanceMethodTest()
        {
            Pellet member = new Pellet();
            Path path = new Path(26, 26, member);
            Path path2 = new Path(30, 30, member);
            float expected = Vector2.Distance(path2.Position, path.Position);
            float actual = path.GetDistance(path2.Position);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollideMethodTest_EventRaisedCheck()
        {
            Pellet p = new Pellet();
            Path path = new Path(10, 10, p);
            bool actual = false;
            bool expected = true;
            p.CollisionEvent += (x) =>
            {
                actual = true;
            };

            path.Collide();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CollideMethodTest_NullMemberCheck()
        {
            Pellet p = new Pellet();
            Path path = new Path(10, 10, p);
            p.CollisionEvent += (x) =>
            {
                Console.Write("just testing");
            };

            path.Collide();
            ICollidable expected = null;
            ICollidable actual = path.Member;
            Assert.AreEqual(actual, expected);
        }


    }
}
