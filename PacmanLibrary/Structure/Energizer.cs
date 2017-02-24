using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    
    public class Energizer : ICollidable
    {
        public event CollisionEventHandler CollisionEvent;

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
        protected virtual void OnCollisionEvent(Energizer x)
        {
            CollisionEvent?.Invoke(x);
        }


        public void Collide()
        {
            this.points += 100;
            OnCollisionEvent(this);
            //Invoke ScareGhosts() method from GhostPack?
        }
    }
}
