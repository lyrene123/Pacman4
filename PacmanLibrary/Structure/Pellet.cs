using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    /// <summary>
    /// The Pellet Class encapsulates the behavior of a Pellet.
    /// It keeps track of the points of a Pellet.
    /// It also implements the collide method in which raises the
    /// Collision Event. Every time Pacman eats a Pellet a method
    /// subscribed to it will be triggered. 
    /// 
    /// author: Daniel C
    /// version: Feb 2017
    /// </summary>
    public class Pellet : ICollidable
    {
        private int points;
        public event CollisionEventHandler CollisionEvent;
        /// <summary>
        /// The Pellet Constructor will initialize its points to 10
        /// by default until user decides to change a Pellet value with
        /// the Points property;
        /// </summary>
        public Pellet()
        {
            this.points = 10;
        }
        /// <summary>
        /// Points Property. Provides a flexible mechanism 
        /// to get and set the points of the Pellet. An ArgumentException
        /// will be thrown if the input is less than 0 
        /// </summary>
        public int Points
        {              
            get { return points; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("The points of a Pellet must have a value of 0 " +
                                                    "or above");
                points = value;
            }
        }
        /// <summary>
        /// The OnCollisionEvent method will raise the event CollisionEvent 
        /// which will call all methods or event handlers subscribed. When
        /// a pacman object collides with a Pellet object, the score of pacman
        /// should simply increment. An ArgumentException will be thrown if 
        /// the input is null
        /// </summary>
        /// <param name="x">A Pellet Object</param>
        protected virtual void OnCollisionEvent(Pellet x)
        {
            CollisionEvent?.Invoke(x);
        }
        /// <summary>
        /// The Collide method will call the OnCollisionEvent method.
        /// </summary>
        public void Collide()
        {
            OnCollisionEvent(this);           
        }

   }
}
