using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanLibrary;

namespace PacmanGame
{
    public class PacmanSprite : DrawableGameComponent
    {
        GameState gs;
        Game1 game;
        //to render
        private SpriteBatch spriteBatch;
        private KeyboardState oldState;
        private Texture2D imagePacman;
        private Texture2D imgPacman;
        private Texture2D imgPacMoveRight;
        private Texture2D imgPacMoveLeft;
        private Texture2D imgPacMoveDown;
        private Texture2D imgPacMoveUp;
        private Texture2D currentAnimation;
        private int frame_height;
        private int frame_width;

        //Variable to manage animation
        Rectangle destinationRect;
        Rectangle sourceRect;
        float elapsed;
        float delay = 200f;
        int frames = 0;

        // variable to manage loop animation
        private int counter;
        private int threshold = 0;
        double millisecondsPerFramePacman = 200; //Update every x second
        double timeSinceLastUpdatePacman = 0; //Accumulate the elapsed time
        

        enum MyDirections
        {
            Right, Left, Up, Down
        };

        public PacmanSprite(Game1 game1) : base(game1)
        {
            this.game = game1;
            gs = game1.gameState;
            
        }
        public override void Initialize()
        {
            frame_height = 28;
            frame_width = 32;
            destinationRect = new Rectangle((int)gs.Pacman.Position.X * 32, (int)gs.Pacman.Position.Y * 28, 32, 32);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imgPacMoveRight = game.Content.Load<Texture2D>("imgPacMoveRight");
            imgPacMoveLeft = game.Content.Load<Texture2D>("imgPacMoveLeft");
            imgPacMoveUp = game.Content.Load<Texture2D>("imgPacMoveUp");
            imgPacMoveDown = game.Content.Load<Texture2D>("imgPacMoveDown");

            currentAnimation = imgPacMoveRight;

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

            sourceRect = new Rectangle(frame_width * frames, 0, 32, 32);

        }
        public override void Update(GameTime gameTime)
        {
            Animate(gameTime);
            timeSinceLastUpdatePacman += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdatePacman >= millisecondsPerFramePacman)
            {
                timeSinceLastUpdatePacman = 0;

                checkInput();
            }

            destinationRect = new Rectangle((int)gs.Pacman.Position.X * frame_width, (int)gs.Pacman.Position.Y * frame_height, 32, 32);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (gs.Score.Lives == 0)
            {
                LoadContent();
            }

            spriteBatch.Begin();
            spriteBatch.Draw(currentAnimation, destinationRect, 
                sourceRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
     private void checkInput()
    {
        KeyboardState newState = Keyboard.GetState();
        if (newState.IsKeyDown(Keys.Right))
        {
            currentAnimation = imgPacMoveRight;
            // If not down last update, key has just been pressed.
            if (!oldState.IsKeyDown(Keys.Right))
            {
                gs.Pacman.Move(Direction.Right);
                counter = 0; //reset counter with every new keystroke
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
            // If not down last update, key has just been pressed.
            if (!oldState.IsKeyDown(Keys.Left))
            {
                gs.Pacman.Move(Direction.Left);
                counter = 0; //reset counter with every new keystroke
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
            // If not down last update, key has just been pressed.
            if (!oldState.IsKeyDown(Keys.Down))
            {
                gs.Pacman.Move(Direction.Down);
                counter = 0; //reset counter with every new keystroke
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
            // If not down last update, key has just been pressed.
            if (!oldState.IsKeyDown(Keys.Up))
            {
                gs.Pacman.Move(Direction.Up);
                counter = 0; //reset counter with every new keystroke
            }
            else
            {
                counter++;
                if (counter > threshold)
                    gs.Pacman.Move(Direction.Up);
            }
        }
        // Improve/change the code above to also check forKeys.Left
        // Once finished checking all keys, update old state.
        oldState = newState;
        }

    }
}



