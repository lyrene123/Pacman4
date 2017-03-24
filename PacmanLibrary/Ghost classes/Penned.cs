using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// The Penned encapsulated the behavior of a ghost when
    /// it is inside the pen. It implements the IGhostState interface
    /// </summary>
    public class Penned : IGhostState
    {
        /// <summary>
        /// No param contructor simply constructs the Penned object
        /// </summary>
        public Penned() { }

        /// <summary>
        /// The move simply throws a NotSupportedException exception
        /// since a ghost will normally not move inside the pen nor chase
        /// the ghost
        /// </summary>
        public void Move()
        {
            throw new NotSupportedException();
        }
    }
}
