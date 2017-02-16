using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PacmanLibrary
{
    /// <summary>
    /// The IMovable interface sets the default properties 
    /// and methods that define a movable object in a game
    /// of pacman
    /// @author Lyrene Labor
    /// @version Feb 2017
    /// </summary>
    interface IMovable
    {
        Direction Direction
        {
            get; set;
        }

        /// <summary>
        /// The Position property returns the current position
        /// of the IMovable object and sets its position as well
        /// </summary>
        Vector2 Position
        {
            get; set;
        }

        /// <summary>
        /// The Move method is called whenever the IMovable 
        /// object makes a move
        /// </summary>
        void Move();
    }
}
