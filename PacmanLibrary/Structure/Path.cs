using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary.Structure
{
    /// <summary>
    /// The Path class which implemenrs the Tile class 
    /// represents a type of tile in a maze of a pacman game.
    /// The Path class encapsulates the properties and behavior
    /// of a path in a maze which a pacman or a ghost object may 
    /// use to move around in a maze of a pacman game
    /// </summary>
    public class Path: Tile
    {
        //an ICollidable member that a path may contain
        private ICollidable member;

        /// <summary>
        /// The Path constructor will take as input a x and y 
        /// coordinate for its position and initializes it in the maze 
        /// and an ICollidable object which the path may contain. 
        /// </summary>
        /// <param name="x">int x position </param>
        /// <param name="y">int y position</param>
        /// <param name="member">an ICollidable object</param>
        public Path(int x, int y, ICollidable member) : base(x,y)
        {
            this.member = member;
        }

        /// <summary>
        /// Member Property method will get and set the
        /// ICollidable object that a specific Path can
        /// contain in a maze
        /// </summary>
        public override ICollidable Member
        {
            get{ return member; }
            set{this.member = value;}
        }

        /// <summary>
        /// CanEnter method will return a boolean true to represent 
        /// that every Path object can be entered any pacman or ghost
        /// object in a pacman game
        /// </summary>
        /// <returns></returns>
        public override bool CanEnter()
        {
            return true;
        }

        /// <summary>
        /// Collide method will invoke the collide 
        /// method of the ICollidable object that a
        /// Path object can contain but only if that 
        /// ICollidable object is null
        /// </summary>
        public override void Collide()
        {         
            member?.Collide();        
        }

        /// <summary>
        /// The IsEmpty method will check if the Path does
        /// not contain a ICollidable object and will return
        /// true if yes and false if the path is empty
        /// </summary>
        /// <returns>a boolean value</returns>
        public override bool IsEmpty()
        {
            return this.member == null ? false : true;
        }

        
    }
}
