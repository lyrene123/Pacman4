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
        /// </summary>
        /// <param name="gameState"></param>
        public ScoreAndLives (GameState g)
        {
            this.gameState = g;
            this.lives = 3; //default
        }
        /// <summary>
        /// The Lives property gets and sets the lives of pacman.
        /// </summary>
        public int Lives
        {
            get { return this.lives;  }
            set { this.lives = value; }
        }
        /// <summary>
        /// The Score property gets and sets the score of the game.
        /// </summary>
        public int Score
        {
            get { return gameState.Score.Score; }
            set { gameState.Score.Score = value; }
        }
        /// <summary>
        /// OnGameOver method will raise the GameOver event
        /// </summary>
        protected void OnGameOver()
        {
            GameOver?.Invoke();
        }
        /// <summary>
        /// deadPacman method will invoke the OnGameOver method if 
        /// pacman looses all his lives or reset all Ghosts to the 
        /// initial position of the game if pacman dies and still have 
        /// remaining lives.
        /// </summary>
        public void deadPacman()
        {
            gameState.Score.Lives--;
            if(gameState.Score.Lives == 0)
            {
                OnGameOver();
            }
            else
            {
                this.gameState.GhostPack.ResetGhosts();
            }
        }
        /// <summary>
        /// The incrementScore method will increment the score every time 
        /// Pacman eats a Pellet or an energizer. If Pacman eats
        /// an energizer the ScareGhosts method will be invoked.
        /// </summary>
        /// <param name="member"></param>
        public void incrementScore(ICollidable member)
        {
            gameState.Score.Score += member.Points; //increment score

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
