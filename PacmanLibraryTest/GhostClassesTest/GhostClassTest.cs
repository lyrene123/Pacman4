using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary;
using System.IO;
using Microsoft.Xna.Framework;
using PacmanLibrary.Enums;
using PacmanLibrary.Ghost_classes;

namespace PacmanLibraryTest.GhostClassesTest
{
    [TestClass]
    public class GhostClassTest
    {
        [TestMethod]
        public void ConstructorTest_ValidInput()
        {
            GameState gs = getState();
            Vector2 pos = new Vector2(3, 1);
            Vector2 target = new Vector2(2, 2);
            GhostState state = GhostState.Chasing;
            PacmanLibrary.Enums.Color c = PacmanLibrary.Enums.Color.Red;
            Ghost ghost = new Ghost(gs, 3, 1, target, state, c);

            bool expected = true;
            bool actual = true;

            if (ghost.ghostColor != c)
                actual = false;
            if (ghost.Position != pos)
                actual = false;
            if (ghost.CurrentState != GhostState.Chasing)
                actual = false;
            if (ghost.Points != 200)
                actual = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_InvalidInput()
        {
            GameState gs = null;
            Vector2 pos = new Vector2(3, 1);
            Vector2 target = new Vector2(2, 2);
            GhostState state = GhostState.Chasing;
            PacmanLibrary.Enums.Color c = PacmanLibrary.Enums.Color.Red;
            Ghost ghost = new Ghost(gs, 3, 1, target, state, c);
        }

        [TestMethod]
        public void PositionPropertyTestGet()
        {
            Ghost ghost = getGhost();
            Vector2 expected = new Vector2(3, 1);
            Vector2 actual = ghost.Position;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PositionPropertyTestSet_ValidInput()
        {
            Ghost ghost = getGhost();
            ghost.Position = new Vector2(1, 1);
            Vector2 expected = new Vector2(1, 1);
            Vector2 actual = ghost.Position;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PositionPropertyTestSet_InvalidInput()
        {
            Ghost ghost = getGhost();
            ghost.Position = new Vector2(-1, 1);
        }


        [TestMethod]
        public void PointsPropertyTestGet()
        {
            Ghost ghost = getGhost();
            int expected = 200;
            int actual = ghost.Points;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void PointsPropertyTestSet_ValidInput()
        {
            Ghost ghost = getGhost();
            ghost.Points = 1000;
            int expected = 1000;
            int actual = ghost.Points;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PointsPropertyTestSet_InvalidInput()
        {
            Ghost ghost = getGhost();
            ghost.Points = -1000;
        }


        [TestMethod]
        public void CurrentStatePropertyTestGet()
        {
            Ghost ghost = getGhost();
            GhostState expected = GhostState.Chasing;
            GhostState actual = ghost.CurrentState;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ColorPropertyTestGet()
        {
            Ghost ghost = getGhost();
            PacmanLibrary.Enums.Color expected = PacmanLibrary.Enums.Color.Red;
            PacmanLibrary.Enums.Color actual = ghost.ghostColor;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangeStateMethodTest_IntoScared()
        {
            Ghost ghost = getGhost();
            ghost.ChangeState(GhostState.Scared);
            GhostState expected = GhostState.Scared;
            GhostState actual = ghost.CurrentState;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangeStateMethodTest_IntoRelease()
        {
            Ghost ghost = getGhost();
            ghost.ChangeState(GhostState.Released);
            GhostState expected = GhostState.Chasing;
            GhostState actual = ghost.CurrentState;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void ChangeStateMethodTest_IntoChasing()
        {
            Ghost ghost = getGhost();
            ghost.ChangeState(GhostState.Scared);
            ghost.ChangeState(GhostState.Chasing);
            GhostState expected = GhostState.Chasing;
            GhostState actual = ghost.CurrentState;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CollideMethodTest_PacmanDiedEventRaised()
        {
            Ghost ghost = getGhost();
            bool expected = true;
            bool actual = false;
            ghost.PacmanDiedEvent += () =>
            {
                actual = true;
            };
            ghost.Collide();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollideMethodTest_CollisionEventRaised()
        {
            Ghost ghost = getGhost();
            ghost.ChangeState(GhostState.Scared);
            
            bool expected = true;
            bool actual = false;
            ghost.CollisionEvent += (x) =>
            {
                actual = true;
            };
            ghost.Collide();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckCollisionsMethodTest_ValidInput()
        {
            Ghost ghost = getGhost();
            Vector2 pacmanPos = new Vector2(2, 2);
            ghost.Position = pacmanPos;
            bool expected = true;
            bool actual = false;
            ghost.PacmanDiedEvent += () =>
            {
                actual = true;
            };
            ghost.CheckCollisions(pacmanPos);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckCollisionsMethodTest_InvalidInput()
        {
            Ghost ghost = getGhost();
            ghost.CheckCollisions(new Vector2(-1,1));
        }


        public GameState getState()
        {
            String content = File.ReadAllText(@"ghostAndPacman.csv");
            GameState gstate = GameState.Parse(content);
            return gstate;
        }


        public Ghost getGhost()
        {
            GameState gs = getState();
            PacmanLibrary.Tile tile = new PacmanLibrary.Structure.Path(3, 3, null);
            gs.Pen.AddTile(tile);
            Vector2 pos = new Vector2(3, 1);
            Vector2 target = new Vector2(2, 2);
            GhostState state = GhostState.Chasing;
            PacmanLibrary.Enums.Color c = PacmanLibrary.Enums.Color.Red;
            Ghost ghost = new Ghost(gs, 3, 1, target, state, c);
            return ghost;
        }
    }
}
