using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    public delegate void PacmanWonEventHandler();
    public class Maze
    {
        private Tile[,] maze;
        public event PacmanWonEventHandler PacmanWonEvent;
        public Maze()
        {
            Tile[,] someTiles = new Tile[2, 2]; //default
            SetTiles(someTiles);
        }

        public void SetTiles(Tile[,] tiles)
        {
            this.maze = tiles;
        }

        protected virtual void PacmanWon()
        {
            PacmanWonEvent?.Invoke();
        }

        public Tile this[int row, int column]
        {
            get { return this.maze[row, column]; }
            set { this.maze[row, column] = value; }
        }

        public int Size
        {
            get { return this.maze.GetLength(0); }
        }

        public List<Tile> GetAvailableNeighbours(Vector2 position, Direction direction)
        {
            List<Tile> availables = new List<Tile>();
            if (direction == Direction.Down)
            {
                availables = GetAvailableTilesDown(position);
                availables.AddRange(GetAvailableTilesLeft(position));
                availables.AddRange(GetAvailableTilesRight(position));
            }

            if(direction == Direction.Up)
            {
                availables = GetAvailableTilesUp(position);
                availables.AddRange(GetAvailableTilesLeft(position));
                availables.AddRange(GetAvailableTilesRight(position));
            }

            if(direction == Direction.Left)
            {
                availables = GetAvailableTilesLeft(position);
                availables.AddRange(GetAvailableTilesUp(position));
                availables.AddRange(GetAvailableTilesDown(position));
            }

            if(direction == Direction.Right)
            {
                availables = GetAvailableTilesRight(position);
                availables.AddRange(GetAvailableTilesUp(position));
                availables.AddRange(GetAvailableTilesDown(position));
            }
            return availables;
               
        }

        public void CheckMembersLeft()
        {
            int count = 0;
            foreach(Tile item in this.maze)
            {
                if (item is Path && item.IsEmpty() == false)
                {
                    count++;
                }
            }

            if(count == 0)
            {
                PacmanWon();
            }
        }


        private List<Tile> GetAvailableTilesDown(Vector2 position)
        {
            List<Tile> downTiles = new List<Tile>();
            
            //the x value of vector2 is the column value of array
            //the y value of vector 2 is the row value of array
            int column = (int)position.X; //does not change
            int row = (int)(position.Y + 1);

            while (row < this.maze.GetLength(0))
            {
                if(this.maze[row, column] is Path)
                {
                    downTiles.Add(this.maze[row, column]);
                }
                else
                {
                    break;
                }
                row++;
            }
            return downTiles;
        }

        private List<Tile> GetAvailableTilesUp(Vector2 position)
        {
            List<Tile> upTiles = new List<Tile>();

            //the x value of vector2 is the column value of array
            //the y value of vector 2 is the row value of array
            int column = (int)position.X; //does not change
            int row = (int)(position.Y - 1);

            while (column >= 0)
            {
                if (this.maze[row, column] is Path)
                {
                    upTiles.Add(this.maze[row, column]);
                }
                else
                {
                    break;
                }
                column--;
            }
            return upTiles;
        }

        private List<Tile> GetAvailableTilesLeft(Vector2 position)
        {
            List<Tile> leftTiles = new List<Tile>();

            //the x value of vector2 is the column value of array
            //the y value of vector 2 is the row value of array
            int column = (int)(position.X - 1);
            int row = (int)position.Y; //never changes

            while (column >= 0)
            {
                if (this.maze[row, column] is Path)
                {
                    leftTiles.Add(this.maze[row, column]);
                }
                else
                {
                    break;
                }
                column--;
            }
            return leftTiles;
        }

        private List<Tile> GetAvailableTilesRight(Vector2 position)
        {
            List<Tile> rightTiles = new List<Tile>();
            int posY = (int)position.Y; //never changes

            //the x value of vector2 is the column value of array
            //the y value of vector 2 is the row value of array
            int column = (int)(position.X + 1);
            int row = 

            while (posX < this.maze.GetLength(1))
            {
                if (this.maze[posX, posY] is Path)
                {
                    rightTiles.Add(this.maze[posX, posY]);
                }
                else
                {
                    break;
                }
                posY++;
            }
            return rightTiles;
        }


    }


    
}
