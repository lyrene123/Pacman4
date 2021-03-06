﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{
    public delegate void CollisionEventHandler(ICollidable member);
        
    /// <summary>
    /// The ICollidable interface sets the default properties
    /// and methods of an object considered as something that
    /// can cause a collision in a game of pacman.
    /// @author Lyrene Labor
    /// @version Feb 2017
    /// </summary>
    public interface ICollidable
    {
        //Collision event declaration
        event CollisionEventHandler CollisionEvent;

        /// <summary>
        /// The property Points will return the amount of 
        /// points made after a collision has occured
        /// </summary>
        int Points  { get; set; }

        /// <summary>
        /// The Collide method is called when a collision has occured
        /// </summary>
        void Collide();
    }
}
