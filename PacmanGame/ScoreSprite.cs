using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacmanLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame
{
    /// <summary>
    /// The ScorePrite class takes care of displaying on the screen
    /// all necessary inforamtion about pacman during the game such as
    /// the lives and the total number of score and the message displaying
    /// to the user if the pacman has won or game is over while prompting the user
    /// if he or she wants to play again.
    /// </summary>
    public class ScoreSprite : DrawableGameComponent
    {
        private Game1 game;
        private GameState gs;
        private ScoreAndLives scores;
        private SpriteBatch spriteBatch;
        private bool isWon; //will keep track if pacman has won
        private SpriteFont font;
        private Texture2D lives;
        private int level1Score;
        private int level2Score;
        private int level3Score;
        //private int level4Score;
        //private int level5Score;
        private int currentScore;
        private int currentLives;

        /// <summary>
        /// The constructor will take as input a game1 object and will 
        /// initialize the data members such as the game1 object, the gamestate
        /// the scoreandlives object and the isWon to false
        /// </summary>
        /// <param name="game">A game1 object</param>
        public ScoreSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.gs = game.GameState;
            this.scores = gs.Score;
            isWon = false;

        }

        /// <summary>
        /// The property iswon method will set and return the
        /// value of the bool IsWon
        /// </summary>
        public bool IsWon
        {
            get { return this.isWon; }
            set { this.isWon = value; }
        }
        public int CurrentScore
        {
            get { return this.currentScore; }
            set { this.currentScore = value; }
        }
        public override void Initialize()
        {
            //Game1 newgame = new Game1();
            scores = game.GameState.Score;
            base.Initialize();
        }

        /// <summary>
        /// the loadcontent method will load the score sprite
        /// for the score font, the lives font and the spriteBatch
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = game.Content.Load<SpriteFont>("score");
            lives = game.Content.Load<Texture2D>("pacmanLive");
            base.LoadContent();

        }
        public override void Update(GameTime gameTime)
        {
            if (game.Level == 1)
            {
                if (scores.Score != 0)
                {
                    level1Score = scores.Score;
                }
                currentScore = level1Score;
            }
            else if (game.Level == 2)
            {
                if (scores.Score != 0)
                {
                    level2Score = scores.Score;
                }
                currentScore = level1Score + level2Score;
            }
            else if (game.Level == 3)
            {
                if (scores.Score != 0)
                {
                    level3Score = scores.Score;
                }
                currentScore = level1Score + level2Score + level3Score;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// the draw method will take care of displaying necessary information
        /// to the window such as the score, the lives and messages for game over
        /// and pacman won 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "SCORE: " + currentScore, new Vector2(0, 750), Color.White);
            spriteBatch.DrawString(font, "LEVEL: " + game.Level, new Vector2(600, 750), Color.White);
            spriteBatch.End();
            DisplayLives(gameTime);
            CheckWinOrLoss(gameTime);
            base.Draw(gameTime);
        }

        /// <summary>
        /// the displaylives method will display the right amount of pacman's head
        /// which represents the lives of pacman. Example, if pacman has 3 lives, 
        /// three pacman head will be displayed at the botom and so on
        /// </summary>
        /// <param name="gameTime">A gametime object</param>
        private void DisplayLives(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (scores.Lives == 3)
            {
                spriteBatch.Draw(lives, new Rectangle(400, 750, 32, 32), Color.White);
                spriteBatch.Draw(lives, new Rectangle(450, 750, 32, 32), Color.White);
                spriteBatch.Draw(lives, new Rectangle(500, 750, 32, 32), Color.White);
            }
            else if (scores.Lives == 2)
            {
                spriteBatch.Draw(lives, new Rectangle(450, 750, 32, 32), Color.White);
                spriteBatch.Draw(lives, new Rectangle(500, 750, 32, 32), Color.White);
            }
            else if (scores.Lives == 1)
            {
                spriteBatch.Draw(lives, new Rectangle(500, 750, 32, 32), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// CheckWinOrLoss method will display the approriate method for game over
        /// or pacman won
        /// </summary>
        /// <param name="gameTime">A gametime object</param>
        private void CheckWinOrLoss(GameTime gameTime)
        {
            if (this.game.IsGameOver && this.isWon == false)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "GAME OVER!", new Vector2(300, 780), Color.Red);
                spriteBatch.DrawString(font, "Press 'p' to play again", new Vector2(230, 800), Color.Red);
                spriteBatch.End();
            }

            if (this.game.IsGameOver && this.isWon == true)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "PACMAN WON!", new Vector2(300, 780), Color.Red);
                spriteBatch.DrawString(font, "Press 'p' to play again", new Vector2(230, 800), Color.Red);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
