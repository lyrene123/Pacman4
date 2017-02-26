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
        public bool CheckCollideGhosts(Vector2 target)
        {
            foreach(Ghost monster in ghosts)
            {
                
            }
        }
        public void ResetGhosts()
        {
            foreach(Ghost monster in ghosts)
            {
                monster.Reset();
            }

        }
        public void ScareGhosts()
        {
            foreach(Ghost monster in ghosts)
            {
                monster.ChangeState(GhostState.Scared);
            }
        }
        public void Move()
        {
            foreach (Ghost monster in ghosts)
            {
                monster.Move();
            }
        }
        public void Add(Ghost g)
        {
            ghosts.Add(g);
        }
        
    }
}
