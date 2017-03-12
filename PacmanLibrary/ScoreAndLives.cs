using PacmanLibrary.Ghost_classes;
using PacmanLibrary.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{
    public delegate void GameOverDelegate();
    /// <summary>
    /// The ScoreAndLives Class hods the properties of the lives of pacman
    /// and the score in a pacman game. It triggers a GameOver event if
    /// Pacman dies and increments the score every time pacman eats an
    /// energizer, a pellet or a Ghost(in Scared mode).
    /// </summary>
    public class ScoreAndLives
    {
        private GameState gameState;
        private int lives;
        private int score;
        public event GameOverDelegate GameOver;
        /// <summary>
        /// The one parameter constructor ScoreAndLives receives
        /// a GameState object and sets its members and will also
        /// set the number of lives of Pacman to 3 by default
        /// and initialize the total scores to 0. An exception
        /// will be thrown if the object passed is null
        /// </summary>
        /// <param name="gameState"></param>
        public ScoreAndLives (GameState g)
        {
            if (Object.ReferenceEquals(null, g))
                throw new ArgumentException("The GameState object passed to the ScoreAndLives constructor must not be null");
            this.gameState = g;
            this.lives = 3; //default
            this.score = 0;
        }
        /// <summary>
        /// The Lives property gets and sets the lives of pacman.
        /// An exception will be thrown if pacman's lives is negative
        /// </summary>
        public int Lives
        {
            get { return this.lives;  }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Pacman's lives value must not be negative");
                this.lives = value;
            }
        }
        /// <summary>
        /// The Score property gets and sets the score of the game by incrementing.
        /// An exeption will be thrown if the value is passed is negative
        /// </summary>
        public int Score
        {
            get { return this.score; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Pacman's score value must not be negative");
                this.score += value;
            }
        }
        /// <summary>
        /// OnGameOver method will raise the GameOver event
        /// </summary>
        protected void OnGameOver()
        {
            GameOver?.Invoke();
        }
        /// <summary>
        /// DeadPacman method will invoke the OnGameOver method if 
        /// pacman looses all his lives or reset all Ghosts to the 
        /// initial position of the game if pacman dies and still have 
        /// remaining lives.
        /// </summary>
        public void DeadPacman()
        {
            this.lives--;
            if(this.lives == 0)
            {
                OnGameOver();
            }
            else
            {
                this.gameState.GhostPack.ResetGhosts();
            }
        }
        /// <summary>
        /// The IncrementScore method will increment the score every time 
        /// Pacman eats a Pellet or an energizer. If Pacman eats
        /// an energizer the ScareGhosts method will be invoked.
        /// An exception will be thrown if the object passed
        /// is null
        /// </summary>
        /// <param name="member"></param>
        public void IncrementScore(ICollidable member)
        {
            if (Object.ReferenceEquals(null, member))
                throw new ArgumentException("The ICollidable member passed as input to the IncrementScore must not be null");

            this.score += member.Points; //increment score

            //everytime increment score, check if there are any pellets/energizers
            //left, if no more then a PacmanWon event will be raised
            this.gameState.Maze.CheckMembersLeft();
            if(member is Energizer)
            {
                this.gameState.GhostPack.ScareGhosts();
            }
        }

    }
}
