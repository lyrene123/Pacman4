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
    /// <summary>
    /// The MazeSprite class takes care of the maze structure
    /// such as wall, empty spaces, pellets and energizers.
    /// </summary>
    public class MazeSprite : DrawableGameComponent
    {
        GameState gs;
        Game1 game;
        //variables to manage images
        private SpriteBatch spriteBatch;
        private Texture2D imageWallLevel1;
        private Texture2D imageWallLevel2;
        private Texture2D imageWallLevel3;
        private Texture2D currentimageWall;
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

        /// <summary>
        /// The MazeSprite constructor will take as input a game1 object and will 
        /// initialize the data members such as the game object, the gamestate, 
        /// </summary>
        /// <param name="game">A game1 object</param>
        public MazeSprite(Game1 game1) : base(game1)
        {
            this.game = game1;
            gs = game1.GameState;
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
            base.Initialize();
        }
        /// <summary>
        /// The loadcontent method will load the images responsable 
        /// for the structure of the game(wall, energizer, pellet and empty space) and will also
        /// set the current image wall.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageWallLevel1 = game.Content.Load<Texture2D>("wall1");
            imageWallLevel2 = game.Content.Load<Texture2D>("wall2");
            imageWallLevel3 = game.Content.Load<Texture2D>("wall3");
            imageEnergizer = game.Content.Load<Texture2D>("energizer2");
            imagePellet = game.Content.Load<Texture2D>("pellet2");
            imageEmpty = game.Content.Load<Texture2D>("empty");

            currentimageWall = imageWallLevel1;
            gs = game.GameState;
            base.LoadContent();
        }

        /// <summary>
        /// The Update method Allows the game to run logic such as updating the 
        /// wall accordinly to the level and updating energizer animation while the game is running.
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        public override void Update(GameTime gameTime)
        {
            Animate(gameTime);
            if (game.Level == 1)
            {
                currentimageWall = imageWallLevel1;
            }
            else if (game.Level == 2)
            {
                currentimageWall = imageWallLevel2;
            }
            else if (game.Level == 3)
            {
                currentimageWall = imageWallLevel3;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// The Draw method will draw the images of all images related with
        /// the structure on the screen.
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
        public override void Draw(GameTime gameTime)
        {
            if (gs.Score.Lives >= 1 && game.WonGame == false)
            {
                spriteBatch.Begin();
                for (var i = 0; i < gs.Maze.Size; i++)
                {
                    for (var j = 0; j < gs.Maze.Size; j++)
                    {
                        if (gs.Maze[i, j] is Wall)
                        {
                            spriteBatch.Draw(currentimageWall, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
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
            }
            base.Draw(gameTime);
        }

        /// <summary>
        /// The Animate method will take care of all the animation,
        /// changing the appropriate frame from the source Rectangle 
        /// to give the ilusion of movements on the screen.
        /// </summary>
        /// <param name="gameTime">A GameTime Object</param>
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

