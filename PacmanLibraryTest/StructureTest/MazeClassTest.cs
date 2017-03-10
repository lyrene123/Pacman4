using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary;
using PacmanLibrary.Structure;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PacmanLibraryTest.StructureTest
{
    [TestClass]
    public class MazeClassTest
    {
        [TestMethod]
        public void NoParamConstructorTest()
        {
            Maze maze = new Maze();
            int expected = 2;
            int actual = maze.Size;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetTilesMethodTest_ValidInput()
        {
            Tile[,] tiles = { { new Wall(0,0), new Wall(0,1) },
                 {new Wall(1,0), new Wall(1,1)}};
            Maze maze = new Maze();
            maze.SetTiles(tiles);

            bool expected = true;
            bool actual = true;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].GetType() != maze[j, i].GetType())
                    {
                        actual = false;
                    }
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTilesMethodTest_InvalidInput()
        {
            Maze maze = new Maze();
            maze.SetTiles(null);
        }

        [TestMethod]
        public void IndexerGetTest()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            Tile[,] tiles = getEmptyMaze();

            bool expected = true;
            bool actual = true;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[j, i].GetType() != maze[i, j].GetType())
                    {
                        actual = false;
                    }
                }
            }
            Assert.AreEqual(expected, actual);
        } 


        [TestMethod]
        public void IndexerSetTest_ValidInput()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            bool expected = true;
            bool actual = true;
            for (int i = 0; i < maze.Size; i++)
            {
                for (int j = 0; j < maze.Size; j++)
                {
                    maze[j, i] = new Wall(i,j);
                }
            }

            for (int i = 0; i < maze.Size; i++)
            {
                for (int j = 0; j < maze.Size; j++)
                {
                    if(maze[j,i].GetType() != typeof(Wall))
                    {
                        actual = false;
                    }
                }
            }
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IndexerSetTest_InvalidInputNegative()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            maze[-1, 1] = new Wall(1,1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IndexerSetTest_InvalidInputOutOfBounds()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            maze[10, 1] = new Wall(1, 1);
        }

        [TestMethod]
        public void SizePropertyTest()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            int expected = 5;
            int actual = maze.Size;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAvailableNeighbours_InvalidInputPosition()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            Vector2 pos = new Vector2(-1, 1);
            List<Tile> tiles = maze.GetAvailableNeighbours(pos, Direction.Down);
        }

        [TestMethod]
        public void GetAvailableNeighbours_ValidInputDown()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            Vector2 pos = new Vector2(1, 1);
            PacmanLibrary.Structure.Path path1 = new PacmanLibrary.Structure.Path(2, 1,new Pellet());
            PacmanLibrary.Structure.Path path2 = new PacmanLibrary.Structure.Path(1, 2, new Energizer());
            List<Tile> tiles1 = new List<Tile>();
            tiles1.Add(path2);
            tiles1.Add(path1);
            List<Tile> tiles2 = maze.GetAvailableNeighbours(pos, Direction.Down);

            bool expected = true;
            bool actual = true;
            for(int i = 0; i<tiles1.Count; i++)
            {
                if(tiles1[i].GetType() != tiles2[i].GetType() || tiles1[i].Member?.GetType() != tiles2[i].Member?.GetType())
                {
                    actual = false;
                }
            }

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetAvailableNeighbours_ValidInputUp()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            Vector2 pos = new Vector2(1, 1);
            PacmanLibrary.Structure.Path path1 = new PacmanLibrary.Structure.Path(2, 1, new Pellet());
            List<Tile> tiles1 = new List<Tile>();
            tiles1.Add(path1);
            List<Tile> tiles2 = maze.GetAvailableNeighbours(pos, Direction.Up);

            bool expected = true;
            bool actual = true;
            for (int i = 0; i < tiles1.Count; i++)
            {
                if (tiles1[i].GetType() != tiles2[i].GetType() || tiles1[i].Member?.GetType() != tiles2[i].Member?.GetType())
                {
                    actual = false;
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAvailableNeighbours_ValidInputLeft()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            Vector2 pos = new Vector2(1, 1);
            PacmanLibrary.Structure.Path path1 = new PacmanLibrary.Structure.Path(1, 2, new Energizer());
            List<Tile> tiles1 = new List<Tile>();
            tiles1.Add(path1);
            List<Tile> tiles2 = maze.GetAvailableNeighbours(pos, Direction.Left);

            bool expected = true;
            bool actual = true;
            for (int i = 0; i < tiles1.Count; i++)
            {
                if (tiles1[i].GetType() != tiles2[i].GetType() || tiles1[i].Member?.GetType() != tiles2[i].Member?.GetType())
                {
                    actual = false;
                }
            }
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetAvailableNeighbours_ValidInputRight()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            Vector2 pos = new Vector2(1, 1);
            PacmanLibrary.Structure.Path path1 = new PacmanLibrary.Structure.Path(2, 1, new Pellet());
            PacmanLibrary.Structure.Path path2 = new PacmanLibrary.Structure.Path(1, 2, new Energizer());
            List<Tile> tiles1 = new List<Tile>();
            tiles1.Add(path2);
            tiles1.Add(path1);
            List<Tile> tiles2 = maze.GetAvailableNeighbours(pos, Direction.Right);

            bool expected = true;
            bool actual = true;
            for (int i = 0; i < tiles1.Count; i++)
            {
                if (tiles1[i].GetType() != tiles2[i].GetType() || tiles1[i].Member?.GetType() != tiles2[i].Member?.GetType())
                {
                    actual = false;
                }
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckMembersLeftTest_NoEventRaised()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            maze[1, 1].Member = null;
            maze[1, 2].Member = null;
            maze[1, 3].Member = null;
            bool expected = false;
            bool actual = false;
            maze.PacmanWonEvent += () =>
            {
                actual = true;
            };
            maze.CheckMembersLeft();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckMembersLeftTest_EventRaised()
        {
            GameState gs = getState();
            Maze maze = gs.Maze;
            for (int i = 0; i < maze.Size; i++)
            {
                for (int j = 0; j < maze.Size; j++)
                {
                    if(maze[j,i] is PacmanLibrary.Structure.Path)
                    {
                        maze[j, i].Member = null;
                    }
                }
            }
            bool expected = true;
            bool actual = false;
            maze.PacmanWonEvent += () =>
            {
                actual = true;
            };
            maze.CheckMembersLeft();
            Assert.AreEqual(expected, actual);
        }




        public Tile[,] getEmptyMaze()
        {
            Tile[,] tiles = { {new Wall(0,0), new Wall(0, 1), new Wall(0, 2),
                            new Wall(0,3),new Wall(0,4)},
                                {new Wall(1,0), new PacmanLibrary.Structure.Path(1,1,new Pellet()),
                        new PacmanLibrary.Structure.Path(1, 2, new Pellet()),
                        new PacmanLibrary.Structure.Path(1,3,new Pellet()),
                        new Wall(1,4)},
                                {new Wall(2,0), new PacmanLibrary.Structure.Path(2,1,new Energizer()),
                        new Wall(2,2),
                        new PacmanLibrary.Structure.Path(2,3,new Energizer()),
                        new Wall(2, 4)},
                                {new Wall(3,0), new PacmanLibrary.Structure.Path(3,1,new Pellet()),
                        new Wall(3,2),
                        new PacmanLibrary.Structure.Path(3,3,null),
                        new Wall(3,4)},
                                { new Wall(4,0), new Wall(4, 1), new Wall(4, 2),
                            new Wall(4,3),new Wall(4,4)}
                        };
            return tiles;
        }



        public GameState getState()
        {
            String content = File.ReadAllText(@"empty.csv");
            GameState gstate = GameState.Parse(content);
            return gstate;
        }


    }
}
