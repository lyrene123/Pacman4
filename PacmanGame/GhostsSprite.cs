using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacmanLibrary;
using PacmanLibrary.Ghost_classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame
{
    public class GhostsSprite : DrawableGameComponent
    {
        GameState gs;
        Game1 game;

        //variables to manage images
        private SpriteBatch spriteBatch;
        //variables to manage Blinky Ghost movement images
        private Texture2D imageRedGhostLookUp;
        private Texture2D imageRedGhostLookDown;
        private Texture2D imageRedGhostLookRight;
        private Texture2D imageRedGhostLookLeft;

        //variables to manage Pinky Ghost movement images
        private Texture2D imagePinkGhostLookUp;
        private Texture2D imagePinkGhostLookDown;
        private Texture2D imagePinkGhostLookRight;
        private Texture2D imagePinkGhostLookLeft;

        //variables to manage Inky Ghost movement images
        private Texture2D imageGreenGhostLookUp;
        private Texture2D imageGreenGhostLookDown;
        private Texture2D imageGreenGhostLookRight;
        private Texture2D imageGreenGhostLookLeft;

        //variables to manage Clyde Ghost movement images
        private Texture2D imageYellowGhostLookUp;
        private Texture2D imageYellowGhostLookDown;
        private Texture2D imageYellowGhostLookRight;
        private Texture2D imageYellowGhostLookLeft;
        private Texture2D imgScareGhosts;

        //Variable to manage animation
        private int frame_height;
        private int frame_width;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        Rectangle ghostSourceRect;


        float elapsedTimeIntro;
        float delayTimeIntro = 2800f;
        private bool intro;

        private double millisecondsPerFrame = 350; //Update every x second
        private double timeSinceLastUpdate = 0; //Accumulate the elapsed time
        public TimeSpan TargetElapsedTime { get; private set; }

        public GhostsSprite(Game1 game) : base(game)
        {
            this.game = game;
            gs = game.GameState;
            intro = true;

        }
        public override void Initialize()
        {
            frame_height = 32;
            frame_width = 32;
            gs = game.GameState;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            imageRedGhostLookUp = game.Content.Load<Texture2D>("red_GhostLookUp");
            imageRedGhostLookDown = game.Content.Load<Texture2D>("red_GhostLookDown");
            imageRedGhostLookLeft = game.Content.Load<Texture2D>("red_GhostLookLeft");
            imageRedGhostLookRight = game.Content.Load<Texture2D>("red_GhostLookRight");

            imageGreenGhostLookUp = game.Content.Load<Texture2D>("green_GhostLookUp");
            imageGreenGhostLookDown = game.Content.Load<Texture2D>("green_GhostLookDown");
            imageGreenGhostLookLeft = game.Content.Load<Texture2D>("green_GhostLookLeft");
            imageGreenGhostLookRight = game.Content.Load<Texture2D>("green_GhostLookRight");

            imageYellowGhostLookUp = game.Content.Load<Texture2D>("yellow_GhostLookUp");
            imageYellowGhostLookDown = game.Content.Load<Texture2D>("yellow_GhostLookDown");
            imageYellowGhostLookLeft = game.Content.Load<Texture2D>("yellow_GhostLookLeft");
            imageYellowGhostLookRight = game.Content.Load<Texture2D>("yellow_GhostLookRight");

            imagePinkGhostLookUp = game.Content.Load<Texture2D>("pink_GhostLookUp");
            imagePinkGhostLookDown = game.Content.Load<Texture2D>("pink_GhostLookDown");
            imagePinkGhostLookLeft = game.Content.Load<Texture2D>("pink_GhostLookLeft");
            imagePinkGhostLookRight = game.Content.Load<Texture2D>("pink_GhostLookRight");

            imgScareGhosts = game.Content.Load<Texture2D>("scare_Ghosts");
            gs = game.GameState;
            base.LoadContent();
        }
        private void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 1) { frames = 0; }
                else { frames++; }
                elapsed = 0;
            }
            ghostSourceRect = new Rectangle(frame_width * frames, 0, 32, 32);

        }
        public override void Update(GameTime gameTime)
        {
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 1.0f);
            Animate(gameTime);

            if (intro)
            {
                elapsedTimeIntro += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTimeIntro >= delayTimeIntro)
                {
                    elapsedTimeIntro = 0;
                    intro = false;
                }

            }
            else
            {
                timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeSinceLastUpdate >= millisecondsPerFrame)
                {
                    timeSinceLastUpdate = 0;

                    foreach (Ghost g in gs.GhostPack)
                    {
                        g.Move();
                    }
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (Ghost g in gs.GhostPack)
            {
                if (g.CurrentState == GhostState.Chasing || g.CurrentState == GhostState.Penned)
                {
                    spriteBatch.Begin();
                    if (g.ghostColor.Equals(PacmanLibrary.Enums.Color.Red))
                    {
                        if (g.Direction.Equals(PacmanLibrary.Direction.Right))
                        {
                            spriteBatch.Draw(imageRedGhostLookRight, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Left))
                        {
                            spriteBatch.Draw(imageRedGhostLookLeft, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Down))
                        {
                            spriteBatch.Draw(imageRedGhostLookDown, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Up))
                        {
                            spriteBatch.Draw(imageRedGhostLookUp, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                    }
                    else if (g.ghostColor.Equals(PacmanLibrary.Enums.Color.Green))
                    {
                        if (g.Direction.Equals(PacmanLibrary.Direction.Right))
                        {
                            spriteBatch.Draw(imageGreenGhostLookRight, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Left))
                        {
                            spriteBatch.Draw(imageGreenGhostLookLeft, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Down))
                        {
                            spriteBatch.Draw(imageGreenGhostLookDown, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Up))
                        {
                            spriteBatch.Draw(imageGreenGhostLookUp, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                    }
                    else if (g.ghostColor.Equals(PacmanLibrary.Enums.Color.Pink))
                    {
                        if (g.Direction.Equals(PacmanLibrary.Direction.Right))
                        {
                            spriteBatch.Draw(imagePinkGhostLookRight, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Left))
                        {
                            spriteBatch.Draw(imagePinkGhostLookLeft, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Down))
                        {
                            spriteBatch.Draw(imagePinkGhostLookDown, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Up))
                        {
                            spriteBatch.Draw(imagePinkGhostLookUp, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                    }
                    else if (g.ghostColor.Equals(PacmanLibrary.Enums.Color.Yellow))
                    {
                        if (g.Direction.Equals(PacmanLibrary.Direction.Right))
                        {
                            spriteBatch.Draw(imageYellowGhostLookRight, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Left))
                        {
                            spriteBatch.Draw(imageYellowGhostLookLeft, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Down))
                        {
                            spriteBatch.Draw(imageYellowGhostLookDown, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                        else if (g.Direction.Equals(PacmanLibrary.Direction.Up))
                        {
                            spriteBatch.Draw(imageYellowGhostLookUp, new Rectangle((int)g.Position.X * frame_width,
                                (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                        }
                    }
                    spriteBatch.End();
                }
                else if (g.CurrentState == GhostState.Scared)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(imgScareGhosts, new Rectangle((int)g.Position.X * frame_width,
                               (int)g.Position.Y * frame_height, 32, 32), ghostSourceRect, Color.White);
                    spriteBatch.End();
                }
            }

            base.Draw(gameTime);
        }
    }
}
