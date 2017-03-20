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

        //to render
        private SpriteBatch spriteBatch;
        private Texture2D imageRedGhostLookUp;
        private Texture2D imageRedGhostLookDown;
        private Texture2D imageRedGhostLookRight;
        private Texture2D imageRedGhostLookLeft;
        private Texture2D imagePinkGhostLookUp;
        private Texture2D imagePinkGhostLookDown;
        private Texture2D imagePinkGhostLookRight;
        private Texture2D imagePinkGhostLookLeft;
        private Texture2D imageGreenGhostLookUp;
        private Texture2D imageGreenGhostLookDown;
        private Texture2D imageGreenGhostLookRight;
        private Texture2D imageGreenGhostLookLeft;
        private Texture2D imageYellowGhostLookUp;
        private Texture2D imageYellowGhostLookDown;
        private Texture2D imageYellowGhostLookRight;
        private Texture2D imageYellowGhostLookLeft;
        //private int counter;

        //Variable to manage animation
       
        float elapsed;
        float delay = 200f;
        int frames = 0;

        Rectangle destinationRect;

        Rectangle ghostSourceRect;
        /*Rectangle yellow_GhostSourceRect;
        Rectangle green_GhostSourceRect;
        Rectangle pink_GhostSourceRect;
        */
        double millisecondsPerFrame = 800; //Update every x second
        double timeSinceLastUpdate = 0; //Accumulate the elapsed time
        

        public TimeSpan TargetElapsedTime { get; private set; }

        enum MyColors
        {
            Red, Yellow, Pink, Green
        };
        enum MyDirections
        {
            Right, Left, Up, Down
        };

        public GhostsSprite(Game1 game) : base(game)
        {
            this.game = game;
            gs = game.gameState;
            
        }
        public override void Initialize()
        {
            destinationRect = new Rectangle((int)gs.Pacman.Position.X * 32, (int)gs.Pacman.Position.Y * 32, 32, 32);
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

            //GameState.Parse("levels.c");
            base.LoadContent();
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
                else
                {
                    frames++;

                }
                elapsed = 0;
            }

            ghostSourceRect = new Rectangle(32 * frames, 0, 32, 32);
            /*yellow_GhostSourceRect = new Rectangle(32 * frames, 0, 32, 32);
            pink_GhostSourceRect = new Rectangle(32 * frames, 0, 32, 32);
            green_GhostSourceRect = new Rectangle(32 * frames, 0, 32, 32);
            */

        }
        public override void Update(GameTime gameTime)
        {
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 1.0f);
            Animate(gameTime);
            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= millisecondsPerFrame)
            {
                timeSinceLastUpdate = 0;

                foreach (Ghost g in gs.GhostPack)
                {
                   g.Move();

                }
            }

            destinationRect = new Rectangle((int)gs.Pacman.Position.X * 32, (int)gs.Pacman.Position.Y * 32, 32, 32);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (gs.Score.Lives == 0)
            {
                LoadContent();
            }

            spriteBatch.Begin();
           
            foreach (Ghost g in gs.GhostPack)
            {
                if (g.ghostColor.ToString("G").Equals(MyColors.Red.ToString("G")))
                {
                    if (g.Direction.ToString("G").Equals(MyDirections.Right.ToString("G")))
                    {
                        spriteBatch.Draw(imageRedGhostLookRight, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Left.ToString("G")))
                    {
                        spriteBatch.Draw(imageRedGhostLookLeft, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Down.ToString("G")))
                    {
                        spriteBatch.Draw(imageRedGhostLookDown, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Up.ToString("G")))
                    {
                        spriteBatch.Draw(imageRedGhostLookUp, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                }
                else if (g.ghostColor.ToString("G").Equals(MyColors.Green.ToString("G")))
                {
                    if (g.Direction.ToString("G").Equals(MyDirections.Right.ToString("G")))
                    {
                        spriteBatch.Draw(imageGreenGhostLookRight, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Left.ToString("G")))
                    {
                        spriteBatch.Draw(imageGreenGhostLookLeft, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Down.ToString("G")))
                    {
                        spriteBatch.Draw(imageGreenGhostLookDown, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Up.ToString("G")))
                    {
                        spriteBatch.Draw(imageGreenGhostLookUp, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                }
                else if (g.ghostColor.ToString("G").Equals(MyColors.Pink.ToString("G")))
                {
                    if (g.Direction.ToString("G").Equals(MyDirections.Right.ToString("G")))
                    {
                        spriteBatch.Draw(imagePinkGhostLookRight, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Left.ToString("G")))
                    {
                        spriteBatch.Draw(imagePinkGhostLookLeft, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Down.ToString("G")))
                    {
                        spriteBatch.Draw(imagePinkGhostLookDown, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Up.ToString("G")))
                    {
                        spriteBatch.Draw(imagePinkGhostLookUp, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                }
                else if (g.ghostColor.ToString("G").Equals(MyColors.Yellow.ToString("G")))
                {
                    if (g.Direction.ToString("G").Equals(MyDirections.Right.ToString("G")))
                    {
                        spriteBatch.Draw(imageYellowGhostLookRight, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Left.ToString("G")))
                    {
                        spriteBatch.Draw(imageYellowGhostLookLeft, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect, Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Down.ToString("G")))
                    {
                        spriteBatch.Draw(imageYellowGhostLookDown, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32), ghostSourceRect,Color.White);
                    }
                    else if (g.Direction.ToString("G").Equals(MyDirections.Up.ToString("G")))
                    {
                        spriteBatch.Draw(imageYellowGhostLookUp, new Rectangle((int)g.Position.X * 32, (int)g.Position.Y * 32, 32, 32),ghostSourceRect, Color.White);
                    }
                }
              
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
