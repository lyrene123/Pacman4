using Microsoft.Xna.Framework;
using PacmanLibrary.Ghost_classes;
using PacmanLibrary.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{

    /// <summary>
    /// The GameState Class holds the properties of the main objects
    /// that run the pacman game providing setters and getters. It also
    /// contains a static method Parse which is responsable for all objects
    /// instantiations and the Maze settings. 
    /// 
    /// author: Daniel C
    /// version: March 2017
    /// </summary>
    public class GameState
    {
        /* private Pen _Pen;
         private Maze _Maze;
         private GhostPack _GhostPack;
         private ScoreAndLives _Score;
         private Pacman _Pacman;*/

        /// <summary>
        /// The Parse method reads from a file and creates a GameState object. 
        /// In the process of creation it instantiates all objects necessary to 
        /// run the game and sets them inside the Maze. It also automatically makes 
        /// the ScoreAndLives object subscribe to the related object events. 
        /// </summary>
        /// <param name="file">The text file to read from</param>
        /// <returns></returns>
        public static GameState Parse(String file)
        {
            //string 2d array to hold the elements from the file text.
            string[,] map = null;
            Tile[,] tileArray = null;

            //Setting GameState Object to hold the state of the game and its properties
            GameState g = new GameState();
            Pen pen = new Pen();
            Maze maze = new Maze();
            GhostPack gpack = new GhostPack();

            //g.Pacman = new Pacman(GameState);
            g.Pen = pen;
            g.Maze = maze;
            g.GhostPack = gpack;

            ScoreAndLives score = new ScoreAndLives(g);
            Pacman pacman = new Pacman(g);
            Vector2 myPac = new Vector2(1, 1);
            pacman.Position = myPac;

            g.Pacman = pacman;
            g.Score = score;


            //read text from file
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName +
            System.IO.Path.DirectorySeparatorChar + "TextFiles" + System.IO.Path.DirectorySeparatorChar + file;
            try
            {
                string[] fileText = File.ReadAllLines(path);
                int mapSize = fileText.Length;
                map = new string[mapSize, mapSize];
                tileArray = new Tile[mapSize, mapSize];
                //assign elements to the map string array
                Char delimiter = ',';
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    String[] elements = fileText[i].Split(delimiter);
                    for (int j = 0; j < map.GetLength(0); j++)
                    {
                        map[i, j] = elements[j];
                    }
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could Not Find File");
            }




            //Iterate through the map 2d array that holds the elements from the file text.

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    switch (map[y, x])
                    {
                        case "w":
                            Wall wall = new Wall(x, y);
                            Structure.Path wallPath = new Structure.Path(x, y, null);
                            tileArray[x, y] = wall;
                            break;
                        case "p":
                            Pellet pellet = new Pellet();
                            pellet.CollisionEvent += g.Score.incrementScore;
                            Structure.Path pelletMazePath = new Structure.Path(x, y, pellet);
                            tileArray[x, y] = pelletMazePath;
                            break;
                        case "e":
                            Energizer energizer = new Energizer(g.GhostPack);
                            energizer.CollisionEvent += g.Score.incrementScore;
                            Structure.Path energizerPath = new Structure.Path(x, y, energizer);
                            tileArray[x, y] = energizerPath;
                            break;
                        case "m":
                            Structure.Path emptyMazePath = new Structure.Path(x, y, null);
                            tileArray[x, y] = emptyMazePath;
                            break;
                        case "x":
                            Structure.Path emptyPath = new Structure.Path(x, y, null);
                            tileArray[x, y] = emptyPath;
                            pen.AddTile(emptyPath);
                            break;
                        case "P":
                            Vector2 myPac2 = new Vector2(x, y);
                            tileArray[x, y] = new Structure.Path(x, y, null);
                            pacman.Position = myPac2;
                            break;
                        case "1":
                            Vector2 blinky_target = new Vector2(pacman.Position.X + 2, pacman.Position.Y);
                            Ghost blinky = new Ghost(g, x, y, blinky_target, GhostState.Chasing, Color.Red);
                            Ghost.ReleasePosition = blinky.Position;
                            blinky.CollisionEvent += score.incrementScore;
                            blinky.PacmanDiedEvent += score.deadPacman;
                            gpack.Add(blinky);
                            tileArray[x, y] = new Structure.Path(x, y, null);
                            break;
                        case "2":
                            Vector2 pinky_target = new Vector2(pacman.Position.X + 4, pacman.Position.Y);
                            Ghost pinky = new Ghost(g, x, y, pinky_target, GhostState.Chasing, Color.Pink);
                            pinky.CollisionEvent += score.incrementScore;
                            pinky.PacmanDiedEvent += score.deadPacman;
                            gpack.Add(pinky);
                            Structure.Path aPath = new Structure.Path(x, y, null);
                            tileArray[x, y] = aPath;
                            pen.AddTile(aPath);
                            pen.AddToPen(pinky);
                            break;
                        case "3":
                            Vector2 inky_target = new Vector2(pacman.Position.X + 6, pacman.Position.Y);
                            Ghost inky = new Ghost(g, x, y, inky_target, GhostState.Chasing, Color.Blue);
                            inky.CollisionEvent += score.incrementScore;
                            inky.PacmanDiedEvent += score.deadPacman;
                            gpack.Add(inky);
                            Structure.Path aPath1 = new Structure.Path(x, y, null);
                            tileArray[x, y] = aPath1;
                            pen.AddTile(aPath1);
                            pen.AddToPen(inky);
                            break;
                        case "4":
                            Vector2 clyde_target = new Vector2(pacman.Position.X + 1, pacman.Position.Y);
                            Ghost clyde = new Ghost(g, x, y, clyde_target, GhostState.Chasing, Color.Green);
                            clyde.CollisionEvent += score.incrementScore;
                            clyde.PacmanDiedEvent += score.deadPacman;
                            gpack.Add(clyde);
                            Structure.Path aPath2 = new Structure.Path(x, y, null);
                            tileArray[x, y] = aPath2;
                            pen.AddTile(aPath2);
                            pen.AddToPen(clyde);
                            break;

                    }

                }
            }
            //set the Tiles in the Maze object with the tileArray 2d array.
            g.Maze.SetTiles(tileArray);

            return g;
        }
        /// <summary>
        /// The Pacman property gets and sets a Pacman Object.
        /// </summary>
        public Pacman Pacman
        {
            get;
            private set;
        }
        /// <summary>
        /// The GhostPack property gets and sets a GhostPack Object.
        /// The GhostPack object will encapsulates a list of Ghost Objects.
        /// </summary>
        public GhostPack GhostPack
        {
            get;
            private set;
        }
        /// <summary>
        /// The Maze property gets and sets a Maze Object.
        /// </summary>
        public Maze Maze
        {
            get;
            private set;
        }
        /// <summary>
        /// The Pen property gets and sets a Pen Object.
        /// </summary>
        public Pen Pen
        {
            get;
            private set;
        }
        /// <summary>
        /// The Score property gets and sets a ScoreAndLives Object.
        /// </summary>
        public ScoreAndLives Score
        {
            get;
            private set;
        }

    }

}
