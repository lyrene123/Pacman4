using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    public delegate void PacmanWonEventHandler();
    public class Maze
    {
        private Tile[,] maze;
        public event PacmanWonEventHandler PacmanWonEvent;
        public Maze()
        {
            //??????????????
        }

        public void SetTiles(Tile[,] tiles)
        {
            this.maze = tiles;
        }

        protected virtual void PacmanWon()
        {
            PacmanWonEvent?.Invoke();
        }

        public Tile this[int x, int y]
        {
            get { return this.maze[x, y]; }
            set { this.maze[x, y] = value; }
        }

        public int Size
        {
            get { return this.maze.Length; }
        }




    }
}
