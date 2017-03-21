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
    public class ScoreSprite : DrawableGameComponent
    {
        private SpriteFont font;
        private Game1 game;
        private GameState gs;
        private ScoreAndLives scores;
        private SpriteBatch spriteBatch;
        private bool isWon;

        public ScoreSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.gs = game.GameState;
            this.scores = this.gs.Score;
            isWon = false;
        }


        public bool IsWon
        {
            get { return this.isWon; }
            set { this.isWon = value; }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = game.Content.Load<SpriteFont>("scoreFont");
            base.LoadContent();
            //this.scores.ballObj.PaddleCollision += updateScore;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "SCORE: " + this.scores.Score, new Vector2(0, 800), Color.White);
            spriteBatch.DrawString(font, "LIVES: " + this.scores.Lives, new Vector2(650, 800), Color.White);
            spriteBatch.End();

            if (this.gs.Score.Lives <= 0)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "GAME OVER!", new Vector2(300, 800), Color.Red);
                spriteBatch.End();
            }

            if(this.gs.Score.Lives > 0 && this.isWon == true)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "PACMAN WON!", new Vector2(300, 800), Color.Red);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

         

    }
}
