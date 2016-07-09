using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Display
{
    public struct TileSprite
    {
        // Animated indicates second frame is available
        // Flipped indicates second frame is shown
        public bool Animated, Flipped;
        // How long to show each side in milliseconds
        // and ellapsed time since last flip
        public int ForeDuration, TotalDuration, Ellapsed;
        // Front and back source rects
        public int Fore, Back;

        public TileSprite(int fore)
        {
            Animated = false;
            Flipped = false;
            ForeDuration = 0;
            TotalDuration = 0;
            Ellapsed = 0;
            Fore = fore;
            Back = fore;
        }
        public TileSprite(int fore, int foreDuration, int back, int totalDuration)
        {
            Animated = true;
            Flipped = false;
            ForeDuration = foreDuration;
            TotalDuration = totalDuration;
            Ellapsed = 0;
            Fore = fore;
            Back = back;
        }
    }

    class SpriteMap
    {
        Texture2D _spriteMap;
        int Width;
        int Height;
        int Size;
        public Dictionary<int, Rectangle> _rectCache;
        public Dictionary<string, TileSprite> Sprites;

        public static SpriteMap FromJson(string filename)
        {
            string json;
            using (var stream = TitleContainer.OpenStream(filename))
            {
                var sreader = new System.IO.StreamReader(stream);
                json = sreader.ReadToEnd();
            }
            var spriteMap = JsonConvert.DeserializeObject<SpriteMap>(json);
            return spriteMap;
        }

        public SpriteMap(int width, int height, int size = 16)
        {
            Width = width;
            Height = height;
            Size = size;
            _rectCache = new Dictionary<int, Rectangle>();
            Sprites = new Dictionary<string, TileSprite>();
        }
        public void SetSpriteMap(Texture2D spriteMap)
        {
            _spriteMap = spriteMap;
        }

        public void Update(GameTime gameTime)
        {
            TileSprite sprite;
            foreach (var key in Sprites.Keys.ToList())
            {
                sprite = Sprites[key];
                if (sprite.Animated)
                {
                    sprite.Ellapsed += gameTime.ElapsedGameTime.Milliseconds;
                    if (sprite.Ellapsed > sprite.TotalDuration)
                    {
                        sprite.Flipped = false;
                        sprite.Ellapsed = 0;
                    } else if (sprite.Ellapsed > sprite.ForeDuration)
                    {
                        sprite.Flipped = true;
                    }
                    Sprites[key] = sprite;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, string tile, Rectangle dest)
        {
            if (Sprites.ContainsKey(tile))
            {
                var sprite = Sprites[tile];
                var index = sprite.Fore;
                if (sprite.Flipped)
                    index = sprite.Back;
                var source = Index2Rect(index);
                spriteBatch.Draw(_spriteMap, dest, source, Color.White);
            }
        }

        public Rectangle Index2Rect(int index)
        {
            if (_rectCache.ContainsKey(index))
            {
                return _rectCache[index];
            }
            var y = index / Width;
            var x = index - (y * Width);
            var rect = new Rectangle(x * Size, y * Size, Size, Size);
            _rectCache[index] = rect;
            return rect;
        }
    }
}
