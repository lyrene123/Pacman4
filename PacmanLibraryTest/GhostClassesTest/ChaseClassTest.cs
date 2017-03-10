using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary;
using System.IO;
using PacmanLibrary.Ghost_classes;
using Microsoft.Xna.Framework;
using PacmanLibrary.Structure;

namespace PacmanLibraryTest.GhostClassesTest
{
    [TestClass]
    public class ChaseClassTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_InvalidGhostInput()
        {
            GameState state = getState();
            //Ghost g = getGhost();
            Maze m = state.Maze;
            Vector2 t = new Vector2(1, 1);
            Pacman p = state.Pacman;
            Chase c = new Chase(null, m, t, p);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_InvalidMazeInput()
        {
            GameState state = getState();
            Ghost g = getGhost();
            //Maze m = state.Maze;
            Vector2 t = new Vector2(1, 1);
            Pacman p = state.Pacman;
            Chase c = new Chase(g, null, t, p);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_InvalidTargetInput()
        {
            GameState state = getState();
            Ghost g = getGhost();
            Maze m = state.Maze;
            Vector2 t = new Vector2(-1, 1);
            Pacman p = state.Pacman;
            Chase c = new Chase(g, m, t, p);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_InvalidPacmanInput()
        {
            GameState state = getState();
            Ghost g = getGhost();
            Maze m = state.Maze;
            Vector2 t = new Vector2(1, 1);
            //Pacman p = state.Pacman;
            Chase c = new Chase(g, m, t, null);
        }

        [TestMethod]
        public void MoveMethodTest_MovingLeft()
        {
            Ghost g = getGhost();
            Vector2 newPos = new Vector2(2, 1);
            Direction newDir = Direction.Left;
            g.Move(); //this should move the chase's move method

            bool expected = true;
            bool actual = true;

            if (g.Position != newPos || g.Direction != newDir)
                actual = false;

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void MoveMethodTest_MovingRight()
        {
            GameState state = getState();
            Ghost g = null;
            foreach(var ghost in state.GhostPack)
            {
                ghost.Position = new Vector2(1, 1);
                g = ghost;
            }         
            state.Pacman.Position = new Vector2(3, 3);
            Vector2 newPos = new Vector2(2, 1);
            Direction newDir = Direction.Right;
            g.Move(); //this should move the chase's move method

            bool expected = true;
            bool actual = true;

            if (g.Position != newPos || g.Direction != newDir)
                actual = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MoveMethodTest_MovingUp()
        {
            GameState state = getState();
            Ghost g = null;
            foreach (var ghost in state.GhostPack)
            {
                ghost.Position = new Vector2(3, 3);
                g = ghost;
            }
            state.Pacman.Position = new Vector2(1, 1);
            Vector2 newPos = new Vector2(3, 2);
            Direction newDir = Direction.Up;
            g.Move(); //this should move the chase's move method

            bool expected = true;
            bool actual = true;

            if (g.Position != newPos || g.Direction != newDir)
                actual = false;

            Assert.AreEqual(expected, actual);
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
