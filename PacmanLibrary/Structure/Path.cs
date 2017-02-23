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

        public override ICollidable Member
        {
            get{ return member; }
            set{this.member = value;}
        }

        public override bool CanEnter()
        {
            return true;
        }

        public override void Collide()
        {
            if (member != null)
            {
                member.Collide();
            }
                
        }

        public override bool IsEmpty()
        {
            if(member == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        
    }
}
