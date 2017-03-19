using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame
{
    public class Pacman : DrawableGameComponent
    {
        GameState gs;
        Game1 game;
        //to render
        private SpriteBatch spriteBatch;
        private KeyboardState oldState;
        private Texture2D imagePacman;
        private int counter;
        private int threshold = 0;
        double millisecondsPerFramePacman = 500; //Update every x second
        double timeSinceLastUpdatePacman = 0; //Accumulate the elapsed time
        public TimeSpan TargetElapsedTime { get; private set; }

        enum MyDirections
        {
            Right, Left, Up, Down
        };

        public Pacman(Game1 game1) : base(game1)
        {
            this.game = game1;
            gs = game1.gameState;
            
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imagePacman = game.Content.Load<Texture2D>("pacman");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            timeSinceLastUpdatePacman += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdatePacman >= millisecondsPerFramePacman)
            {
                timeSinceLastUpdatePacman = 0;
                checkInput();


            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (gs.Score.Lives == 0)
            {
                LoadContent();
            }

            spriteBatch.Begin();
            spriteBatch.Draw(imagePacman, new Rectangle((int)gs.Pacman.Position.X * 32, (int)gs.Pacman.Position.Y * 32, 32, 32), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
     private void checkInput()
    {
        KeyboardState newState = Keyboard.GetState();
        if (newState.IsKeyDown(Keys.Right))
        {
           
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

        if (newState.IsKeyDown(Keys.Left))
        {

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
        if (newState.IsKeyDown(Keys.Down))
        {

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
        if (newState.IsKeyDown(Keys.Up))
        {

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



