using PacmanLibrary.Ghost_classes;
using PacmanLibrary.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{
    
    //for Amin
    public class GameState
    {
        Pacman pacman;
        Maze maze;
        Pen pen;
        public Pacman pacmanObj
        {
            get { return this.pacman; }
            private set { this.pacman = value; }
        }

        public Maze mazeObj
        {
            get { return this.maze; }
            private set { this.maze = value; }
        }

        public Pen penObj
        {
            get { return this.pen; }
            private set { this.pen = value; }
        }

    }
}
