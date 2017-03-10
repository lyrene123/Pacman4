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
    /// The Pacman class encapsulates the behavior and 
    /// properties of a pacman object in a Pacman Game.
    /// The Pacman class will take care of its current 
    /// position movementand its collision with any 
    /// ghost during the game.
    /// </summary>
    public class Pacman
    {
        private Vector2 position;
        private GameState gamestate;
        private Maze maze;
        private GhostPack ghostPack;

        /// <summary>
        /// The constructor will use the input GameState
        /// to initialize its fields
        /// </summary>
        /// <param name="gs">A GameState object</param>
        public Pacman(GameState gs)
        {
            this.gamestate = gs;
            this.maze = this.gamestate.Maze;
            this.ghostPack = this.gamestate.GhostPack;
        }

        /// <summary>
        /// The Position property gets and sets
        /// the current vector position of pacman in a
        /// pacman game
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }
            set
            {
                if (value.X < 0)
                {
                    throw new ArgumentException("Vector can not be negative");
                }

                this.position = value;

            }
        }


        /// <summary>
        /// The Move method will take care of pacman's movements.
        /// This method will move one tile depending on the direction
        /// input passed to this method. The Move method will not allow
        /// any move that is backwards or will not let pacman go back
        /// to the previous tile it has been.
        /// </summary>
        /// <param name="dir">A Direction enum</param>
        public void Move(Direction dir)
        {
            if (dir == Direction.Down)
            {
                if (IsMovePossible(Direction.Down, new Vector2(this.position.X, this.position.Y + 1)))
                {
                    this.position.Y++;
                }
            }
            else if (dir == Direction.Up)
            {
                if (IsMovePossible(Direction.Up, new Vector2(this.position.X, this.position.Y - 1)))
                {
                    this.position.Y--;
                }
            }
            else if (dir == Direction.Left)
            {
                if (IsMovePossible(Direction.Left, new Vector2(this.position.X - 1, this.position.Y)))
                {
                    this.position.X--;
                }
            }
            else
            {
                if (IsMovePossible(Direction.Right, new Vector2(this.position.X + 1, this.position.Y)))
                {
                    this.position.X++;
                }
            }

            CheckCollisions();
        }

        /// <summary>
        /// The CheckCollisions method will check if pacman has 
        /// collided with a pellet or an energizer and will also
        /// check if pacman has collided with a ghost
        /// </summary>
        public void CheckCollisions()
        {
            //check non empty tile collision
            if (!this.maze[(int)this.position.X, (int)this.position.Y].IsEmpty())
            {
                this.maze[(int)this.position.X, (int)this.position.Y].Collide();
            }
            //ghost collision
            this.ghostPack.CheckCollideGhosts(this.position);
        }



        /// <summary>
        /// The IsMovePossible method will check if a specific 
        /// vector position is one of the possible movements
        /// that pacman can do based on the input Direction
        /// </summary>
        /// <param name="dir">A Direction enum</param>
        /// <param name="nextPos">A vector position</param>
        /// <returns>a boolean true if the next position is a valid move
        ///             or false otherwise</returns>
        private bool IsMovePossible(Direction dir, Vector2 nextPos)
        {
            List<Tile> freeTiles = this.maze.GetAvailableNeighbours(this.position, dir);
            foreach (var tile in freeTiles)
            {
                if (tile.Position == nextPos)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
