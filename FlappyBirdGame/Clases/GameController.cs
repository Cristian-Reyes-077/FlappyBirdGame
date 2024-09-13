using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdGame.Clases
{
    public class GameController
    {
        public const int PLAY_STATE = 0;
        public const int LOSE_STATE = 1;
        public const int PAUSE_STATE = 2;

        MouseState previousMouseState;
        private int upDistance;
        private int horizontalDistanceCounter;
        private ArrayList arrayPipes;
        private int gameState;
        public static SoundEffect dieSound;
        public static SoundEffect hitSound;
        public static SoundEffect pauseSound;

        private int indexFrame = 0;
        private int score;
        private bool point;
        private string path;
        private int bestScore;
        private string path_tmp;

        public GameController()
        {
            path_tmp = @"%USERPROFILE%\\flappyBirdScore.txt";
            path = Environment.ExpandEnvironmentVariables(path_tmp);
            gameState = 0;
            score = 0;
            bestScore = ReadScore();
            point = true;
            previousMouseState = new MouseState();
            upDistance = 0;
            arrayPipes = new ArrayList();
        }

        public void MovePipes()
        {
            foreach (Pipe pipe in arrayPipes.ToArray())
            {
                // los movemos
                pipe.move();

                //se limpian
                if (pipe.TopPipeRectangle.Right < 0)
                {
                    arrayPipes.Remove(pipe);
                }
            }
        }

        public void VerifyIncreasedScore(Eagle eagle)
        {
            foreach (Pipe pipe in arrayPipes.ToArray())
            {
                if (pipe.State == Pipe.FRONT_STATE)
                {
                    if (point && eagle.Rectangle.X >= pipe.TopPipeRectangle.X && eagle.Rectangle.Y > pipe.TopPipeRectangle.Y && eagle.Rectangle.Y < pipe.BottonPipeRectangle.Y)
                    {
                        score++;
                        point = false;
                    }
                    if (eagle.Rectangle.X >= pipe.TopPipeRectangle.Right)
                    {
                        point = true;
                        pipe.State = Pipe.BACK_STATE;
                    }
                }
            }
        }

        public void AddPipes()
        {
            horizontalDistanceCounter++;
            if (horizontalDistanceCounter >= Pipe.horizontalDistanceBetween)
            {
                //si la distancia horizontal es mayor o igual a la distancia que debe haber entre los tubos
                arrayPipes.Add(new Pipe());
                //cada que se agregue una instancia el contador se va reseterar
                horizontalDistanceCounter = 0;
            }
        }

        public void RaiseBirdOnClick(Eagle eagle)
        {
            if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                eagle.IsUp = true;
                Eagle.wingSound.Play(); // sonar cuando se este elevando
            }
            previousMouseState = Mouse.GetState();

            if (upDistance < 10 && eagle.IsUp)
            {
                upDistance++;
                eagle.GoUp();
            }
            else
            {
                upDistance = 0;
                eagle.IsUp = false;
                eagle.GoDown();
            }
        }

        public void GetDownBirdAfterLose(Eagle eagle)
        {
            if (!eagle.IsOnFloor())
            {
                eagle.GoDown();
            }
        }
        public void VerifyForImpactPipe(Eagle eagle)
        {
            foreach (Pipe pipe in arrayPipes.ToArray())
            {
                if (eagle.Rectangle.Intersects(pipe.TopPipeRectangle) || eagle.Rectangle.Intersects(pipe.BottonPipeRectangle))
                {
                    hitSound.Play();
                    dieSound.Play();
                    gameState = LOSE_STATE;
                }
            }
        }

        public void VerifyIfLoseForFloorImpactPipe(Eagle eagle)
        {
            if (eagle.IsOnFloor())
            {
                hitSound.Play();
                gameState = LOSE_STATE;
            }
        }

        public void SetBestScore()
        {
            if (score > bestScore)
            {
                bestScore = score;
                SaveScore(bestScore);
            }
        }

        private int ReadScore()
        {
            // Open the file to read from.
            int sc = 0;
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        sc = Int32.Parse(s);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.Write(e.StackTrace);
                sc = 0;
            }
            return sc;
        }

        private void SaveScore(int scoreToSave)
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(scoreToSave);
            }
        }
        public int GetWingsBirdFrame(GameTime gameTime, Eagle eagle)
        {
            if ((int)gameTime.TotalGameTime.TotalMilliseconds % 100 == 0)
            {
                indexFrame++;
                if (indexFrame == eagle.Texture2Ds.Length)
                {
                    indexFrame = 0;
                }
            }
            return indexFrame;
        }
        public ArrayList ArrayPipes
        {
            get { return arrayPipes; }
            set { this.arrayPipes = value; }
        }
        public int BestScore
        {
            get { return bestScore; }
            set { this.bestScore = value; }
        }
        public int GameState
        {
            get { return gameState; }
            set { this.gameState = value; }
        }

        public int Score
        {
            get { return score; }
            set { this.score = value; }
        }
    }
}
