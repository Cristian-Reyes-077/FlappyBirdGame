using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdGame.Clases
{
    public class Scene
    {
        public static GraphicsDeviceManager graphics;
        private Texture2D backgroundTexture; //2d para el background
        private Texture2D floorTexture; //2d para el piso
        private Rectangle backgroundRectangle;
        private Rectangle floorRectangle;
        private Rectangle backgroundRectangle2;
        private Rectangle floorRectangle2;
        public Scene()
        {
            // Inicializar los rectangulos que son objetos normales y las texturas se inicializan en el loadcontent
            //--comenzaran al principio de la pantalla esq superior izq--//
            backgroundRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            floorRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            //--comenzaran al final de la pantalla esq superior der--//
            backgroundRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            floorRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        private void MoveBackground()
        {
            if (backgroundRectangle.X <= -graphics.PreferredBackBufferWidth)
                backgroundRectangle.X = backgroundRectangle2.Right;
            if(backgroundRectangle2.X <= -graphics.PreferredBackBufferWidth)
                backgroundRectangle2.X = backgroundRectangle.Right;

            backgroundRectangle.X -= 1;
            backgroundRectangle2.X -= 1;
        }
        
        public void Move()
        {
            MoveFloor();
            MoveBackground();

        }
        private void MoveFloor()
        {
            if (floorRectangle.X <= -graphics.PreferredBackBufferWidth)
                floorRectangle.X = floorRectangle2.Right;
            if(floorRectangle2.X <= -graphics.PreferredBackBufferWidth)
                floorRectangle2.X = floorRectangle.Right;

            //--Movemos el suelo--//
            floorRectangle.X -= 5;
            floorRectangle2.X -= 5;
        }

        public Texture2D BackgroundTexture
        {
            get { return backgroundTexture; }
            set { backgroundTexture = value; }
        }

        public Texture2D FloorTexture
        {
            get { return floorTexture; }
            set { floorTexture = value; }
        }

        public Rectangle BackgroundRectangle
        {
            get { return backgroundRectangle; }
            set { backgroundRectangle = value; }
        }

        public Rectangle FloorRectangle
        {
            get { return floorRectangle; }
            set { floorRectangle = value; }
        }

        public Rectangle BackgroundRectangle2
        {
            get { return backgroundRectangle2; }
            set { backgroundRectangle2 = value; }
        }

        public Rectangle FloorRectangle2
        {
            get { return floorRectangle2; }
            set { floorRectangle2 = value; }
        }

    }
}
