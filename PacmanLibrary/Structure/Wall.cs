using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    /// <summary>
    /// The Wall class which implements the Tile class 
    /// represents a type of tile in a maze of a pacman game.
    /// The Wall class encapsulates the properties and behavior
    /// of a wall in a maze
    /// 
    /// author: Daniel C
    /// version: Feb 2017
    /// </summary>
    public class Wall: Tile
    {
        /// <summary>
        /// The Wall no parameter constructor will initialize
        /// the position of the wall in the maze
        /// </summary>
        /// <param name="x">int x position</param>
        /// <param name="y">int y position</param>
        public Wall(int x, int y): base(x,y){}

        /// <summary>
        /// The Member property method will throw a
        /// NotSupportedException since a wall does not 
        /// contain any objects or ICollidable objects. 
        /// </summary>
        public override ICollidable Member
        {
            get{ throw new NotSupportedException();}
            set{throw new NotSupportedException();}
        }

        /// <summary>
        /// The CanEnter method will return a boolean false
        /// since a wall cannot be entered any other objects in
        /// a maze
        /// </summary>
        /// <returns>boolean false</returns>
        public override bool CanEnter()
        {
            return false;
        }

        /// <summary>
        /// The Collide will throw a NotSupportedException since
        /// a Wall should not react or do anything special when an 
        /// object collides into it in a maze
        /// </summary>
        public override void Collide()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The IsEmpty method will throw a NotSupportedException
        /// since a wall is never empty and in a pacman game, there
        /// is no purpose in checking if a wall is empty or not for 
        /// a wall does not have any special functionality and does 
        /// not contain any ICollidable object
        /// </summary>
        /// <returns>a boolean value</returns>
        public override bool IsEmpty()
        {
            throw new NotSupportedException();
        }
    }
}
