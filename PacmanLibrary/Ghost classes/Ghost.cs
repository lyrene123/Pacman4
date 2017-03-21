using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Timers;
using PacmanLibrary.Structure;
using PacmanLibrary.Enums;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// The Ghost class encapsulates the behavior and properties
    /// of a ghost in a pacman game. The Ghost class implements the
    /// IMovable and ICollidable interfaces and therefore can move
    /// around the maze with a specific position and direction 
    /// and reacts to colliding with pacman. The Ghost's behaviors
    /// may vary depending on the state the Ghost is in during the game. 
    /// 
    /// author: Lyrene Labor
    /// version: Feb 2017
    /// </summary>

    public delegate void PacmanDiedEventHandler(); //delegate for the pacman died event

    public class Ghost : IMovable, ICollidable
    {
        private Pacman pacman;
        private Vector2 position;
        private Vector2 target;
        private int points;
        private Pen pen;
        private Maze maze;
        private Enums.Color colour;
        private IGhostState currentState;

        public static Timer scared;
        public static Vector2 ReleasePosition;
        public event CollisionEventHandler CollisionEvent; //event to encapsulate collision event
        public event PacmanDiedEventHandler PacmanDiedEvent; //event encapsulating pacman died event

        /// <summary>
        /// The Ghost constructor will take as input a GameState object 
        /// in order to get information about the pacman object, the maze 
        /// and the pen of the ghosts. The constructor will also initialize
        /// the position fo the ghost, its target relative to Pacman's position, 
        /// a ghoststate enum and a color of the ghost with the input with 
        /// the input values passed to the method. An exception will be thrown
        /// if the gamestate object passed is null or if the target's x or y
        /// values are negative
        /// </summary>
        /// <param name="g">A GameState object</param>
        /// <param name="x">the int x coordinates of the ghost position</param>
        /// <param name="y">the int y coordinates of the ghost position</param>
        /// <param name="target">the vector target of the ghost</param>
        /// <param name="start">the ghoststate enum of the ghost</param>
        /// <param name="c">the color of the ghost</param>
        public Ghost(GameState g, int x, int y, Vector2 target, GhostState start, Enums.Color c)
        {
            if (Object.ReferenceEquals(null, g))
                throw new ArgumentException("The input GameState object passed to the Ghost constructor must not be null");

            if (x < 0 || y < 0)
                throw new ArgumentException("The Ghost's x and y position must be negative");

            this.pacman = g.Pacman;
            this.maze = g.Maze;
            this.pen = g.Pen;
            this.target = target;
            this.colour = c;
            this.position = new Vector2(x, y);

            if (start == GhostState.Scared)
                this.currentState = new Scared(this, this.maze);
            if (start == GhostState.Chasing)
                this.currentState = new Chase(this, this.maze, this.target, this.pacman);

            this.points = 200; //default points set to 200
        }

        /// <summary>
        /// The OnCollisionEvent method will raise the CollisionEvent method
        /// by passing it a ICollidable object
        /// </summary>
        /// <param name="member">An ICollidable object</param>
        protected virtual void OnCollisionEvent(ICollidable member)
        {
            CollisionEvent?.Invoke(member);
        }

        /// <summary>
        /// OnPacmanDiedEvent method will raise the PacmanDied event
        /// </summary>
        protected virtual void OnPacmanDiedEvent()
        {
            PacmanDiedEvent?.Invoke();
        }

        /// <summary>
        /// The Direction property method gets and sets the current
        /// direction with an enum of the ghost object
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// The Position property method get and sets the current 
        /// vector position of the ghost object. An exception will be
        /// thrown if the x and y values are negative
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }

            set
            {
                Vector2 pos = value;
                if (pos.X < 0 || pos.Y < 0)
                    throw new ArgumentException("The X and Y position of a ghost must not be negative");
                this.position = pos;
            }

        }

        /// <summary>
        /// The Points property method gets and sets the point
        /// value of a ghost which will only be used when ghost
        /// collided with pacman during scared mode. An exception
        /// will be thrown if the value is passed is 0 or below
        /// </summary>
        public int Points
        {
            get { return this.points; }

            set
            {
                if (value <= 0)
                    throw new ArgumentException("The Ghost's point value must be larger than 0");
                this.points = value;
            }

        }

        /// <summary>
        /// CurrentState property will return the current state 
        /// of the ghost object
        /// </summary>
        public GhostState CurrentState
        {
            get
            {
                if (this.currentState is Scared)
                    return GhostState.Scared;

                return GhostState.Chasing;
            }
        }

        /// <summary>
        /// ghostColor property will return the current color
        /// of the ghost object
        /// </summary>
        public Enums.Color ghostColor
        {
            get { return this.colour; }
        }

        /// <summary>
        /// The Move method will call the ghost's current state 
        /// move method
        /// </summary>
        public void Move()
        {
            this.currentState.Move();
            CheckCollisions(this.pacman.Position);
        }


        /// <summary>
        /// CheckCollisions method will check if the current position
        /// of a ghost object has the same vector position as pacman and
        /// if so, then the collide method will be executed. An exception
        /// will be thrown if the target's x or y values are negative
        /// </summary>
        /// <param name="target">A Vector2 object</param>
        public void CheckCollisions(Vector2 target)
        {
            if (target.X < 0 || target.Y < 0)
                throw new ArgumentException("The vector position x and y position passed to the CheckCollisions method must not be negative");

            if (this.Position == target)
            {
                Collide();
            }
        }


        /// <summary>
        /// The Collide method will call the appropriate method
        /// that will raise the corresponding event when the ghost
        /// object has collided with pacman. 
        /// </summary>
        public void Collide()
        {
            if (this.CurrentState == GhostState.Scared)
            {
                OnCollisionEvent(this); //raise collision event to increment score of pacman
                this.pen.AddToPen(this); //add ghost back to pen 
            }
            if (this.CurrentState == GhostState.Chasing)
            {
                OnPacmanDiedEvent(); //raise pacman died event
            }
        }

        /// <summary>
        /// The Reset method will place the ghost object
        /// back to the pen
        /// </summary>
        public void Reset()
        {
            this.pen.AddToPen(this);
        }

        

        /// <summary>
        /// ChangeState method will take as input a GhostState enum
        /// and will change the state of the ghost object corresponding to 
        /// that input GhostState enum
        /// </summary>
        /// <param name="g">A ghoststate enum</param>
        public void ChangeState(GhostState g)
        {
            switch (g)
            {
                case GhostState.Scared:
                    this.currentState = new Scared(this, this.maze);
                    break;
                case GhostState.Chasing:
                    this.currentState = new Chase(this, this.maze, this.target, this.pacman);
                    break;
                case GhostState.Released:
                    this.Position = ReleasePosition;
                    if(this.CurrentState != GhostState.Scared)
                        this.currentState = new Chase(this, this.maze, this.target, this.pacman);
                    break;
                default:
                    this.currentState = new Chase(this, this.maze, this.target, this.pacman);
                    break;
            }
        }
    }
}
