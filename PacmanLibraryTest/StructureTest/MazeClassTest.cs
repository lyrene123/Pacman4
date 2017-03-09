using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanLibrary;
using PacmanLibrary.Structure;
using System.IO;

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
            Tile[,] tiles = { { new Wall(0,0), new Wall(0,1), new Wall(0,2) },
                 {new Wall(1,0), new PacmanLibrary.Structure.Path(1,1,new Pellet()), new Wall(1,2)}};
            Maze maze = new Maze();
            maze.SetTiles(tiles);

            bool expected = true;
            bool actual = true;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] != maze[i, j])
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
                        new PacmanLibrary.Structure.Path(3,3,new Pellet()),
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
