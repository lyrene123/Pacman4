using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanLibrary;
using Microsoft.Xna.Framework;
using PacmanLibrary.Structure;
using PacmanLibrary.Ghost_classes;

namespace PacmanLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class Pacman
    {
        private Vector2 position;
        private GameState gamestate;
        private Maze maze;
        private GhostPack ghostPack;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gs"></param>
        public Pacman(GameState gs) 
        {
            this.gamestate = gs;
            this.maze = this.gamestate.Maze;
            this.position = this.gamestate.Pacman.position;
            this.ghostPack = this.gamestate.GhostPack;         
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        public void Move(Direction dir)
        {
            if(dir == Direction.Down)
            {
                if (MoveIfPossible(Direction.Down, new Vector2(this.position.X, this.position.Y + 1)))
                {
                    this.position.Y++;
                }
            }
            else if(dir == Direction.Up)
            {
                if (MoveIfPossible(Direction.Up, new Vector2(this.position.X, this.position.Y - 1)))
                {
                    this.position.Y--;
                }
            }
            else if(dir == Direction.Left)
            {
                if (MoveIfPossible(Direction.Left, new Vector2(this.position.X - 1, this.position.Y)))
                {
                    this.position.X--;
                }
            }
            else
            {
                if (MoveIfPossible(Direction.Right, new Vector2(this.position.X + 1, this.position.Y)))
                {
                    this.position.X++;
                }
            }

            CheckCollisions();
        }


        public void CheckCollisions()
        {
            //check tile collision
            if (this.maze[(int)this.position.X, (int)this.position.Y].IsEmpty())
            {
                this.maze[(int)this.position.X, (int)this.position.Y].Collide();
            }
            //ghost collision
            if (this.ghostPack.CheckCollideGhosts(this.position)){
                
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="nextPos"></param>
        /// <returns></returns>
        private bool MoveIfPossible(Direction dir, Vector2 nextPos)
        {
            List<Tile> freeTiles = this.maze.GetAvailableNeighbours(this.position, dir);
            foreach(var tile in freeTiles)
            {
                if(tile.Position == nextPos)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
