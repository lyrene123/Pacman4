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
            //g.Score = new ScoreAndLives(GameState);
                        
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

            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
           // assingning elements to the properties - to be implemented
                
            return g;
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
        /*public ScoreAndLives Score 
        {
            get { return Score; }
            private set { Score = value; }
        }
        */
    }
}
