using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PCGTest.Display.MonoGame
{

    class MapView: IView, IMapView
    {
        GraphicsDevice _screen;
        SpriteBatch spriteBatch;
        SpriteMap mapSprites;
        SpriteMap decoSprites;
        SpriteMap itemSprites;
        SpriteMap charSprites;
        Rectangle tileRect;
        Dictionary<int, string> _tileKeys;
        int[] _tileMap;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }

        public MapView(GraphicsDevice screen, int width, int height)
        {
            _screen = screen;
            Width = width;
            Height = height;
            spriteBatch = new SpriteBatch(screen);
            _tileKeys = new Dictionary<int, string>();
        }

        public void LoadContent(object contentSource)
        {
            var content = (ContentManager)contentSource;
            mapSprites = SpriteMap.FromJson("Content/map.json");
            mapSprites.SetSpriteMap(content.Load<Texture2D>("map"));
            Size = mapSprites.Size;
            // Convert width/height from pixels to tiles
            Width /= Size;
            Height /= Size;
            tileRect = new Rectangle(0, 0, Size, Size);
            _tileMap = new int[Width * Height];
        }

        public void Update(int elapsed)
        {
            mapSprites.Update(elapsed);
        }

        public void Draw(int elapsed)
        {
            Rectangle dest = tileRect;
            string tile;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            for (var i=0; i < _tileMap.Count(); i++)
            {
                tile = _tileKeys[_tileMap[i]];
                dest.X = Size * (i % Width);
                dest.Y = Size * (i / Width);
                mapSprites.Draw(spriteBatch, tile, dest);
            }
            spriteBatch.End();
        }

        public void UpdateTileKeys(Dictionary<int, string> keys)
        {
            foreach(var key in keys.Keys.Except(_tileKeys.Keys))
            {
                // FIXME convert tile type map into all tile variations
                _tileKeys[key] = keys[key] + ".Middle";
            }
        }

        public void AddTileKey(int key, string value)
        {
            _tileKeys[key] = value;
        }

        public void RemoveTileKey(int key)
        {

        }

        public void UpdateMap(IEnumerable<int> map)
        {
            // FIXME preprocess map with tile solver to expand tile variants
            _tileMap = map.ToArray();
        }
    }
}
