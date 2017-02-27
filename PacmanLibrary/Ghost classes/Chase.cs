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

        /// <summary>
        /// Four-parameter constructor to initialize the Chase state. 
        /// It requires a handle to the Ghost who is chasing Pacman
        /// as well as the Maze to know which tiles are available, a
        /// target which is relative to Pacman position as well as
        /// Pacman.
        /// </summary>
        /// <param name="ghost">Ghost Object</param>
        /// <param name="maze">Maze Object</param>
        /// <param name="target">Vector2 target which is relative to 
        ///                     Pacman position</param>
        /// <param name="pacman">Pacman Object</param>
        public Chase(Ghost ghost, Maze maze, Vector2 target, Pacman pacman)
        {
            //change direction 
          /*  switch (ghost.Direction)
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
            }*/
            this.ghost = ghost;
            this.maze = maze;
            this.target = target;
            this.pacman = pacman;
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
            List<Tile> tiles = new List<Tile>();
            tiles =  maze.GetAvailableNeighbours(ghost.Position, ghost.Direction);
            int num = tiles.Count;
            if (num == 0)
                throw new Exception("Nowhere to go");

            //set lowestDistance and closestTile relative to the first tile 
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
            if (tiles[(int)lowestDistance].Position.X == ghost.Position.X + 1)
                ghost.Direction = Direction.Right;
            else if (tiles[(int)lowestDistance].Position.X == ghost.Position.X - 1)
                ghost.Direction = Direction.Left;
            else if (tiles[(int)lowestDistance].Position.Y == ghost.Position.Y - 1)
                ghost.Direction = Direction.Up;
            else
                ghost.Direction = Direction.Down;

            //set new position
            ghost.Position = tiles[(int)lowestDistance].Position;
        }
        
     }
}
