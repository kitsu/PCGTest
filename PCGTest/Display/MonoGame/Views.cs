using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Display.MonoGame
{
    public abstract class BaseView : IView
    {
        protected GraphicsDevice _screen;
        protected SpriteBatch spriteBatch;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }

        public BaseView(GraphicsDevice screen, int width, int height)
        {
            _screen = screen;
            Width = width;
            Height = height;
            spriteBatch = new SpriteBatch(screen);
        }

        abstract public void Draw(int elapsed);
        abstract public void LoadContent(object contentSource);
        abstract public void Update(int elapsed);
    }
}
