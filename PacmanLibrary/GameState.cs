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
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// The Parse method reads from a file and creates a GameState object. 
        /// In the process of creation it instantiates all objects necessary to 
        /// run the game and sets them inside the Maze. It also automatically makes 
        /// the ScoreAndLives object subscribe to the related object events. 
        /// </summary>
        /// <param name="file">The text file to read from</param>
        /// <returns></returns>
        public static GameState Parse(string file)
        {
            //string 2d array to hold the elements from the file text.
            string[,] map = new string[23, 23];
            Tile[,] tileArray = new Tile[23, 23];

            //Setting GameState Object to hold the state of the game and its properties
            GameState g = new GameState();

            //g.Pacman = new Pacman(GameState);
            g.Pen = new Pen();
            g.Maze = new Maze();
            g.GhostPack = new GhostPack();
            g.Score = new ScoreAndLives(g);
            g.Pacman = new Pacman(g);

            //read text from file
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
            System.IO.Path.DirectorySeparatorChar + "TextFiles" + System.IO.Path.DirectorySeparatorChar + file;
            try
            {
                string[] fileText = File.ReadAllLines(file);
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
                            g.Pen.AddTile(emptyPath);
                            break;
                        case "1":
                            Vector2 blinky_target = new Vector2(g.Pacman.Position.X + 2, g.Pacman.Position.Y);
                            Ghost blinky = new Ghost(g, x, y, blinky_target, GhostState.Chasing, Color.Red);
                            Ghost.ReleasePosition = blinky.Position;
                            blinky.CollisionEvent += g.Score.incrementScore;
                            blinky.PacmanDiedEvent += g.Score.deadPacman;
                            g.GhostPack.Add(blinky);
                            tileArray[x, y] = new Structure.Path(x, y, null);
                            break;
                        case "2":
                            Vector2 pinky_target = new Vector2(g.Pacman.Position.X + 4, g.Pacman.Position.Y);
                            Ghost pinky = new Ghost(g, x, y, pinky_target, GhostState.Chasing, Color.Pink);
                            pinky.CollisionEvent += g.Score.incrementScore;
                            pinky.PacmanDiedEvent += g.Score.deadPacman;
                            g.GhostPack.Add(pinky);
                            tileArray[x, y] = new Structure.Path(x, y, null);
                            g.Pen.AddTile(g.Maze[x, y]);
                            g.Pen.AddToPen(pinky);
                            break;
                        case "3":
                            Vector2 inky_target = new Vector2(g.Pacman.Position.X + 6, g.Pacman.Position.Y);
                            Ghost inky = new Ghost(g, x, y, inky_target, GhostState.Chasing, Color.Blue);
                            inky.CollisionEvent += g.Score.incrementScore;
                            inky.PacmanDiedEvent += g.Score.deadPacman;
                            g.GhostPack.Add(inky);
                            tileArray[x, y] = new Structure.Path(x, y, null);
                            g.Pen.AddTile(g.Maze[x, y]);
                            g.Pen.AddToPen(inky);
                            break;
                        case "4":
                            Vector2 clyde_target = new Vector2(g.Pacman.Position.X + 1, g.Pacman.Position.Y);
                            Ghost clyde = new Ghost(g, x, y, clyde_target, GhostState.Chasing, Color.Green);
                            clyde.CollisionEvent += g.Score.incrementScore;
                            clyde.PacmanDiedEvent += g.Score.deadPacman;
                            g.GhostPack.Add(clyde);
                            tileArray[x, y] = new Structure.Path(x, y, null);
                            g.Pen.AddTile(g.Maze[x, y]);
                            g.Pen.AddToPen(clyde);
                            break;
                        case "P":
                            tileArray[x, y] = new Structure.Path(x, y, null);
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
            get { return Pacman; }
            private set { Pacman = value; }
        }
        /// <summary>
        /// The GhostPack property gets and sets a GhostPack Object.
        /// The GhostPack object will encapsulates a list of Ghost Objects.
        /// </summary>
        public GhostPack GhostPack
        {
            get { return GhostPack; }
            private set { GhostPack = value; }
        }
        /// <summary>
        /// The Maze property gets and sets a Maze Object.
        /// </summary>
        public Maze Maze
        {
            get { return Maze; }
            private set { Maze = value; }
        }
        /// <summary>
        /// The Pen property gets and sets a Pen Object.
        /// </summary>
        public Pen Pen
        {
            get { return Pen; }
            private set { Pen = value; }
        }
        /// <summary>
        /// The Score property gets and sets a ScoreAndLives Object.
        /// </summary>
        public ScoreAndLives Score
        {
            get { return Score; }
            private set { Score = value; }
        }

    }

}
