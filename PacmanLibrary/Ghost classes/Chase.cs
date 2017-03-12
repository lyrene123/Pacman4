using Microsoft.Xna.Framework;
using PacmanLibrary.Structure;
using System;
using System.Collections.Generic;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// The Chase class encapsulates the required behaviour 
    /// when a Ghost is in Chase state. On every move, ghost 
    /// will look at the available Tiles, and will choose the 
    /// one that is closest to its target (Vector2 target which
    /// is relative to Pacman position)
    /// 
    /// author: Daniel C
    /// version: Feb 2017
    /// </summary>
    public class Chase : IGhostState
    {
        private Ghost ghost;
        private Maze maze;
        private Vector2 target;
        private Pacman pacman;
        private int relativeDistance;

        /// <summary>
        /// Four-parameter constructor to initialize the Chase state. 
        /// It requires a handle to the Ghost who is chasing Pacman
        /// as well as the Maze to know which tiles are available, a
        /// target which is relative to Pacman position as well as
        /// Pacman. An exception will be thrown if the input ghost object is null,
        /// if the maze is null, if pacaman is null, if the target x and y values are 
        /// negative.
        /// </summary>
        /// <param name="ghost">Ghost Object</param>
        /// <param name="maze">Maze Object</param>
        /// <param name="target">Vector2 target which is relative to 
        ///                     Pacman position</param>
        /// <param name="pacman">Pacman Object</param>
        public Chase(Ghost ghost, Maze maze, Vector2 target, Pacman pacman)
        {
            if (Object.ReferenceEquals(null, ghost))
                throw new ArgumentException("The input Ghost object to the Chase Constructor must not be null");
            if (Object.ReferenceEquals(null, maze))
                throw new ArgumentException("The input Maze object to the Chase Constructor must not be null");
            if (Object.ReferenceEquals(null, pacman))
                throw new ArgumentException("The input Pacman object to the Chase Constructor must not be null");
            if(target.X < 0 || target.Y < 0)
                throw new ArgumentException("The input target X and Y positions to the Chase Constructor must not be negative");

            this.ghost = ghost;
            this.maze = maze;
            this.target = target;
            this.pacman = pacman;

            //generate a random number between 1 to 4 as a distance relative to pacman
            Random rnd = new Random();
            this.relativeDistance = rnd.Next(1, 5);     
        }

        /// <summary>
        /// This method is invoked to move the Ghost while 
        /// chasing Pacman to the closest available tile.
        /// Everytime a Ghost moves, we have to do two things:
        /// update the Ghost's Position and update the Ghosts's
        /// Direction. This indicates the direction in which it
        /// is moving, \ and it is required to make sure that 
        /// the Ghosts doesn't turn back to it's previous
        /// position (i.e., to avoid 180 degree turns) (used 
        /// by the Maze class's GetAvailableNeighbours
        /// method)
        /// </summary>
        public void Move()
        {
            float lowestDistance;
            List<Tile> tiles =  maze.GetAvailableNeighbours(ghost.Position, ghost.Direction);
            int num = tiles.Count;
            if (num == 0)
                throw new Exception("Nowhere to go");

            //update ghost's target depending on the new position of pacman
            this.target = new Vector2(this.relativeDistance, 0) + this.pacman.Position;
            
            //set lowestDistance and closestTile as the first tile 
            //in the list as a start
            lowestDistance = tiles[0].GetDistance(target);
            Tile closestTile = tiles[0];

            //determine the closest Tile
            foreach (Tile element in tiles)
            {
                if(element.GetDistance(target) < lowestDistance)
                {
                    lowestDistance = element.GetDistance(target);
                    closestTile = element;
                }          
            }

            //determine new direction
            if (closestTile.Position.X == ghost.Position.X + 1)
                ghost.Direction = Direction.Right;
            else if (closestTile.Position.X == ghost.Position.X - 1)
                ghost.Direction = Direction.Left;
            else if (closestTile.Position.Y == ghost.Position.Y - 1)
                ghost.Direction = Direction.Up;
            else
                ghost.Direction = Direction.Down;

            //set new position
            ghost.Position = closestTile.Position;
        }
        
     }
}
