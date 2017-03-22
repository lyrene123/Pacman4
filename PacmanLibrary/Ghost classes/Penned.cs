using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Ghost_classes
{
    public class Penned : IGhostState
    {
        private Vector2 position;
        private Ghost ghost;
        public Penned(Ghost g)
        {
            this.ghost = g;
        }

        public void Move()
        {
            this.position = ghost.Position;
        }
    }
}
