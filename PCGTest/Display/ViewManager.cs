using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace PCGTest.Display
{
    class ViewManager
    {
        GraphicsDevice _screen;
        ContentManager _content;
        List<IView> Views;

        public ViewManager(GraphicsDevice screen)
        {
            _screen = screen;
            Views = new List<IView>();
            // FIXME add a MapView here, replace with title menu
            Views.Add(new MapView(screen));
        }

        public void Update(GameTime gameTime)
        {
            foreach (var view in Views)
            {
                view.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var view in Views)
            {
                view.Draw(gameTime);
            }
        }

        internal void LoadContent(ContentManager content)
        {
            _content = content;
            foreach (var view in Views)
            {
                view.LoadContent(content);
            }
        }
    }
}
