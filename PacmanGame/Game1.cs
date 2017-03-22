using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace PacmanGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private MazeSprite wall;
        private PacmanSprite pacman;
        private GhostsSprite ghosts;
        private ScoreSprite score;
        private GameState gameState;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private string content;
        private SoundEffect soundEffect;
        private SoundEffectInstance soundEffectInstance;
        List<SoundEffect> soundEffects;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 835;
            graphics.PreferredBackBufferWidth = 736;
            soundEffects = new List<SoundEffect>();
            //graphics.ToggleFullScreen();
            
            Content.RootDirectory = "Content";
            content = File.ReadAllText(@"levels.csv");
            gameState = GameState.Parse(content);

            this.gameState.Maze.PacmanWonEvent += GameEnded;
            this.gameState.Score.GameOver += GameEnded;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            wall = new MazeSprite(this);
            pacman = new PacmanSprite(this);
            ghosts = new GhostsSprite(this);
            score = new ScoreSprite(this);

            Components.Add(wall);
            Components.Add(pacman);
            Components.Add(ghosts);
            Components.Add(score);

            base.Initialize();
            

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundEffect = Content.Load<SoundEffect>("siren");
            soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.IsLooped = true;
            //soundEffectInstance.Play();

            //Sound Effects
            soundEffects.Add(Content.Load<SoundEffect>("pacman_chomp"));
            soundEffects.Add(Content.Load<SoundEffect>("Soundenergizer"));
            soundEffects.Add(Content.Load<SoundEffect>("pacmanDied"));

            content = File.ReadAllText(@"levels.csv");
            gameState = GameState.Parse(content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

             base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        public GameState GameState
        {
            get { return this.gameState; }

        }
        public SoundEffect this[int index]
        {
            get { return this.soundEffects[index]; }

        }

        private void GameEnded()
        {
            Components.Remove(pacman);
            Components.Remove(ghosts);
            if (this.gameState.Score.Lives > 0)
                this.score.IsWon = true;
        }
    
    }
}
