using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{
    /// <summary>
    /// The ICollidable interface sets the default properties
    /// and methods of an object considered as something that
    /// can cause a collision in a game of pacman.
    /// </summary>
    interface ICollidable
    {
        //add the collision event here

        /// <summary>
        /// The property Points will return the amount of 
        /// points made after a collision has occured
        /// </summary>
        int Points
        {
            get; set;
        }

        /// <summary>
        /// The Collide method is called when a collision has occured
        /// </summary>
        void Collide();
    }
}
