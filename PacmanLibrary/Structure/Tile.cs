using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PacmanLibrary
{
    /// <summary>
    /// Abstract Class
    /// </summary>
    public abstract class Tile
    {
        public Vector2 position;
       

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
        abstract ICollidable Member;
        
    }

}
