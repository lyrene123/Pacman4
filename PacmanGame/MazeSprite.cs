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
        //to render
        private SpriteBatch spriteBatch;
        private Texture2D imageWall;
        private Texture2D imagePen;
        private Texture2D imagePenDoor;
        private Texture2D imageEnergizer;
        private Texture2D imagePellet;
        private Texture2D imageEmpty;
        private int frame_height;
        private int frame_width;
       
        
        private List<Wall> list;
       
        public TimeSpan TargetElapsedTime { get; private set; }

        public MazeSprite(Game1 game1) : base(game1)
        {
            this.game = game1;
            gs = game1.gameState;
            list = new List<Wall>();

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
            imagePen = game.Content.Load<Texture2D>("pen");
            imagePenDoor = game.Content.Load<Texture2D>("penDoor");
            imageEnergizer = game.Content.Load<Texture2D>("energizer");
            imagePellet = game.Content.Load<Texture2D>("pellet2");
            imageEmpty = game.Content.Load<Texture2D>("empty");
            
            
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (gs.Score.Lives == 0)
            {
                LoadContent();
            }
            
            spriteBatch.Begin();
            for (var i = 0; i < gs.Maze.Size; i++)
            {
                for (var j = 0; j < gs.Maze.Size; j++)
                {
                    if (gs.Maze[i, j] is Wall)
                    {
                        if (i == 8 && j >= 9 && j <= 12)
                        {
                            spriteBatch.Draw(imagePen, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                        }
                        else if (i == 14 && j >= 9 && j <= 12)
                        {
                            spriteBatch.Draw(imagePen, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                        }
                        else if (i >= 9 && i <= 14 && j == 9)
                        {
                            if (i == 12 || i == 11 || i == 10)
                            {
                                spriteBatch.Draw(imagePenDoor, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(imagePen, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                            }

                        }
                        else if (i >= 9 && i <= 14 && j == 12)
                        {
                            spriteBatch.Draw(imagePen, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(imageWall, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                        }
                        
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
                            spriteBatch.Draw(imageEnergizer, new Rectangle(i * frame_width, j * frame_height, 32, 32), Color.White);
                        }
                    }
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
  
