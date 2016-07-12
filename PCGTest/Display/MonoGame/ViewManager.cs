using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace PCGTest.Display.MonoGame
{
    class ViewManager: IViewManager
    {
        GraphicsDevice _screen;
        public readonly int Width, Height;
        object _content; // ContentManager
        List<IView> Views;

        public ViewManager(GraphicsDevice screen, int width, int height)
        {
            Width = width;
            Height = height;
            _screen = screen;
            Views = new List<IView>();
        }

        public IMapView CreateMapView()
        {
            var view = new MapView(_screen, Width, Height);
            Add(view);
            return view;
        }

        public void Add(IView view)
        {
            Views.Add(view);
            view.LoadContent(_content);
        }

        public void Remove(IView view)
        {
            Views.Remove(view);
        }

        public void Update(int elapsed)
        {
            foreach (var view in Views)
            {
                view.Update(elapsed);
            }
        }

        public void Draw(int elapsed)
        {
            foreach (var view in Views)
            {
                view.Draw(elapsed);
            }
        }

        public void LoadContent(object contentSource)
        {
            _content = contentSource;
            foreach (var view in Views)
            {
                view.LoadContent(contentSource);
            }
        }
    }
}
