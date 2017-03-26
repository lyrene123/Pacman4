using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanLibrary;
using PacmanLibrary.Structure;
using System;
using System.Collections.Generic;
using System.IO;

namespace PacmanGame
{
    public class MazeSprite : DrawableGameComponent
    {
        GameState gs;
        Game1 game;
        //variables to manage images
        private SpriteBatch spriteBatch;
        private Texture2D imageWall;
        private Texture2D imageEnergizer;
        private Texture2D imagePellet;
        private Texture2D imageEmpty;

        //Variable to manage animation
        private int frame_height;
        private int frame_width;
        float elapsed;
        float delay = 200f;
        int frames;
        Rectangle sourceRect;


        public TimeSpan TargetElapsedTime { get; private set; }

        public MazeSprite(Game1 game1) : base(game1)
        {
            this.game = game1;
            gs = game1.GameState;
        }
        public override void Initialize()
        {
            frame_height = 32;
            frame_width = 32;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageWall = game.Content.Load<Texture2D>("wall");
            imageEnergizer = game.Content.Load<Texture2D>("energizer2");
            imagePellet = game.Content.Load<Texture2D>("pellet2");
            imageEmpty = game.Content.Load<Texture2D>("empty");
            gs = game.GameState;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Animate(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            for (var i = 0; i < gs.Maze.Size; i++)
            {
                for (var j = 0; j < gs.Maze.Size; j++)
                {
                    if (gs.Maze[i, j] is Wall)
                    {
                        spriteBatch.Draw(imageWall, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                    }

                    if (gs.Maze[i, j] is PacmanLibrary.Structure.Path)
                    {
                        if (gs.Maze[i, j].IsEmpty())
                        {
                            spriteBatch.Draw(imageEmpty, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                        }
                        if (gs.Maze[i, j].Member is Pellet)
                        {
                            spriteBatch.Draw(imagePellet, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                        }
                        if (gs.Maze[i, j].Member is Energizer)
                        {
                            spriteBatch.Draw(imageEnergizer, new Rectangle(i * frame_width, j * frame_height, 32, 32), sourceRect, Color.White);
                        }
                    }
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 1)
                {
                    frames = 0;
                }
                else { frames++; }
                elapsed = 0;
            }
            sourceRect = new Rectangle(32 * frames, 0, 32, 32);

        }
    }
}

