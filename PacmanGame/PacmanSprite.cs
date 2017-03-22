using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanLibrary;
using PacmanLibrary.Ghost_classes;

namespace PacmanGame
{
    public class PacmanSprite : DrawableGameComponent
    {
        GameState gs;
        Game1 game;
        //to render
        private SpriteBatch spriteBatch;
        private KeyboardState oldState;
        private Texture2D imgPacMoveRight;
        private Texture2D imgPacDied;
        private Texture2D imgPacMoveLeft;
        private Texture2D imgPacMoveDown;
        private Texture2D imgPacMoveUp;
        private Texture2D currentAnimation;
        private int frame_height;
        private int frame_width;
        private SoundEffect pacman_chomp;
        private bool isDead;

        //Variable to manage animation
        Rectangle destinationRect;
        Rectangle sourceRect;
        Rectangle sourceRectPacDied;
        float elapsed;
        float delay = 200f;
        int frames = 0;

        float elapsedDraw;
        float delayDraw = 2000f;

        // variable to manage loop animation
        private int counter;
        private int threshold = 0;
        double millisecondsPerFramePacman = 200; //Update every x second
        double timeSinceLastUpdatePacman = 0; //Accumulate the elapsed time
        
        public PacmanSprite(Game1 game1) : base(game1)
        {
            this.game = game1;
            gs = game1.GameState;
            this.isDead = false;
            foreach(Ghost ghost in gs.GhostPack)
            {
                ghost.PacmanDiedEvent += PacmanDied;
            }
        }

        public void PacmanDied()
        {
            this.isDead = true;
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
            imgPacMoveRight = game.Content.Load<Texture2D>("imgPacMoveRight");
            imgPacMoveLeft = game.Content.Load<Texture2D>("imgPacMoveLeft");
            imgPacMoveUp = game.Content.Load<Texture2D>("imgPacMoveUp");
            imgPacDied = game.Content.Load<Texture2D>("imgpacdied");
            imgPacMoveDown = game.Content.Load<Texture2D>("imgPacMoveDown");
            pacman_chomp = game.Content.Load<SoundEffect>("pacman_chomp");

            currentAnimation = imgPacMoveRight;
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
            sourceRect = new Rectangle(frame_width * frames, 0, 32, 32);

        }
        private void PacmanDiedAnimate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 1) { frames = 0; }
                else { frames++; }
                elapsed = 0;
            }

            sourceRectPacDied = new Rectangle(32 * frames, 0, 32, 32);

        }
        public override void Update(GameTime gameTime)
        {
            if (!isDead)
            {Animate(gameTime);}
            else{PacmanDiedAnimate(gameTime);}   

            timeSinceLastUpdatePacman += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdatePacman >= millisecondsPerFramePacman)
            {
                timeSinceLastUpdatePacman = 0;

                CheckInput();
                this.gs.Maze.CheckMembersLeft();
            }
            destinationRect = new Rectangle((int)gs.Pacman.Position.X * frame_width, (int)gs.Pacman.Position.Y * frame_height, 32, 32);
            base.Update(gameTime);
        }
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
                spriteBatch.Begin();
                spriteBatch.Draw(imgPacDied, destinationRect,
                sourceRectPacDied, Color.White);
                spriteBatch.End();
                elapsedDraw += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedDraw >= delayDraw)
                {
                    this.isDead = false;
                    elapsedDraw = 0;
                }
                   
            }
           
            base.Draw(gameTime);
        }
        private void CheckInput()
        {
            KeyboardState newState = Keyboard.GetState();
            if (!isDead)
            {
                if (newState.IsKeyDown(Keys.Right))
                {
                    currentAnimation = imgPacMoveRight;
                    // key Right has just been pressed.
                    if (!oldState.IsKeyDown(Keys.Right))
                    {
                        gs.Pacman.Move(Direction.Right);
                        counter = 0; 
                    }
                    else
                    {
                        counter++;
                        if (counter > threshold)
                            gs.Pacman.Move(Direction.Right);
                    }
                }

                else if (newState.IsKeyDown(Keys.Left))
                {
                    currentAnimation = imgPacMoveLeft;

                    // key Left has just been pressed.
                    if (!oldState.IsKeyDown(Keys.Left))
                    {
                        gs.Pacman.Move(Direction.Left);
                        counter = 0; 
                    }
                    else
                    {
                        counter++;
                        if (counter > threshold)
                            gs.Pacman.Move(Direction.Left);
                    }
                }
                else if (newState.IsKeyDown(Keys.Down))
                {
                    currentAnimation = imgPacMoveDown;

                    // key Down has just been pressed.
                    if (!oldState.IsKeyDown(Keys.Down))
                    {
                        gs.Pacman.Move(Direction.Down);
                        counter = 0; 
                    }
                    else
                    {
                        counter++;
                        if (counter > threshold)
                            gs.Pacman.Move(Direction.Down);
                    }
                }
                else if (newState.IsKeyDown(Keys.Up))
                {
                    currentAnimation = imgPacMoveUp;

                    // key Up has just been pressed.
                    if (!oldState.IsKeyDown(Keys.Up))
                    {
                        gs.Pacman.Move(Direction.Up);
                        counter = 0; 
                    }
                    else
                    {
                        counter++;
                        if (counter > threshold)
                            gs.Pacman.Move(Direction.Up);
                    }
                }
                //update old state.
                oldState = newState;
            }

        }

    }
             
}



