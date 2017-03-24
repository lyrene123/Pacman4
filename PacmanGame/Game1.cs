using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanLibrary;
using PacmanLibrary.Ghost_classes;
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
        //variables for sprites
        private MazeSprite wall;
        private PacmanSprite pacman;
        private GhostsSprite ghosts;
        private ScoreSprite score;

        private GameState gameState;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private string content;

        //variables for sounds
        private SoundEffect backgroundMusic;
        private SoundEffectInstance backgroundSong;
        List<SoundEffect> soundEffects;
        
        float elapsed;
        float delay = 2000f;

        //variables to keep track if game has ended and if pacman has died
        private bool isDead;
        private bool isGameOver;

        /// <summary>
        /// The constructor will set the GraphicsDeviceManager, set the size of
        /// the window, set the sound effects list and will setup the game
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 835;
            graphics.PreferredBackBufferWidth = 736;
            soundEffects = new List<SoundEffect>();        
            SetupGame();
        }

        /// <summary>
        /// The setupgame method will load the 
        /// </summary>
        private void SetupGame()
        {
            Content.RootDirectory = "Content";
            content = File.ReadAllText(@"levels.csv");
            gameState = GameState.Parse(content);

            this.gameState.Maze.PacmanWonEvent += GameEnded;
            this.gameState.Score.GameOver += GameEnded;
            foreach(Ghost g in gameState.GhostPack)
            {
                g.PacmanDiedEvent += Pacman_Died;
            }

            isDead = false;
            isGameOver = false;
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

            //background song
            backgroundMusic = Content.Load<SoundEffect>("siren");
            backgroundSong = backgroundMusic.CreateInstance();
            backgroundSong.IsLooped = true;
            //backgroundSong.Play();

            //Sound Effects
            soundEffects.Add(Content.Load<SoundEffect>("pacman_chomp"));
            soundEffects.Add(Content.Load<SoundEffect>("Soundenergizer"));
            soundEffects.Add(Content.Load<SoundEffect>("pacmanDying"));
           

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
            if (isDead)
            {
                backgroundSong.Stop();
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsed >= delay)
                {
                    if(!isGameOver)
                    {
                        backgroundSong.Play();
                    }
                    elapsed = 0;
                    isDead = false;
                }
            }else
            {
                if (!isGameOver)
                    backgroundSong.Play();
            }
            CheckInput();
            base.Update(gameTime);
        }


        private void CheckInput()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.P) && this.score.IsWon || newState.IsKeyDown(Keys.P) && this.isGameOver)
            {
                Components.Remove(this.score);
                SetupGame();
                Initialize();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
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
            backgroundSong.Stop();
            isGameOver = true;        
        }
        public void Pacman_Died()
        {
            this.isDead = true;
        }

    }
}
