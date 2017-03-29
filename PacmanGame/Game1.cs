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
    /// This is the main type for the pacman game.
    /// The Game1 class will setup the whole pacman game
    /// draw the maze, the ghosts and the pacman on to the 
    /// game window. It will also load all classes from the business
    /// class and the loading of the sprites and will also take care
    /// of the collisions, pacman won and game over events
    /// </summary>
    public class Game1 : Game
    {
        //variables for sprites
        private MazeSprite wall;
        private PacmanSprite pacman;
        private GhostsSprite ghosts;
        private ScoreSprite score;

        //Variables to manage the Game State and graphics
        private GameState gameState;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private string content;

        //variables for sounds
        private SoundEffect backgroundMusic;
        private SoundEffectInstance backgroundSong;
        private SoundEffect IntroMusic;
        private SoundEffectInstance IntroSong;

        private SoundEffect gameOverMusic;
        private SoundEffectInstance gameOverSong;

        private SoundEffect energizerMusic;
        private SoundEffectInstance energizerSong;

        List<SoundEffect> soundEffects;

        //variables to manage Game Intro
        float elapsedTimeIntro;
        float delayTimeIntro = 4000f;
        private bool intro;

        //variables to manage Game Over
        float elapsedTimeGameOver;
        float delayTimeGameOver = 4000f;

        //variables to manage Pacman Dying Animation
        float elapsed;
        float delay = 2000f;

        //variables to keep track if game has ended and if pacman has died
        private bool isDead;
        private bool gameWon;
        private bool isGameOver;

        //Variables to manage Levels and keep track of currentScore
        private int level;


        /// <summary>
        /// The constructor will set the GraphicsDeviceManager, set the size of
        /// the window, set the sound effects list and will setup the game.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 736;
            soundEffects = new List<SoundEffect>();
            gameWon = false;
            level = 3;
            SetupGame();
        }
        /// <summary>
        /// The property Level method will set and return the
        /// level of Pacman Game.
        /// </summary>
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        /// <summary>
        /// The property WonGame method will set and return the
        /// gameWon of Pacman Game.
        /// </summary>
        public bool WonGame
        {
            get { return this.gameWon; }
            private set { this.gameWon = value; }
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

            //add sprites to components
            Components.Add(wall);
            Components.Add(pacman);
            Components.Add(ghosts);
            Components.Add(score);

            base.Initialize();
        }
        /// <summary>
        /// Allows the game to perform the initialization it needs for every level to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected void InitializeNextLevel()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            wall = new MazeSprite(this);
            pacman = new PacmanSprite(this);
            ghosts = new GhostsSprite(this);

            Components.Add(wall);
            Components.Add(pacman);
            Components.Add(ghosts);
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

            //background song
            IntroMusic = Content.Load<SoundEffect>("intro");
            IntroSong = IntroMusic.CreateInstance();

            //Game Over song
            gameOverMusic = Content.Load<SoundEffect>("Game_Over");
            gameOverSong = gameOverMusic.CreateInstance();
            gameOverSong.IsLooped = true;

            //Game Over song
            energizerMusic = Content.Load<SoundEffect>("msenergizer");
            energizerSong = energizerMusic.CreateInstance();
            energizerSong.IsLooped = true;

            //Sound Effects
            soundEffects.Add(Content.Load<SoundEffect>("pacman_chomp"));
            soundEffects.Add(Content.Load<SoundEffect>("Soundenergizer"));
            soundEffects.Add(Content.Load<SoundEffect>("pacmanDying"));
            soundEffects.Add(Content.Load<SoundEffect>("msghost"));
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
            //Condition to change the Ghosts speed accordinly to the level
            if (level == 2)
            {
                ghosts.GhostSpeed = 420;
            }
            if (level == 3)
            {
                ghosts.GhostSpeed = 320;
            }
            //Condition to manage the intro sound for the beginning of all levels.
            if (intro)
            {
                IntroSong.Play();
                elapsedTimeIntro += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTimeIntro >= delayTimeIntro)
                {
                    elapsedTimeIntro = 0;
                    intro = false;

                }
            }
            else
            {
                //Condition to manage the logic when pacman dies
                if (isDead)
                {
                    backgroundSong.Pause();

                    //wait for a couple of seconds to play the background song again, giving
                    //time to pacman to re-appear.
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= delay)
                    {
                        if (!isGameOver)
                        {
                            backgroundSong.Play(); //if pacman dies but still not Game Over
                        }
                        elapsed = 0;
                        isDead = false;
                    }
                }
                else
                {
                    if (!isGameOver) //if the game is not yet over, play background song again
                        backgroundSong.Play();
                    else if (isGameOver)
                    {
                        elapsedTimeGameOver += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (elapsedTimeGameOver >= delayTimeGameOver)
                        {
                            elapsedTimeGameOver = 0;
                            gameOverSong.Stop();
                            gameOverSong.Dispose();

                        }
                    }

                }
                //Checking when the ghosts are scared to play the energizer sound
                foreach (Ghost g in gameState.GhostPack)
                {
                    if (g.CurrentState == GhostState.Scared)
                    {
                        backgroundSong.Stop();
                        energizerSong.Play();

                    }
                    else
                    {
                        energizerSong.Stop();
                    }

                }

                CheckInput(); //check of keyboard input

            }

            base.Update(gameTime);
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

        /// <summary>
        /// The property gamestate method will return the gamestate object
        /// </summary>
        public GameState GameState
        {
            get { return this.gameState; }

        }

        /// <summary>
        /// The IsGameOver property method will return a bool value
        /// if game is over or not
        /// </summary>
        public bool IsGameOver
        {
            get { return this.isGameOver; }
        }

        /// <summary>
        /// The indexer method will return the soundeffect object from the list
        /// based on the input value as index. An exception will be raised if input
        /// is greater then list's count or less than 0
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SoundEffect this[int index]
        {
            get
            {
                if (index > this.soundEffects.Count || index < 0)
                    throw new IndexOutOfRangeException("The sound object you are trying to access cannot be retrieved");
                return this.soundEffects[index];
            }
        }

        /// <summary>
        /// The GameEnded method is the handler for the pacman won event
        /// and the game over event. In both cases, all ghosts and pacman
        /// will be removed from the window and setup the newLevel if pacman
        /// still have lives and is not in the final level.
        /// </summary>
        private void GameEnded()
        {

            Components.Remove(pacman);
            Components.Remove(ghosts);
            if (this.gameState.Score.Lives > 0)
            {
                if (level < 3)
                {
                    level++;
                    energizerSong.Stop();
                    backgroundSong.Stop();
                    SetupGame();
                    InitializeNextLevel();
                    ghosts.NewLevel = true;
                    ghosts.GhostSpeed -= 100;
                    isGameOver = false;
                }
                else
                {
                    score.IsWon = true;
                    gameWon = true;
                    energizerSong.Stop();
                    energizerSong.Dispose();
                    isGameOver = true;
                }

            }
            else
            {
                gameOverSong.Play();
                isGameOver = true;
                score.IsWon = false;
                energizerSong.Stop();
                energizerSong.Dispose();
                backgroundSong.Stop();
                backgroundSong.Dispose();
            }

            backgroundSong.Stop();

        }

        /// <summary>
        /// The pacman_died method is method handler for when pacman collides
        /// with a ghost and dies but may or may not have more lives left
        /// </summary>
        public void Pacman_Died()
        {
            this.isDead = true;
        }
        /// <summary>
        /// The GameEnded method is the handler for ghost Collision Event.
        /// It plays the Eating Ghost sound every time a ghost is eaten by
        /// Pacman.
        /// </summary>
        /// <param name="member"></param>
        public void PlayEatingGhost(ICollidable member)
        {
            soundEffects[3].Play();
        }
        /// <summary>
        /// The setupgame method will load the the game state, and will set up 
        /// all event handling
        /// </summary>
        private void SetupGame()
        {

            Content.RootDirectory = "Content";
            content = File.ReadAllText(@"levels.csv");
            gameState = GameState.Parse(content);

            this.gameState.Maze.PacmanWonEvent += GameEnded;
            this.gameState.Score.GameOver += GameEnded;
            foreach (Ghost g in gameState.GhostPack)
            {
                g.PacmanDiedEvent += Pacman_Died;
                g.CollisionEvent += PlayEatingGhost;

            }
            isDead = false;
            isGameOver = false;
            intro = true;
        }

        /// <summary>
        /// The checkinput method will check if the player pressed the key p
        /// to play again when game is over or player won
        /// </summary>
        private void CheckInput()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.P) && this.isGameOver)
            {
                level = 1;
                Components.Remove(this.score);
                gameOverSong.Stop();
                SetupGame();
                Initialize();
            }
        }
    }
}
