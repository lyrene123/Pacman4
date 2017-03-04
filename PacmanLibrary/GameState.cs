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
    
    //for Amin
    public class GameState
    {
        public static GameState Parse(string file)
        {
            //string 2d array to hold the elements from the file text.
            string[,] map = new string[23, 23];
            //Setting GameState Object to hold the state of the game and its properties
            GameState g = new GameState();
            //g.Pacman = new Pacman(GameState);
            g.GhostPack = new GhostPack();
            g.Maze = new Maze();
            g.Pen = new Pen();
            g.Score = new ScoreAndLives(g);

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
        

            //On going Implementation / 
             
            for (int i = 0; i < map.GetLength(0); i++)
              {
                  for (int j = 0; j < map.GetLength(0); j++)
                  {
                      switch (map[i,j])
                      {
                          case "w":
                              Wall wall = new Wall(i, j);
                              g.Maze[i, j] = wall;
                              break;
                        case "m":
                              Structure.Path emptyMazePath = new Structure.Path(i,j,null);
                              g.Maze[i, j] = emptyMazePath;
                              break;
                        case "p":
                              Pellet pellet = new Pellet();
                              Structure.Path pelletMazePath = new Structure.Path(i, j, pellet);
                              g.Maze[i, j] = pelletMazePath;
                              break;
                         case "e":
                              Energizer energizer = new Energizer(g.GhostPack);
                              Structure.Path energizerPath = new Structure.Path(i, j, energizer);
                              g.Maze[i, j] = energizerPath;
                              break;
                          case "1":
                              Vector2 blinky_target = new Vector2(g.Pacman.Position.X + 2, g.Pacman.Position.Y);
                              Ghost blinky = new Ghost(g, i, j, blinky_target, GhostState.Released, Color.Red);
                              Structure.Path blinkyPath = new Structure.Path(i, j, blinky);
                              g.GhostPack.Add(blinky);
                              g.Maze[i, j] = blinkyPath;
                              break;
                          case "2":
                              Vector2 pinky_target = new Vector2(g.Pacman.Position.X + 4, g.Pacman.Position.Y);
                              Ghost pinky = new Ghost(g, i, j, pinky_target, GhostState.Penned, Color.Pink);
                              g.GhostPack.Add(pinky);
                              Structure.Path pinkyPath = new Structure.Path(i, j, pinky);
                              g.Maze[i, j] = pinkyPath;
                              g.Pen.AddTile(g.Maze[i,j]);
                              g.Pen.AddToPen(pinky);
                              break;
                          case "3":
                              Vector2 inky_target = new Vector2(g.Pacman.Position.X + 6, g.Pacman.Position.Y);
                              Ghost inky = new Ghost(g, i, j, inky_target, GhostState.Penned, Color.Blue);
                              g.GhostPack.Add(inky);
                              Structure.Path inkyPath = new Structure.Path(i, j, inky);
                              g.Maze[i, j] = inkyPath;
                              g.Pen.AddTile(g.Maze[i, j]);
                              g.Pen.AddToPen(inky);
                            break;
                          case "4":
                              Vector2 clyde_target = new Vector2(g.Pacman.Position.X + 1, g.Pacman.Position.Y);
                              Ghost clyde = new Ghost(g, i, j, clyde_target, GhostState.Penned, Color.Green);
                              g.GhostPack.Add(clyde);
                              Structure.Path clydePath = new Structure.Path(i, j, clyde);
                              g.Maze[i, j] = clydePath;
                              g.Pen.AddTile(g.Maze[i, j]);
                              g.Pen.AddToPen(clyde);
                              break;
                          case "P":
                              g.Pacman = new Pacman(g);
                              break;
                      }

                  }
              }

            return g;
        }
        private void SubscribeElements(ICollidable member)
        {

        }
        public Pacman Pacman
        {
            get { return Pacman; }
            private set { Pacman = value; }
        }
        public GhostPack GhostPack
        {
            get { return GhostPack; }
            private set { GhostPack = value; }
        }
        public Maze Maze
        {
            get { return Maze; }
            private set { Maze = value; }
        }
        public Pen Pen
        {
            get { return Pen; }
            private set { Pen = value; }
        }
        public ScoreAndLives Score 
        {
            get { return Score; }
            private set { Score = value; }
        }
        
    }
}
