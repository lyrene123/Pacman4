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

        public ScoreSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.gs = game.gameState;
            this.scores = this.gs.Score;
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
            spriteBatch.DrawString(font, "LIVES: " + this.scores.Lives, new Vector2(600, 800), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
