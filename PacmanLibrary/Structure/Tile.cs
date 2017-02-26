using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PacmanLibrary
{
    /// <summary>
    /// The Abstract Class Tile Initialize the 
    /// coordinates x and y location of a tile 
    /// in a maze during a pacman game and provide
    /// methods to retrieve the position and the
    /// distance between two tiles. It also provide 
    /// abstract methods to be implemented in 
    /// concrete classes.
    /// </summary>
    public abstract class Tile
    {
        private Vector2 position;//position of the tile
        private ICollidable member;//an ICollidable object a tile can contain

        /// <summary>
        /// The Tile constructor will initiliaze the
        /// position of the tile in a maze
        /// </summary>
        /// <param name="x">int x position</param>
        /// <param name="y">int y position</param>
        public Tile(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        /// <summary>
        /// The Position property will get and set 
        /// the position of a tile in a maze
        /// </summary>
        public Vector2 Position
        {
            get{ return position;}
            set{ position = value;}
        }

        /// <summary>
        /// The GetDistance method will return the
        /// distance between the current position of
        /// a tile and the tile passed as input to 
        /// the method
        /// </summary>
        /// <param name="goal"></param>
        /// <returns>a float value representing the distance 
        ///          between two tiles</returns>
        public float GetDistance(Vector2 goal)
        {
            return Vector2.Distance(position, goal);
        }

        //abtract members declaration to be implemented by derived classes
        public abstract ICollidable Member { get; set; }
        public abstract bool CanEnter();
        public abstract void Collide();
        public abstract bool IsEmpty();

    }

}
