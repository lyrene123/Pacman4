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
    /// coordinates x and y and provide methods
    /// to retieve the position and the distance
    /// between vectors. It also provide abstract 
    /// methods to be implemented in concrete classes.
    /// </summary>
    public abstract class Tile
    {
        private Vector2 position;
       

        public Tile(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
        public Vector2 Position
        {
            get{ return position;}
            set{ position = value;}
        }
        public float GetDistance(Vector2 goal)
        {
            return Vector2.Distance(position, goal);
        }

        public abstract ICollidable Member();
        public abstract bool CanEnter();
        public abstract void Collide();
        public abstract bool IsEmpty();

    }

}
