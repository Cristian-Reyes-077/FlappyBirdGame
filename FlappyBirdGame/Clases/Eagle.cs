using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using System.Collections;

namespace FlappyBirdGame.Clases
{
    public class Eagle
    {

        public static GraphicsDeviceManager graphics;
        public static SoundEffect wingSound;
        private Texture2D[] texture2D;
        private Rectangle rectangle;
        private readonly int initX, initY, initWidth, initHeight;
        private int delayShoot;
        private bool isUp; //si va hacia arriba ono
        private ArrayList arrayFireBalls;


        public Eagle()
        {
            texture2D = new Texture2D[3];//Para las imagenes de la aguila
            arrayFireBalls = new ArrayList();
            initWidth = 60;
            initHeight = 45;
            delayShoot = 0;
            isUp = false;
            initX = (graphics.PreferredBackBufferWidth / 4) - (initWidth / 2); //dividimos la pantalla en 4 para poner el aguila un poco mas adelante del inicio de la pantalla
            initY = (4 * (graphics.PreferredBackBufferHeight / 10)) - (initHeight / 2); //dividimos el alto de la pantalla para poner el aguila a cierta altura del floor
                                                                                        //rectangle = new Rectangle(initX, initY, initWidth, initHeight);
            //rectangle = new Rectangle(initX, initY, 50, 70);
            rectangle = new Rectangle(initX, initY, initWidth, initHeight);
        }

        public void ResetPosition()
        {
            rectangle.X= initX; 
            rectangle.Y= initY;
        }

        public bool IsOnFloor()
        {
            bool isOnFloor = false || rectangle.Y > 485;
            return isOnFloor;
        }

        public void GoUp()
        {
            rectangle.Y -= 10;
        }

        public void GoDown()
        {
            rectangle.Y += 5;
        }

        public Texture2D[] Texture2Ds
        {
            get { return texture2D; }
            set { this.texture2D = value; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public bool IsUp
        {
            get { return isUp; }
            set { isUp = value; }
        }

        public int DelayShoot
        {
            get { return delayShoot; }
            set { delayShoot = value; }
        }

        public ArrayList ArrayFireBalls
        {
            get { return arrayFireBalls; }
        }

    }
}
