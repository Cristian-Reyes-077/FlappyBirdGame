using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdGame.Clases
{
    public class Pipe
    {
        public const int FRONT_STATE = 0;
        public const int BACK_STATE = 1;
        public static GraphicsDeviceManager graphics;
        public static Texture2D topPipeTexture;
        public static Texture2D bottomPipeTexture;
        private Rectangle topPipeRectangle;
        private Rectangle bottonPipeRectangle;
        private Random random;
        private readonly int pipeHeight; //alto tuberia
        private readonly int pipeWidth;// largo tuberia
        private readonly int verticalDistanceBetween;//distancia entre tubos
        public static int horizontalDistanceBetween;
        private readonly int position;
        private readonly int position2;
        private int state;
        private bool destroyed;



        public Pipe()
        {
            random = new Random();
            state = FRONT_STATE;
            pipeHeight = 620;
            pipeWidth = 75;
            position = DefinePosition();
            verticalDistanceBetween = 200;
            horizontalDistanceBetween = 50;
            position2 = position + pipeHeight + verticalDistanceBetween;
            topPipeRectangle = new Rectangle(graphics.PreferredBackBufferWidth, position, pipeWidth, pipeHeight);
            bottonPipeRectangle = new Rectangle(graphics.PreferredBackBufferWidth, position2, pipeWidth, pipeHeight);
        }
        public void move()
        {
            bottonPipeRectangle.X -= 5;
            topPipeRectangle.X -= 5;
        }
        public int DefinePosition()
        {
            return random.Next(position - 570, -350);
        }

        public Rectangle TopPipeRectangle
        {
            get { return topPipeRectangle; }
            set { this.topPipeRectangle = value; }
        }

        public Rectangle BottonPipeRectangle
        {
            get { return bottonPipeRectangle; }
            set { this.bottonPipeRectangle = value; }
        }

        public int State
        {
            get { return state; }
            set { this.state = value; }
        }

        public bool Destroyed
        {
            get { return destroyed; }
            set { this.destroyed = value; }
        }
    }
}
