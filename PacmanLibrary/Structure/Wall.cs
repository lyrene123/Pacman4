using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    public class Wall: Tile
    {
        public Wall(int x, int y): base(x,y){}

        public override ICollidable Member
        {
            get{ throw new NotSupportedException();}
            set{throw new NotSupportedException();}
        }

        public override bool CanEnter()
        {
            return false;
        }

        public override void Collide()
        {
            throw new NotSupportedException();
        }

        public override bool IsEmpty()
        {
            throw new NotSupportedException();
        }
    }
}
