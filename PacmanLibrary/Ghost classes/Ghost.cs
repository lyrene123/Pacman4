using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// 
    /// </summary>
    class Ghost : IMovable, ICollidable
    {
        private Pacman pacman;
        private Vector2 target;
        //private Pen pen;
        //private Maze maze;
        private Direction direction;
        private Color colour;
        private IGhostState currentState;
        public static Timer scared;


       public Ghost(GameState g, int x, int y, Vector2 target, GhostState start, Color c)
        {
            //GET PACMAN FROM GAMESTATE G
            //GET MAZE FROM GAMESTATE G
            //GET PEN FROM GAMESTATE G
            this.target = target;
            this.colour = c;
            this.Position = new Vector2(x, y);

            if(start == GhostState.Scared)
            {
                //this.currentState =
            }

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
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
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
    }
}
