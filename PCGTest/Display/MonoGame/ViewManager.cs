using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using PCGTest.Director;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Display.MonoGame
{
    class ViewManager: IViewManager
    {
        GraphicsDevice _screen;
        EventProvider _events;
        public readonly int Width, Height;
        AssetProvider _content;
        List<IView> Views;
        Dictionary<IView, IDisposable> _handlers;

        public ViewManager(GraphicsDevice screen, int width, int height)
        {
            Width = width;
            Height = height;
            _screen = screen;
            _events = new EventProvider();
            _handlers = new Dictionary<IView, IDisposable>();
            Views = new List<IView>();
        }
        public IView CreateTitleView(TitleViewController ctrl)
        {
            var view = new TitleView(_screen, Width, Height);
            Add(view);
            // Bind events
            //ctrl.WhenFoo.Subscribe(view.Foo);
            var dispose = _events.WhenClick.Subscribe(pos => view.MenuSelect());
            _handlers[view] = dispose;
            view.WhenSelected.Subscribe(ctrl.ItemSelected);
            return view;
        }

        public IView CreateMapView(MapViewController ctrl)
        {
            var view = new MapView(_screen, Width, Height);
            Add(view);
            // Setup controller viewport
            var vp = ctrl.AddViewport(new Rect(0, 0, view.Width, view.Height));
            // Bind events
            ctrl.WhenAddTileKey.Subscribe(view.AddTileKey);
            vp.WhenMap.Subscribe(view.UpdateMap);
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
            if (_handlers.ContainsKey(view))
            {
                _handlers[view].Dispose();
            }
        }

        public void Update(int elapsed)
        {
            _events.Update(elapsed);
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
            _content = new AssetProvider((ContentManager)contentSource);
            foreach (var view in Views)
            {
                view.LoadContent(_content);
            }
        }
    }
}
