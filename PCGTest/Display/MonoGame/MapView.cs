using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace PCGTest.Display.MonoGame
{

    class MapView: BaseView, IMapView
    {
        SpriteMap mapSprites;
        SpriteMap decoSprites;
        SpriteMap itemSprites;
        SpriteMap charSprites;
        TileProvider _tiles;
        Rectangle tileRect;
        int[,] _tileMap;
        // Observables
        private readonly Subject<char> _move;
        public IObservable<char> WhenMove => _move.AsObservable();

        public MapView(GraphicsDevice screen, int width, int height):
            base(screen, width, height)
        {
            _move = new Subject<char>();
        }

        override public void LoadContent(object contentSource)
        {
            var assets = (AssetProvider)contentSource;
            mapSprites = assets.GetMapSprites();
            Size = mapSprites.Size;
            // Convert width/height from pixels to tiles
            Width /= Size;
            Height /= Size;
            tileRect = new Rectangle(0, 0, Size, Size);
            _tileMap = new int[Width, Height];
            _tiles = new TileProvider();
        }

        override public void Update(int elapsed)
        {
            mapSprites.Update(elapsed);
        }

        override public void Draw(int elapsed)
        {
            Rectangle dest = tileRect;
            string tile;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            for (var y = 0; y < Height; y++)
            {
                for (var x=0; x < Width; x++)
                {
                    tile = _tiles[_tileMap[x, y]];
                    dest.X = Size * x;
                    dest.Y = Size * y;
                    mapSprites.Draw(spriteBatch, tile, dest);
                }
            }
            spriteBatch.End();
        }

        public void AddTileKey(KeyValuePair<int, string> key)
        {
            _tiles.AddTileType(key);
        }

        public void RemoveTileKey(int key)
        {
        }

        public void UpdateMap(int[,] map)
        {
            _tileMap = _tiles.ResolveMap(map);
        }

        public void HandleInput(Keys[] pressed)
        {
            // Note this ordering incidentally imposes a direction preference
            if (pressed.Contains(Keys.K))
            {
                _move.OnNext('N');
            } else if (pressed.Contains(Keys.J))
            {
                _move.OnNext('S');
            } else if (pressed.Contains(Keys.H))
            {
                _move.OnNext('W');
            } else if (pressed.Contains(Keys.L))
            {
                _move.OnNext('E');
            }
        }
    }
}
