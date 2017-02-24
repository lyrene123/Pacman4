using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading;
using PacmanLibrary.Structure;

namespace PacmanLibrary.Ghost_classes
{
    public delegate void PacmanDiedEventHandler();
    /// <summary>
    /// 
    /// </summary>
    public class Ghost : IMovable, ICollidable
    {
        private Pacman pacman;
        private Vector2 target;
        private Vector2 position;
        private Pen pen;
        private Maze maze;
        private Direction direction;
        private Color colour;
        private IGhostState currentState;
        private int points;

        public static Timer scared;
        public event CollisionEventHandler CollisionEvent;
        public event PacmanDiedEventHandler PacmanDiedEvent;

        public Ghost(GameState g, int x, int y, Vector2 target, GhostState start, Color c)
        {     
            this.pacman = g.pacmanObj;
            this.maze = g.mazeObj;
            this.pen = g.penObj;
            this.target = target;
            this.colour = c;
            this.position = new Vector2(x, y);

            if (start == GhostState.Scared)
                this.currentState = new Scared(this, this.maze);
            if (start == GhostState.Chasing)
                this.currentState = new Chase(this, this.maze, this.target, this.pacman);

            this.points = 200;
        }

        protected virtual void OnCollisionEvent(ICollidable member)
        {
            CollisionEvent?.Invoke(member);
        }

        protected virtual void OnPacmanDiedEvent()
        {
            PacmanDiedEvent?.Invoke();
            Reset();
        }


        public Direction Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        public Vector2 Position
        {
            get{ return this.position; }
            set{ this.position = value; }
        }

        public int Points
        {
            get{ return this.points; }
            set{ this.points = value; }
        }

        public void Move()
        {
            this.currentState.Move();
        }

        public void Collide()
        {
           if(this.CurrentState == GhostState.Scared)
            {
                OnCollisionEvent(this);
            }

           if(this.CurrentState == GhostState.Chasing)
            {
                OnPacmanDiedEvent();
            }
        }

        public void Reset()
        {

        }

        public GhostState CurrentState
        {
            get
            {
                if (this.currentState is Scared)
                    return GhostState.Scared;

                return GhostState.Chasing;
            }
        }

        public Color ghostColor
        {
            get { return this.colour;  }
        }

        public void ChangeState(GhostState g)
        {
            if (g == GhostState.Scared)
                this.currentState = new Scared(this, this.maze);
            if (g == GhostState.Chasing)
                this.currentState = new Chase(this, this.maze, this.target, this.pacman);

        }
    }
}
