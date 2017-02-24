using Microsoft.Xna.Framework;
using PacmanLibrary.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanLibrary;

namespace PacmanLibrary.Ghost_classes
{
    /// <summary>
    /// 
    /// </summary>
    public class Chase : IGhostState
    {
        private Ghost ghost;
        private Maze maze;
        private Vector2 target;
        private Pacman pacman;

        public Chase(Ghost ghost, Maze maze, Vector2 target, Pacman pacman)
        {
            this.ghost = ghost;
            this.maze = maze;
            this.target = target;
            this.pacman = pacman;
        }
        
        public void Move()
        {
           
        }
        
    }
}
