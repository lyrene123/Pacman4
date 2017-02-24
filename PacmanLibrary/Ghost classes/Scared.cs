using PacmanLibrary.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// The Scared class encapsulates the required behaviour when a Ghost is in scared state. The Ghost will
    /// change direction immediately upon instantiating the Scared state. Each move is subsequently randomly
    /// chosen from the available tiles.
    /// will
    /// </summary>
    public class Scared
    {
        private Ghost ghost;
        private Maze maze;

        /// <summary>
        /// Two-parameter constructor to initialize the Scared state. It requires a handle to the Ghost who is scared
        /// as well as the Maze to know which tiles are available.
        /// </summary>
        /// <param name="ghost"></param>
        /// <param name="maze"></param>
        public Scared(Ghost ghost, Maze maze)
        {
            //change direction - make a 180 degree turn
            switch (ghost.Direction)
            {
                case Direction.Up:
                    ghost.Direction = Direction.Down;
                    break;
                case Direction.Down:
                    ghost.Direction = Direction.Up;
                    break;
                case Direction.Right:
                    ghost.Direction = Direction.Left;
                    break;
                case Direction.Left:
                    ghost.Direction = Direction.Right;
                    break;
            }
            this.ghost = ghost;
            this.maze = maze;
        }

        /// <summary>
        /// This method is invoked to move the scared Ghost to the random available tile.
        /// Everytime a Ghost moves, we have to do two things: update the Ghost's Position
        /// and update the Ghosts's Direction. This indicates the direction in which it is moving, 
        /// and it is required to make sure that the Ghosts doesn't turn back to it's previous
        /// position (i.e., to avoid 180 degree turns) (used by the Maze class's GetAvailableNeighbours
        /// method)
        /// </summary>
        public void Move()
        {
            Tile current = maze[(int)ghost.Position.X, (int)ghost.Position.Y];
            List<Tile> places = maze.GetAvailableNeighbours(ghost.Position, ghost.Direction);
            int num = places.Count;
            if (num == 0)
                throw new Exception("Nowhere to go");

            Random rand = new Random();
            int choice = rand.Next(num);
            //determine direction
            if (places[choice].Position.X == ghost.Position.X + 1)
                ghost.Direction = Direction.Right;
            else if (places[choice].Position.X == ghost.Position.X - 1)
                ghost.Direction = Direction.Left;
            else if (places[choice].Position.Y == ghost.Position.Y - 1)
                ghost.Direction = Direction.Up;
            else
                ghost.Direction = Direction.Down;
            ghost.Position = places[choice].Position;
        }
    }
}
