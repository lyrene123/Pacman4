using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Ghost_classes
{
    public class GhostPack
    {
        private List<Ghost> ghosts;

        public GhostPack()
        {
            ghosts = new List<Ghost>();
        }
        public bool CheckCollideGhosts(Vector2 goal)
        {
            throw new NotSupportedException();
        }

    }
}
