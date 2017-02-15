using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PacmanLibrary
{
    public class Tile
    {
        public Vector2 position;

        public Tile(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
       
        
    }

}
