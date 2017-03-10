using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Timers;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// The GhostPack Class encapsulates a list of ghosts and each 
    /// ghost unique behavior acordingly with  the ghost state. 
    /// It also provides a method to reset all ghosts to its original
    /// position, a group collision detection acordingly with the 
    /// target. It also provides a method to add a new ghost to the list. 
    /// 
    /// author: Daniel C
    /// version: Feb 2017
    /// </summary>
    public class GhostPack : IEnumerable<Ghost>
    {
        private List<Ghost> ghosts;
        /// <summary>
        /// No-parameter Constructor to initialize the List of Ghosts
        /// to its default values. 
        /// </summary>
        public GhostPack()
        {
            ghosts = new List<Ghost>();
        }
        
        /// <summary>
        /// The CheckCollideGhosts method will check if any ghost in the
        /// list collided with Pacman(target).
        /// </summary>
        /// <param name="target">A Vector2 object to check collision with</param>
        /// <returns>It returns true if any ghost in the list is in the 
        ///           same position as the target</returns>
        public void CheckCollideGhosts(Vector2 target)
        {
            foreach (var monster in ghosts)
            {
                monster.CheckCollisions(target);
            }
        }
        /// <summary>
        /// The ResetGhosts method will invoke the Reset method provided
        /// in each ghost object in the list.
        /// </summary>
        public void ResetGhosts()
        {
            foreach (Ghost monster in ghosts)
            {
                monster.Reset();
            }

        }
        /// <summary>
        /// The ScareGhosts method will change the state of each ghost
        /// in the list to
        /// Scared mode.
        /// </summary>
        public void ScareGhosts()
        {
            foreach (Ghost monster in ghosts)
            {
                //if the ghosts are already in scared mode, don't do anything
                if (monster.CurrentState == GhostState.Scared)
                    return;

                monster.ChangeState(GhostState.Scared);
            }

            Ghost.scared = new Timer(6000);
            Ghost.scared.Enabled = true;
            Ghost.scared.Elapsed += OnScareGhostDisable;        
        }

        /// <summary>
        /// OnScareGhostDisable method stop the scared static timer
        /// of the ghost class and will change all ghost state
        /// to default chase mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnScareGhostDisable(object sender, ElapsedEventArgs e)
        {
            Timer t = (Timer)sender;
            t.Enabled = false;
            foreach (Ghost monster in ghosts)
            {
                monster.ChangeState(GhostState.Chasing);
            }
        }


        /// <summary>
        /// The move method will invoke the Move method provided in each
        /// ghost object in the list.
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

        public IEnumerator<Ghost> GetEnumerator()
        {
            return ghosts.GetEnumerator(); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ghosts.GetEnumerator();
        }
    }
}
