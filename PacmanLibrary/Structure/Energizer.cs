﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanLibrary.Ghost_classes;

namespace PacmanLibrary.Structure
{
    /// <summary>
    /// The Energizer Class encapsulates the behavior of an Energizer.
    /// It keeps track of the points of the energizers.
    /// It also implements the collide method in which raises the
    /// Collision Event. Every time Pacman eats an Energizer a method
    /// subscribed to it will be triggered. The event will change the Ghost 
    /// State to Scared mode. 
    /// 
    /// author: Daniel C
    /// version: Feb 2017
    /// </summary>
    public class Energizer : ICollidable
    {
        public event CollisionEventHandler CollisionEvent;
        private int points;

        /// <summary>
        /// The Energizer constructor initializes its points to 100 by default
        /// until user decides to change the Energizer value with the points 
        /// property. Keeps track of all ghosts that was passed to its paramenter.
        /// An ArgumentException will be thrown if the input is null
        /// </summary>
        /// <param name="ghosts">GhostPack object. A list of Ghosts objects</param>
        public Energizer() 
        {
            this.points = 100;
        }
        /// <summary>
        /// Points Property. Provides a flexible mechanism 
        /// to get and set the points of the energizer.
        /// An ArgumentException will be thrown if 
        /// the input is less than 0
        /// </summary>
        public int Points
        {
            get { return points; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("The points of an Energizer must have a value of 0 " +
                                                    "or above");
                points = value;
            }
        }
        /// <summary>
        /// The OnCollisionEvent method will raise the event CollisionEvent 
        /// which will call all methods subscribed. When a pacman object
        /// collides with an Energizer object, all ghosts should turn
        /// into scared mode. An ArgumentException will be thrown if 
        /// the input is null
        /// </summary>
        /// <param name="x">An Energizer Object</param>
        protected virtual void OnCollisionEvent(Energizer x)
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
