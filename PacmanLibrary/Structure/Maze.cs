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
    /// 
    /// author: Lyrene Labor
    /// version: Feb 2017
    /// </summary>

    //delegate declaration for the PacmanWon event
    public delegate void PacmanWonEventHandler();
    public class Maze
    {
        private Tile[,] maze; //array of tile objects that make up the maze
        //event to ecapsulate PacmanWon delegate
        public event PacmanWonEventHandler PacmanWonEvent; 

        /// <summary>
        /// The Maze no parameter constructor will initialize the tiles array
        /// to a default 2x2 maze until user decides to change the size of the 
        /// maze by using the SetTiles method.
        /// </summary>
        public Maze()
        {
            Tile[,] someTiles = new Tile[2, 2]; //default 2x2 maze
            SetTiles(someTiles);
        }

        /// <summary>
        /// The SetTiles method will take as input a 2D rectangular array
        /// of Tiles object and will assign in to the tiles array member 
        /// of the class
        /// </summary>
        /// <param name="tiles">A 2D rectangular array of Tiles object</param>
        public void SetTiles(Tile[,] tiles)
        {
            if (object.ReferenceEquals(tiles, null))
                throw new ArgumentException("The tiles array input to the SetTiles method must not be null");
            this.maze = tiles;
        }

        /// <summary>
        /// The PacmanWon method will raise or invoke the Pacman won event
        /// </summary>
        protected virtual void PacmanWon()
        {
            PacmanWonEvent?.Invoke();
        }

        /// <summary>
        /// The Tile indexer method get or set a specific tile
        /// in the tiles array of the maze based on the x and y 
        /// position passed as input to the method
        /// </summary>
        /// <param name="x">integer x position</param>
        /// <param name="y">integer y position</param>
        /// <returns></returns>
        public Tile this[int x, int y]
        {
            get { return this.maze[x, y]; }
            set
            {
                if (x < 0 || y < 0)
                    throw new ArgumentException("Cannot access maze element with negative x and y positions");
                if(x >= this.Size || y >= this.Size)
                    throw new ArgumentException("Cannot access maze element with the input x and y positions");

                this.maze[x, y] = value;
            }
        }

        /// <summary>
        /// The Size property of the maze class only returns
        /// the size of the maze or the array of tiles that 
        /// make up the maze
        /// </summary>
        public int Size
        {
            get { return this.maze.GetLength(0); }
        }

        /// <summary>
        /// The GetAvailableNeighbours method takes in the current Tile position
        /// as well as the current Direction enum, and returns a list of available Tiles 
        /// besides a wall object and besides the one tile that goes backwards from
        /// the current Direction. 
        /// </summary>
        /// <param name="position">Vector2 object for position</param>
        /// <param name="direction">Direction enum for the current direction</param>
        /// <returns>A List of tiles containing available tiles</returns>
        public List<Tile> GetAvailableNeighbours(Vector2 position, Direction direction)
        {
            List<Tile> availables = new List<Tile>();
            Tile down = null;
            Tile up = null;
            Tile right = null;
            Tile left = null;

            //if the direction is DOWN, then get the available tiles going down,
            //up and right
            if (direction == Direction.Down)
            {
                down = GetAvailableTileDown(position);
                up = GetAvailableTileLeft(position);
                right = GetAvailableTileRight(position);
            }
            //if direction is UP, then check available tiles up, left and right
            else if(direction == Direction.Up)
            {
                up = GetAvailableTileUp(position);
                left = GetAvailableTileLeft(position);
                right = GetAvailableTileRight(position);
            }
            //if direction is Left, check available tiles going left, up and down
            else if(direction == Direction.Left)
            {
                left = GetAvailableTileLeft(position);
                up = GetAvailableTileUp(position);
                down = GetAvailableTileDown(position);
            }
            //if direction is right, check availables tiles going right, up and down
            else 
            {
                right = GetAvailableTileRight(position);
                up = GetAvailableTileUp(position);
                down = GetAvailableTileDown(position);
            }

            //add into list all available tiles retrieved which are not null
            if (down != null) availables.Add(down);
            if (up != null) availables.Add(up);
            if (left != null) availables.Add(left);
            if (right != null) availables.Add(right);

            return availables;              
        }

        /// <summary>
        /// The CheckMembersLeft method will check if there are any non empty
        /// tiles left in the maze besides walls. If all tiles are empty, then
        /// the pacman won event will be raised
        /// </summary>
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

        /// <summary>
        /// The GetAvailableTileDown method will check if the
        /// next available tile down based on the position
        /// passed as input is a path, still within the maze.
        /// If the next tile is valid, then it will be returned
        /// and if not, a null tile will be returned
        /// </summary>
        /// <param name="position">A vector2 position</param>
        /// <returns>A tile object</returns>
        private Tile GetAvailableTileDown(Vector2 position)
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

        /// <summary>
        /// The GetAvailableTileUp method will check if the
        /// next available tile up based on the position
        /// passed as input is a path, still within the maze.
        /// If the next tile is valid, then it will be returned
        /// and if not, a null tile will be returned
        /// </summary>
        /// <param name="position">A vector2 position</param>
        /// <returns>A tile object</returns>
        private Tile GetAvailableTileUp(Vector2 position)
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

        /// <summary>
        /// The GetAvailableTileLeft method will check if the
        /// next available tile left based on the position
        /// passed as input is a path, still within the maze.
        /// If the next tile is valid, then it will be returned
        /// and if not, a null tile will be returned
        /// </summary>
        /// <param name="position">A vector2 position</param>
        /// <returns>A tile object</returns>
        private Tile GetAvailableTileLeft(Vector2 position)
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

        /// <summary>
        /// The GetAvailableTileRight method will check if the
        /// next available tile right based on the position
        /// passed as input is a path, still within the maze.
        /// If the next tile is valid, then it will be returned
        /// and if not, a null tile will be returned
        /// </summary>
        /// <param name="position">A vector2 position</param>
        /// <returns>A tile object</returns>
        private Tile GetAvailableTileRight(Vector2 position)
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
