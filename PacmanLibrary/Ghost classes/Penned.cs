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
        public Penned() { }

        public void Move()
        {
            throw new NotSupportedException();
        }
    }
}
