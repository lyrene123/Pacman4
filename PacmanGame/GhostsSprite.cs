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
    /// <summary>
    /// The GhostsSprite class takes care of all possible
    /// movements of all Ghosts during the game. It also manages
    /// all Ghosts animation while moving around or after get scared.
    /// </summary>
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

        //Variables to manage animation
        private int frame_height;
        private int frame_width;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        Rectangle ghostSourceRect;

        //Variables to manage the intro sound elapsed time
        float elapsedTimeIntro;
        float delayTimeIntro = 2800f;
        private bool intro;

        //private int level;
        private bool newLevel;
        private double ghostSpeed; //Update every x second
        private double timeSinceLastUpdate; //Accumulate the elapsed time

        /// <summary>
        /// The GhostsSprite constructor will take as input a game1 object and will 
        /// initialize the data members such as the game object, the gamestate, 
        /// the newLevel and the intro to false.
        /// </summary>
        /// <param name="game">A game1 object</param>
        public GhostsSprite(Game1 game) : base(game)
        {
            this.game = game;
            gs = game.GameState;
            ghostSpeed = 520;
            timeSinceLastUpdate = 0;
            newLevel = false;
            intro = true;

        }
        /// <summary>
        /// The property GhostSpeed method will set and return the
        /// ghostSpeed of Pacman Game.
        /// </summary>
        public double GhostSpeed
        {
            get { return this.ghostSpeed; }
            set { this.ghostSpeed = value; }
        }
        /// <summary>
        /// The property NewLevel method will set and return the
        /// newLevel to indicate if is a new Level.
        /// </summary>
        public bool NewLevel
        {
            get { return this.newLevel; }
            set { this.newLevel = value; }
        }
        /// <summary>
        /// Allows the game to perform the initialization it needs to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  
        /// </summary>
        public override void Initialize()
        {
            frame_height = 32;
            frame_width = 32;
            gs = game.GameState;
            base.Initialize();
        }
        /// <summary>
        /// The loadcontent method will load the spriteSheets responsable 
        /// for each movement of the ghosts (Right,Left,Down,Up) and will also
        /// load the Scared Ghosts image.
        /// </summary>
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

        /// <summary>
        /// The Update method Allows the game to run logic such as updating the 
        /// ghosts movements and updating ghosts animations while the game is running.
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        public override void Update(GameTime gameTime)
        {
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
                if (timeSinceLastUpdate >= ghostSpeed)
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
        /// <summary>
        /// The Draw method will draw the images of all ghosts on the screen according to
        /// its current state such as moving or after an energizer is eaten from Pacman. 
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
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
        /// <summary>
        /// The Animate method will take care of all Ghosts animation,
        /// changing the appropriate frame from the source Rectangle 
        /// to give the ilusion of movements on the screen.
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
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
    }
}
