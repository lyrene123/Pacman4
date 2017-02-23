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
            PacmanWonEvent?.Invoke();///////////
        }

        public Tile this[int x, int y]
        {
            get { return this.maze[x, y]; }
            set { this.maze[x, y] = value; }
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
            int posY = (int)(position.Y + 1);
            int posX = (int)position.X; //does not change

            while(posY < this.maze.GetLength(0))
            {
                if(this.maze[posX, posY] is Path)
                {
                    downTiles.Add(this.maze[posX, posY]);
                }
                else
                {
                    break;
                }
                
                posY++;
            }
            return downTiles;
        }

        private List<Tile> GetAvailableTilesUp(Vector2 position)
        {
            List<Tile> upTiles = new List<Tile>();
            int posY = (int)(position.Y - 1);
            int posX = (int)position.X; //does not change

            while(posY >= 0)
            {
                if (this.maze[posX, posY] is Path)
                {
                    upTiles.Add(this.maze[posX, posY]);
                }
                else
                {
                    break;
                }
                posY--;
            }
            return upTiles;
        }

        private List<Tile> GetAvailableTilesLeft(Vector2 position)
        {
            List<Tile> leftTiles = new List<Tile>();
            int posY = (int)position.Y; //never changes
            int posX = (int)(position.X-1); 

            while (posX >= 0)
            {
                if (this.maze[posX, posY] is Path)
                {
                    leftTiles.Add(this.maze[posX, posY]);
                }
                else
                {
                    break;
                }
                posY--;
            }
            return leftTiles;
        }

        private List<Tile> GetAvailableTilesRight(Vector2 position)
        {
            List<Tile> rightTiles = new List<Tile>();
            int posY = (int)position.Y; //never changes
            int posX = (int)(position.X + 1);

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
