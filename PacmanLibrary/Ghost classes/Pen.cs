using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// The Pen represents the area where Ghosts go when the game starts, when they are eaten or when
    /// Pacman dies and they need to restart. The Pen releases Ghosts in a First-In-First-Out manner, after
    /// a time period has elapsed.
    /// </summary>
    public class Pen
    {
        private Queue<Ghost> ghosts; //fifo structure to release the appropriate ghost
        private List<Timer> timers; //multiple times since more than 1 Ghost may be in teh Pen
        private List<Tile> pen; //list of the Tiles that make up the Pen, so two ghosts aren't placed on teh same Tile

        /// <summary>
        /// Constructor instantiates the internal data structures to empty
        /// </summary>
        public Pen()
        {
            this.ghosts = new Queue<Ghost>();
            this.timers = new List<Timer>();
            pen = new List<Tile>();
        }

        /// <summary>
        /// This method add Tiles to the Pen area. It is meant to be invoked when the game is being
        /// initialized by the GameState.
        /// </summary>
        /// <param name="tile">a Tiles that is part of the Pen</param>
        public void AddTile(Tile tile)
        {
            pen.Add(tile);
        }

        /// <summary>
        /// Event handler for a Timer Elapsed event. Each time a Timer elapses,
        /// the first Ghost in the queue is dequeued and released, and the Timer is removed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Release(object sender, ElapsedEventArgs e)
        {
            Timer t = (Timer)sender;
            t.Enabled = false;
            Ghost g = ghosts.Dequeue();
            timers.Remove(t);
            g.ChangeState(GhostState.Released);
        }

        /// <summary>
        /// Every time a Ghost is added to the Pen (either at the beginning of the
        /// game when the game is being initialized, or every time the Ghost needs to be reset),
        /// it is enqueued. It's position is determined by the next unoccupied Tile in the Pen.
        /// A timer is started: the timer duration is based on how many ghosts are enqueued, so that 
        /// they are not all released at the same time.
        /// </summary>
        /// <param name="ghost"></param>
        public void AddToPen(Ghost ghost)
        {
            ghosts.Enqueue(ghost);
            ghost.Position = pen[ghosts.Count - 1].Position;
            Timer t = new Timer((ghosts.Count * 1000));
            t.Enabled = true;
            t.Elapsed += Release;
            timers.Add(t);
        }
    }
}

