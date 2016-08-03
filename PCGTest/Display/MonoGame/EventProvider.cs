using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PCGTest.Display.MonoGame
{
    class EventProvider
    {
        private readonly Subject<Point> _click;
        public IObservable<Point> WhenClick;
        private readonly Subject<Keys[]> _keys;
        public IObservable<Keys[]> WhenKeyPressed;
        bool _down;

        public EventProvider()
        {
            _click = new Subject<Point>();
            WhenClick = _click.AsObservable();
            _keys = new Subject<Keys[]>();
            WhenKeyPressed = _keys.AsObservable();
        }

        public void Update(int elapsed)
        {
            var ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                _down = true;
            } else if (ms.LeftButton == ButtonState.Released && _down)
            {
                _down = false;
                _click.OnNext(ms.Position);
            }
            var ks = Keyboard.GetState().GetPressedKeys();
            if (ks.Count() > 0)
                _keys.OnNext(ks);
        }
    }
}
