using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    public class Path: Tile
    {
        private ICollidable member;

        public Path(int x, int y, ICollidable member) : base(x,y)
        {
            this.member = member;
        }
        public override ICollidable Member()
        {
            throw new NotImplementedException();
        }

        public override bool CanEnter()
        {
            throw new NotImplementedException();
        }

        public override void Collide()
        {
            throw new NotImplementedException();
        }

        public override bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        
    }
}
