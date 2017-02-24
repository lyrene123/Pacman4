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

        public static Timer scared;
        public event CollisionEventHandler CollisionEvent;

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



        }


        public Direction Direction
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 Position
        {
            get{ return this.position; }

            set{ this.position = value; }
        }

        public int Points
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Collide()
        {
            throw new NotImplementedException();
        }

        public void ChangeState(GhostState g)
        {

        }
    }
}
