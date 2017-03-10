using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary;
using System.IO;
using PacmanLibrary.Ghost_classes;
using Microsoft.Xna.Framework;
using PacmanLibrary.Structure;
using System.Collections.Generic;

namespace PacmanLibraryTest
{
    [TestClass]
    public class PacmanClassTest
    {
        GameState gs = null;
        public PacmanClassTest()
        {
            gs = getState();
        }
        [TestMethod]
        public void PositionPropertyTestGet()
        {
            Vector2 expected = new Vector2(1, 3);
            Vector2 actual = gs.Pacman.Position;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PositionPropertyTestSet_ValidInput()
        {
            gs.Pacman.Position = new Vector2(1, 1);
            Vector2 expected = new Vector2(1, 1);
            Vector2 actual = gs.Pacman.Position;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PositionPropertyTestSet_InvalidInput()
        {
            gs.Pacman.Position = new Vector2(-1, 1);

        }
        [TestMethod]
        public void MoveMethodTest_ValidInput_UpDirection()
        {
            gs.Pacman.Move(Direction.Up);
            Vector2 expected = new Vector2(1, 2);
            Vector2 actual = gs.Pacman.Position;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void MoveMethodTest_ValidInput_RightDirection()
        {
            //First needed to change Pacman Position to be able to move Right
            gs.Pacman.Position = new Vector2(1, 1);
            gs.Pacman.Move(Direction.Right);
            Vector2 expected = new Vector2(2, 1);
            Vector2 actual = gs.Pacman.Position;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void MoveMethodTest_ValidInput_DownDirection()
        {
            //First needed to change Pacman Position to be able to move Down
            gs.Pacman.Position = new Vector2(3, 1);
            gs.Pacman.Move(Direction.Down);
            Vector2 expected = new Vector2(3, 2);
            Vector2 actual = gs.Pacman.Position;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void MoveMethodTest_ValidInput_LeftDirection()
        {
            //First needed to change Pacman Position to be able to move Left
            gs.Pacman.Position = new Vector2(3, 1);
            gs.Pacman.Move(Direction.Left);
            Vector2 expected = new Vector2(2, 1);
            Vector2 actual = gs.Pacman.Position;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void MoveMethodTest_ValidInput_MovingAgainstWall()
        {
            gs.Pacman.Move(Direction.Down);
            Vector2 expected = new Vector2(1, 3);
            Vector2 actual = gs.Pacman.Position;
            Assert.AreEqual(expected, actual);

        }
        /*[TestMethod]
        public void CollisionMethodTest_Valid()
        {
            gs.Pacman.Position = new Vector2(3, 1);
            bool expected = true;
            bool actual = false;
            var g = gs.GhostPack.GetEnumerator();
                      
            g.PacmanDiedEvent += () =>
            {
                actual = true;
            };
            g.CheckCollisions(gs.Pacman.Position);
            Assert.AreEqual(expected, actual);

        }
        */
        public GameState getState()
        {
            String content = File.ReadAllText(@"ghostAndPacman.csv");
            GameState gstate = GameState.Parse(content);
            return gstate;
        }

    }
}