using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanLibrary
{
    public delegate void GameOverDelegate();
    public class ScoreAndLives
    {
        private GameState gameState;
        private int lives;
        private int score;
        public event GameOverDelegate GameOver;

        public ScoreAndLives (GameState gameState)
        {
            this.gameState = gameState;

        }
        public int Lives
        {
            get { return lives;  }
            set { lives = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        protected void OnGameOver()
        {
            GameOver?.Invoke();
        }

        public void deadPacman()
        {
            this.Lives -= 1;
            if(Lives == 0)
            {
                OnGameOver();
            }
        }
        void incrementScore(ICollidable member)
        {
            this.Score += member.Points;
        }

    }
}
