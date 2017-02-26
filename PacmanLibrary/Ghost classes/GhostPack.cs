using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// The GhostPack Class encapsulates a list of ghosts and each ghost unique behavior acordingly with 
    /// the ghost state. It also provides a method to reset all ghosts to its original position, a group collision
    /// detection acordingly with the target. It also provides a method to add a new ghost to the list. 
    /// </summary>
    public class GhostPack
    {
        private List<Ghost> ghosts;
        /// <summary>
        /// No-parameter Constructor to initialize the List of Ghosts to its default values. 
        /// </summary>
        public GhostPack()
        {
            ghosts = new List<Ghost>();
        }
        /// <summary>
        /// The CheckCollideGhosts method will check if any ghost in the list collided with Pacman(target).
        /// </summary>
        /// <param name="target">A Vector2 object to check collision with</param>
        /// <returns>It returns true if any ghost in the list is in the same position as the target</returns>
        public bool CheckCollideGhosts(Vector2 target)
        {
            Boolean areEqual = false;
            foreach(Ghost monster in ghosts)
            {
                if(monster.Position == target)
                {
                    areEqual = true;
                }
            }
            return areEqual;
        }
        /// <summary>
        /// The ResetGhosts method will invoke the Reset method provided in each ghost object in the list.
        /// </summary>
        public void ResetGhosts()
        {
            foreach(Ghost monster in ghosts)
            {
                monster.Reset();
            }

        }
        /// <summary>
        /// The ScareGhosts method will change the state of each ghost in the list to
        /// Scared mode.
        /// </summary>
        public void ScareGhosts()
        {
            foreach(Ghost monster in ghosts)
            {
                monster.ChangeState(GhostState.Scared);
            }
        }
        /// <summary>
        /// The move method will invoke the Move method provided in each ghost object in the list.
        /// </summary>
        public void Move()
        {
            foreach (Ghost monster in ghosts)
            {
                monster.Move();
            }
        }
        /// <summary>
        /// The Add method Adds a ghost object to the end of the list.
        /// </summary>
        /// <param name="g">Ghost object</param>
        public void Add(Ghost g)
        {
            ghosts.Add(g);
        }
        
    }
}
