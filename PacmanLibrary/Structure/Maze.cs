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
            }

            return availables;
               
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
                posY++;
            }

            return downTiles;
        }
    }


    }
}
