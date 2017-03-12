using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary;
using System.IO;
using PacmanLibrary.Structure;
using PacmanLibrary.Ghost_classes;

namespace PacmanLibraryTest
{
    //Author: Lyrene Labor

    [TestClass]
    public class ScoreAndLivesClassTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_InvalidInput()
        {
            ScoreAndLives s = new ScoreAndLives(null);
        }

        [TestMethod]
        public void LivesPropertyTestGet()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            int expected = 3;
            int actual = score.Lives;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LivesPropertyTestSet_ValidInput()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.Lives = 2;
            int expected = 2;
            int actual = score.Lives;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LivesPropertyTestSet_InvalidInput()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.Lives = -2; 
        }

        [TestMethod]
        public void ScorePropertyTestGet()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            int expected = 0;
            int actual = score.Score;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void ScorePropertyTestSet_ValidInput()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.Score = 100;
            score.Score = 100;
            int expected = 200;
            int actual = score.Score;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScorePropertyTestSet_InvalidInput()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.Score = -100;
        }

        [TestMethod]
        public void DeadPacmanMethodTest_EventRaised()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.Lives = 1;
            bool expected = true;
            bool actual = false;
            score.GameOver += () =>
            {
                actual = true;
            };
            score.DeadPacman();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeadPacmanMethodTest_NoEventRaised()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.Lives = 2;
            bool expected = true;
            bool actual = true;
            score.GameOver += () =>
            {
                actual = false;
            };
            score.DeadPacman();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncrementScoreMethodTest_CheckScore()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.IncrementScore(new Pellet());
            int expected = 10;
            int actual = score.Score;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncrementScoreMethodTest_WithEnergizer()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.IncrementScore(new Energizer());
            Ghost g = null;
            foreach(var item in s.GhostPack)
            {
                g = item;
            }

            GhostState expected = GhostState.Scared;
            GhostState actual = g.CurrentState;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncrementScoreMethodTest_InvalidInput()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            score.IncrementScore(null);
        }

        [TestMethod]
        public void IncrementScoreMethodTest_PacmanWonEventRaised()
        {
            GameState s = getState();
            ScoreAndLives score = s.Score;
            Maze m = s.Maze;

            for (int i = 0; i < m.Size; i++)
            {
                for (int j = 0; j < m.Size; j++)
                {
                    if (m[j, i] is PacmanLibrary.Structure.Path)
                        m[j, i].Member = null;
                }
            }

            bool expected = true;
            bool actual = false;
            m.PacmanWonEvent += () =>
            {
                actual = true;
            };

            score.IncrementScore(new Energizer());
            Assert.AreEqual(expected, actual);
        }

        public GameState getState()
        {
            String content = File.ReadAllText(@"ghostAndPacman.csv");
            GameState gstate = GameState.Parse(content);
            PacmanLibrary.Tile tile = new PacmanLibrary.Structure.Path(3, 3, null);
            gstate.Pen.AddTile(tile);
            return gstate;
        }
    }
}
