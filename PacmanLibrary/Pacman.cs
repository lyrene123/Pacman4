﻿using System;
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
    /// 
    /// Author: Lyrene L.
    /// Version: March 2017
    /// </summary>
    public class Pacman
    {
        private Vector2 position;
        private GameState gamestate;
        private Maze maze;
        private GhostPack ghostPack;
        public static Vector2 StartPos;
        private bool canMove;

        /// <summary>
        /// The constructor will use the input GameState
        /// to initialize its fields. An exception will be
        /// thrown if the object passed as input is null
        /// </summary>
        /// <param name="gs">A GameState object</param>
        public Pacman(GameState gs)
        {
            if (Object.ReferenceEquals(null, gs))
                throw new ArgumentException("The GameState object passed to Pacman must not be null");

            this.gamestate = gs;
            this.maze = this.gamestate.Maze;
            this.ghostPack = this.gamestate.GhostPack;
            canMove = true;
        }

        /// <summary>
        /// The Position property gets and sets
        /// the current vector position of pacman in a
        /// pacman game. An exception will be thrown if
        /// the x or y value position are negative
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }
            set
            {
                if (value.X < 0 || value.Y < 0)
                {
                    throw new ArgumentException("Vector can not be negative");
                }

                this.position = value;

            }
        }
        /// <summary>
        /// The property CanMove will set and return the
        /// bool canMove.
        /// </summary>
        public bool CanMove
        {
            get { return this.canMove; }
            private set { this.canMove = value; }
        }


        /// <summary>
        /// The Move method will take care of pacman's movements.
        /// This method will move one tile depending on the direction
        /// input passed to this method. The Move method will not allow
        /// any move that is backwards or will not let pacman go back
        /// to the previous tile it has been.
        /// </summary>
        /// <param name="dir">A Direction enum</param>
        public bool Move(Direction dir)
        {
            if (dir == Direction.Down)
            {
                if ((int)this.position.Y == this.maze.Size - 1)
                {
                    this.position.Y = 0;
                }
                if (!(this.maze[(int)this.position.X, (int)this.position.Y + 1] is Wall))
                {
                        this.position.Y++;
                        canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            else if (dir == Direction.Up)
            {
                if ((int)this.position.Y == 0)
                {
                    this.position.Y = this.maze.Size - 1;
                }
                if (!(this.maze[(int)this.position.X, (int)this.position.Y - 1] is Wall))
                {
                   
                        this.position.Y--;
                        canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            else if (dir == Direction.Left)
            {
                if ((int)this.position.X == 0)
                {
                    this.position.X = this.maze.Size - 1;
                }
                if (!(this.maze[(int)this.position.X-1, (int)this.position.Y] is Wall))
                {
                        this.position.X--;
                        canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            else if(dir == Direction.Right)
            {
                if ((int)this.position.X == this.maze.Size - 1)
                {
                    this.position.X = 0;
                }
                if (!(this.maze[(int)this.position.X+1, (int)this.position.Y] is Wall))
                {
                        this.position.X++;
                        canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            CheckCollisions();
            return canMove;
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
    }
}
