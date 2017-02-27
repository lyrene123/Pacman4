using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanLibrary;
using Microsoft.Xna.Framework;
using PacmanLibrary.Structure;

namespace PacmanLibrary
{
    //For Amin
    public class Pacman
    {
        private Vector2 position;
        private GameState gamestate;
        private Maze maze;

        public Pacman(GameState gs) 
        {
            this.gamestate = gs;
            this.maze = this.gamestate.Maze;
            this.position = this.gamestate.Pacman.position;           
        }

        public void Move(Direction dir)
        {
            if(dir == Direction.Down)
            {

            }
        }

        private void MoveDownIfPossible()


    }
}
