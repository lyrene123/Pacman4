using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    public class Pellet : ICollidable
    {
        private int points;
        public int Points
        {
                      
            get { return points; }
            set { points = value; }
        }

        public void Collide()
        {
            this.points += 30;
        }
    }
}
