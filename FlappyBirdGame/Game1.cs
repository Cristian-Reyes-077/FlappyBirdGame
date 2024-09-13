using FlappyBirdGame.Clases;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState previousMouseState;

        Texture2D birdTexture;
        Vector2 birdPosition;

        private GameController gameController;
        private SpriteFont scoreFont;
        private Scene scene;
        private Eagle eagle;
        private Items items;


        private int gameStateTmp;
        private int indexFrame;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                //Cambiamos dimenciones de la ventana
                PreferredBackBufferWidth = 1080,//ancho
                PreferredBackBufferHeight = 650//alto
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Scene.graphics = _graphics;//pasamos nuesto grapphos dvice de nuestra clase scene
            Eagle.graphics = _graphics;
            Pipe.graphics = _graphics;
            Items.graphics = _graphics;
            previousMouseState = new MouseState();
            gameController = new GameController();
            scene = new Scene();
            eagle = new Eagle();
            items = new Items();
            indexFrame = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //--Carga de img--//
            scene.BackgroundTexture = Content.Load<Texture2D>("SceneImages/background");
            scene.FloorTexture = Content.Load<Texture2D>("SceneImages/floor");
            eagle.Texture2Ds[0] = Content.Load<Texture2D>("Eagles/a1");
            eagle.Texture2Ds[1] = Content.Load<Texture2D>("Eagles/a2");
            eagle.Texture2Ds[2] = Content.Load<Texture2D>("Eagles/a3");
            Pipe.topPipeTexture = Content.Load<Texture2D>("Pipe/toppipe");
            Pipe.bottomPipeTexture = Content.Load<Texture2D>("Pipe/bottompipe");
            Items.clickTexture = Content.Load<Texture2D>("Items/click");
            Items.instructionsTexture = Content.Load<Texture2D>("Items/instructions");
            Items.pauseTexture = Content.Load<Texture2D>("Items/pause");
            Items.loseTexture = Content.Load<Texture2D>("Items/lose");
            //prueba
            birdTexture = Content.Load<Texture2D>("AguilaRealSF250");
            birdPosition = new Vector2(0, 0);

            //--Carga de sonidos--//
            Eagle.wingSound = Content.Load<SoundEffect>("Sounds/wing");
            GameController.dieSound = Content.Load<SoundEffect>("Sounds/die");
            GameController.hitSound = Content.Load<SoundEffect>("Sounds/hit");
            GameController.pauseSound = Content.Load<SoundEffect>("Sounds/pause");

            //--Cargar fuentes--//
            scoreFont = Content.Load<SpriteFont>("Fonts/ScoreFontTwo");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameController.GameState)
            {
                case GameController.PLAY_STATE:
                    scene.Move();//para mover la scena
                    gameController.RaiseBirdOnClick(eagle);
                    gameController.AddPipes();
                    gameController.MovePipes();
                    gameController.VerifyForImpactPipe(eagle);
                    gameController.VerifyIfLoseForFloorImpactPipe(eagle);
                    gameController.VerifyIncreasedScore(eagle);

                    // Mover alas
                    indexFrame = gameController.GetWingsBirdFrame(gameTime, eagle);

                    if (previousMouseState.RightButton == ButtonState.Released && Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameStateTmp = gameController.GameState;
                        gameController.GameState = GameController.PAUSE_STATE;
                        GameController.pauseSound.Play();
                    }
                    previousMouseState = Mouse.GetState();
                    break;
                case GameController.LOSE_STATE:
                    gameController.GetDownBirdAfterLose(eagle);
                    if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameController.GameState = GameController.PLAY_STATE;
                        gameController.ArrayPipes.Clear();
                        eagle.ResetPosition();
                        gameController.SetBestScore();
                        gameController.Score = 0;
                    }
                    previousMouseState = Mouse.GetState();
                    break;
                case GameController.PAUSE_STATE:
                    if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameController.GameState = GameController.PLAY_STATE;
                        GameController.pauseSound.Play();
                    }
                    previousMouseState =Mouse.GetState();
                        break;

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(birdTexture, birdPosition, Color.AliceBlue);
            _spriteBatch.Draw(scene.BackgroundTexture, scene.BackgroundRectangle, Color.White);//primero el fondo
            _spriteBatch.Draw(scene.BackgroundTexture, scene.BackgroundRectangle2, Color.White);//primero el fondo
            foreach (Pipe pipe in gameController.ArrayPipes)
            {
                _spriteBatch.Draw(Pipe.topPipeTexture, pipe.TopPipeRectangle, Color.White);
                _spriteBatch.Draw(Pipe.bottomPipeTexture, pipe.BottonPipeRectangle, Color.White);
            }
            _spriteBatch.Draw(scene.FloorTexture, scene.FloorRectangle, Color.White);
            _spriteBatch.Draw(scene.FloorTexture, scene.FloorRectangle2, Color.White);

            // pajaro patuano
            _spriteBatch.Draw(eagle.Texture2Ds[indexFrame], eagle.Rectangle, Color.White);

            // cuadro pause
            if (gameController.GameState == GameController.PAUSE_STATE)
            {
                _spriteBatch.Draw(Items.pauseTexture, items.PauseRectangle, Color.White);
            }

            // cuadro lose
            if (gameController.GameState == GameController.LOSE_STATE)
            {
                _spriteBatch.Draw(Items.loseTexture, items.LoseRectangle, Color.White);
                _spriteBatch.DrawString(scoreFont, gameController.Score.ToString(), new Vector2((_graphics.PreferredBackBufferWidth / 2) - (scoreFont.MeasureString(gameController.Score.ToString()).X / 2), 260), Color.GhostWhite);
                _spriteBatch.DrawString(scoreFont, gameController.BestScore.ToString(), new Vector2((_graphics.PreferredBackBufferWidth / 2) - (scoreFont.MeasureString(gameController.BestScore.ToString()).X / 2), 370), Color.GhostWhite);
            }

            _spriteBatch.DrawString(scoreFont, gameController.Score.ToString(), new Vector2((_graphics.PreferredBackBufferWidth / 2)-(scoreFont.MeasureString(gameController.Score.ToString())).X /2, 0), Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
