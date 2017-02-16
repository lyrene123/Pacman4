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
        private string colour;
        private IGhostState currentState;
        public static Timer scared;


       


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
