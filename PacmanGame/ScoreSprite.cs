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
       
        private Texture2D pacmanLives;
        private SpriteFont font;
        private Game1 game;
        private GameState gs;
        private ScoreAndLives scores;
        private SpriteBatch spriteBatch;
        Rectangle destinationRect;

        public ScoreSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.gs = game.GameState;
            this.scores = this.gs.Score;
        }
        public override void Initialize()
        {
            destinationRect = new Rectangle(600, 800, 32, 32);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pacmanLives = game.Content.Load<Texture2D>("pacmanLive");
            font = game.Content.Load<SpriteFont>("spritefont");

            base.LoadContent();
            //this.scores.ballObj.PaddleCollision += updateScore;
        }
        public override void Update(GameTime gameTime)
        {
            
            destinationRect = new Rectangle(600,800, 32, 32);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, "SCORE: " + this.scores.Score, new Vector2(0, 800), Color.White);
            spriteBatch.Draw(pacmanLives, destinationRect,Color.White);
            spriteBatch.End();

            if (this.gs.Score.Lives <= 0)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "GAME OVER!", new Vector2(300, 800), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

    }
}
