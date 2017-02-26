using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    /// <summary>
    /// The Maze class encapsulates the properties and the behavior of a 
    /// maze in a pacman game. The Maze which represents the group of tiles 
    /// will be used to locate pacman, ghosts and ICollidable objects in a 
    /// maze and use that location to move the objects around.
    /// </summary>

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
            Tile down = null;
            Tile up = null;
            Tile right = null;
            Tile left = null;

            if (direction == Direction.Down)
            {
                down = GetAvailableTilesDown(position);
                up = GetAvailableTilesLeft(position);
                right = GetAvailableTilesRight(position);
            }
            else if(direction == Direction.Up)
            {
                up = GetAvailableTilesUp(position);
                left = GetAvailableTilesLeft(position);
                right = GetAvailableTilesRight(position);
            }
            else if(direction == Direction.Left)
            {
                left = GetAvailableTilesLeft(position);
                up = GetAvailableTilesUp(position);
                down = GetAvailableTilesDown(position);
            }
            else 
            {
                right = GetAvailableTilesRight(position);
                up = GetAvailableTilesUp(position);
                down = GetAvailableTilesDown(position);
            }

            if (down != null) availables.Add(down);
            if (up != null) availables.Add(up);
            if (left != null) availables.Add(left);
            if (right != null) availables.Add(right);

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


        private Tile GetAvailableTilesDown(Vector2 position)
        {
            Tile downTile = null;
            int posY = (int)(position.Y + 1);
            int posX = (int)position.X; //does not change

            if(posY < this.maze.GetLength(0))
            {
                if (this.maze[posX, posY] is Path)
                {
                    downTile = this.maze[posX, posY];
                }
            }
            return downTile;
        }

        private Tile GetAvailableTilesUp(Vector2 position)
        {
            Tile upTile = null;
            int posY = (int)(position.Y - 1);
            int posX = (int)position.X; //does not change

            if (posY >= 0)
            {
                if (this.maze[posX, posY] is Path)
                {
                    upTile = this.maze[posX, posY];
                }
            }
            return upTile;
        }

        private Tile GetAvailableTilesLeft(Vector2 position)
        {
            Tile leftTile = null;
            int posY = (int)position.Y; //never changes
            int posX = (int)(position.X - 1);

            if (posX >= 0)
            {
                if (this.maze[posX, posY] is Path)
                {
                    leftTile = this.maze[posX, posY];
                }
            }
            return leftTile;
        }

        private Tile GetAvailableTilesRight(Vector2 position)
        {
            Tile rightTile = null;
            int posY = (int)position.Y; //never changes
            int posX = (int)(position.X + 1);

            if (posX < this.maze.GetLength(1))
            {
                if (this.maze[posX, posY] is Path)
                {
                    rightTile = this.maze[posX, posY];
                }
            }
            return rightTile;
        }


    }
   
}
