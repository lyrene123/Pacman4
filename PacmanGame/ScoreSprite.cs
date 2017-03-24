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
    class ScoreSprite: DrawableGameComponent
    {
        private Game1 game;
        private GameState gs;
        private ScoreAndLives scores;
        private SpriteBatch spriteBatch;
        private bool isWon; //will keep track if pacman has won
        private SpriteFont font;
        private Texture2D lives;

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
            this.scores = this.gs.Score;
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
            spriteBatch.DrawString(font, "SCORE: " + this.scores.Score, new Vector2(0, 800), Color.White);
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
            if (this.scores.Lives == 3) 
            {
                spriteBatch.Draw(lives, new Rectangle(600, 800, 32, 32), Color.White);
                spriteBatch.Draw(lives, new Rectangle(650, 800, 32, 32), Color.White);
                spriteBatch.Draw(lives, new Rectangle(700, 800, 32, 32), Color.White);
            }
            else if (this.scores.Lives == 2)
            {
                spriteBatch.Draw(lives, new Rectangle(600, 800, 32, 32), Color.White);
                spriteBatch.Draw(lives, new Rectangle(650, 800, 32, 32), Color.White);
            }
            else if (this.scores.Lives == 1)
            {
                spriteBatch.Draw(lives, new Rectangle(600, 800, 32, 32), Color.White);
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
            if (this.game.IsGameOver)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "GAME OVER!", new Vector2(300, 780), Color.Red);
                spriteBatch.DrawString(font, "Press 'p' to play again", new Vector2(230, 800), Color.Red);
                spriteBatch.End();
            }

            if (this.isWon == true)
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
