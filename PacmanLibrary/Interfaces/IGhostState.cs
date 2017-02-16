using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{
    /// <summary>
    /// The IGhostState interface sets the default properties
    /// and methods of one of the states of a ghost object in
    /// a game object
    /// @author Lyrene Labor
    /// @version Feb 2017
    /// </summary>
    interface IGhostState
    {
        /// <summary>
        /// The Move method will make the ghost make 
        /// according to its current state in a game of pacman
        /// </summary>
        void Move();
    }
}
