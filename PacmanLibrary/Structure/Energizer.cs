using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    public delegate 
    public class Energizer : ICollidable
    {
        private int points;
        //private GhostPack ghosts; // Class GhostPack  still to be created

        /*public Energizer(GhostPack ghosts) // Class GhostPack  still to be created
        {
            this.ghosts = ghosts;
        }
        */
        public int Points
        {
            get { return points; }
            set { points = value;}
        }

        public void Collide()
        {
            this.points += 30;
            //Invoke ScareGhosts() method from GhostPack?
        }
    }
}
