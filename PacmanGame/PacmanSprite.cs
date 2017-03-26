using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanLibrary;
using PacmanLibrary.Ghost_classes;

namespace PacmanGame
{
    /// <summary>
    /// The PacmanSprite class takes care of all possible
    /// movements of Pacman during the game. It also manages
    /// Pacman animation while moving around or after death.
    /// </summary>
    public class PacmanSprite : DrawableGameComponent
    {
        GameState gs;
        Game1 game;
        //variables to manage images
        private SpriteBatch spriteBatch;
        private Texture2D imgPacMoveRight;
        private Texture2D imgPacDied;
        private Texture2D imgPacMoveLeft;
        private Texture2D imgPacMoveDown;
        private Texture2D imgPacMoveUp;
        private Texture2D currentAnimation;

        private bool isDead;
        private bool playdead;

        //Variables to manage animation
        private int frame_height;
        private int frame_width;
        Rectangle destinationRect;
        Rectangle sourceRect;

        //Variable to manage animation and Keyboard Keys
        float elapsedTimeAnimation; //Accumulate the elapsed time to manage the frames of the sprite sheet.
        float delayAnimation = 200f;
        int images = 0;

        //Variables to manage Keyboard Input
        Keys[] keyArray;
        private Keys keyPressed;
        private KeyboardState currentKeyboardState;

        float elapsedDraw; //Accumulate the elapsed time for drawing animation
        float delayDraw = 2000f;

        //variables to manage Game Intro
        float elapsedTimeIntro;
        float delayTimeIntro = 4000f;
        private bool intro;

        // variable to manage loop animation

        double millisecondsPerFramePacman = 215; //Update every x second
        double timeSinceLastUpdatePacman = 0; //Accumulate the elapsed time for pacman movement
        /// <summary>
        /// The PacmanSprite constructor will take as input a game1 object and will 
        /// initialize the data members such as the game object, the gamestate, 
        /// the currentKeyboardState and the isDead and playdead to false.
        /// It will also add the PacmanDied method to all Ghosts's PacmanDiedEvent.
        /// </summary>
        /// <param name="game1">A game1 object</param>
        public PacmanSprite(Game1 game1) : base(game1)
        {
            this.game = game1;
            gs = game1.GameState;
            currentKeyboardState = new KeyboardState();
            this.isDead = false;
            playdead = false;
            intro = true;
            frame_height = 32;
            frame_width = 32;

            foreach (Ghost ghost in gs.GhostPack)
            {
                ghost.PacmanDiedEvent += PacmanDied;
            }
        }
        /// <summary>
        /// The loadcontent method will load the spriteSheets responsable 
        /// for each movement of Pacman (Right,Left,Down,Up) and will also
        /// assign the currentAnimation Texture2D Object to Pacman's spriteSheet's
        /// Right movement.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imgPacMoveRight = game.Content.Load<Texture2D>("imgPacMoveRight");
            imgPacMoveLeft = game.Content.Load<Texture2D>("imgPacMoveLeft");
            imgPacMoveUp = game.Content.Load<Texture2D>("imgPacMoveUp");
            imgPacDied = game.Content.Load<Texture2D>("imgpacdied");
            imgPacMoveDown = game.Content.Load<Texture2D>("imgPacMoveDown");

            currentAnimation = imgPacMoveRight;

            base.LoadContent();
        }
        /// <summary>
        /// The Update method Allows the game to run logic such as updating the 
        /// Keyboard key Pressed, checking for Pacman's collisions and updating 
        /// Pacman animations while the game is running.
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        public override void Update(GameTime gameTime)
        {
            if (!isDead)
            {
                AnimatePacman(gameTime, 1);
            }
            else
            {
                AnimatePacman(gameTime, 11);
                keyPressed = Keys.F1;
            }
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
                currentKeyboardState = Keyboard.GetState();
                keyArray = currentKeyboardState.GetPressedKeys();
                if (keyArray.GetLength(0) != 0)
                {
                    keyPressed = keyArray[0];
                }
                timeSinceLastUpdatePacman += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeSinceLastUpdatePacman >= millisecondsPerFramePacman)
                {
                    timeSinceLastUpdatePacman = 0;
                    CheckKeyPressed();
                    this.gs.Maze.CheckMembersLeft();
                }


            }
            destinationRect = new Rectangle((int)gs.Pacman.Position.X * frame_width, (int)gs.Pacman.Position.Y * frame_height, 32, 32);
            base.Update(gameTime);
        }
        /// <summary>
        /// The Draw method will draw the images of Pacman on the screen according to
        /// its current state such as moving or after a collision to any Ghost. 
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        public override void Draw(GameTime gameTime)
        {

            if (!isDead)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(currentAnimation, destinationRect,
                    sourceRect, Color.White);
                spriteBatch.End();
            }

            if (isDead)
            {
                if (playdead)
                {
                    game[2].Play();
                    playdead = false;
                }
                spriteBatch.Begin();
                spriteBatch.Draw(imgPacDied, destinationRect,
                sourceRect, Color.White);
                spriteBatch.End();
                elapsedDraw += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedDraw >= delayDraw)
                {
                    this.isDead = false;
                    this.gs.Pacman.Position = Pacman.StartPos;
                    elapsedDraw = 0;
                }
            }
            base.Draw(gameTime);
        }
        /// <summary>
        /// The CheckKeyPressed method will check for the value 
        /// that correspond to the keyboard key that has  
        /// been pressed and then assign the appropriate spriteSheet
        /// image to the currentAnimatio Object and move Pacman accordingly.
        /// </summary>
        private void CheckKeyPressed()
        {
            if (keyArray.Equals(null))
            {
                return;
            }
            else
            {
                if (!isDead)
                {
                    if (keyPressed.Equals(Keys.Right))
                    {
                        gs.Pacman.Move(Direction.Right);
                        currentAnimation = imgPacMoveRight;
                    }
                    else if (keyPressed.Equals(Keys.Left))
                    {
                        gs.Pacman.Move(Direction.Left);
                        currentAnimation = imgPacMoveLeft;
                    }
                    else if (keyPressed.Equals(Keys.Down))
                    {
                        gs.Pacman.Move(Direction.Down);
                        currentAnimation = imgPacMoveDown;
                    }
                    else if (keyPressed.Equals(Keys.Up))
                    {
                        gs.Pacman.Move(Direction.Up);
                        currentAnimation = imgPacMoveUp;
                    }
                }
            }
        }
        /// <summary>
        /// The PacmanDied method will be called automatically after 
        /// any Ghost's PacmanDiedEvent is raised. It will assign the 
        /// isDead and playdead variables to true.
        /// </summary>
        public void PacmanDied()
        {
            this.isDead = true;
            playdead = true;
        }
        /// <summary>
        /// The AnimatePacman method will take care of Pacman animation,
        /// changing the appropriate frame from the source Rectangle 
        /// to give the ilusion of movements on the screen.
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        /// <param name="frames">An integer</param>
        private void AnimatePacman(GameTime gameTime, int frames)
        {
            elapsedTimeAnimation += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTimeAnimation >= delayAnimation)
            {
                if (images >= frames)
                {
                    images = 0;

                }
                else
                {
                    images++;

                }
                elapsedTimeAnimation = 0;
            }

            sourceRect = new Rectangle(32 * images, 0, 32, 32);

        }
    }
}



