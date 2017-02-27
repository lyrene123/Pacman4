using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{
    /// <summary>
    /// The IGhostState interface sets the default properties
    /// and methods of the states of a ghost object in
    /// a pacman game 
    /// @author Lyrene Labor
    /// @version Feb 2017
    /// </summary>
    public interface IGhostState
    {
        /// <summary>
        /// The Move method will make the ghost move 
        /// according to its current state in a game of pacman
        /// </summary>
        void Move();
    }
}
