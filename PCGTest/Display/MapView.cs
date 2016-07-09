using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PCGTest.Display
{

    class MapView : IView
    {
        GraphicsDevice _screen;
        SpriteBatch spriteBatch;
        SpriteMap mapSprites;
        SpriteMap decoSprites;
        SpriteMap itemSprites;
        SpriteMap charSprites;
        Rectangle tileRect;

        public MapView(GraphicsDevice screen)
        {
            _screen = screen;
            spriteBatch = new SpriteBatch(screen);
            tileRect = new Rectangle(0, 0, 16, 16);
        }

        public void LoadContent(ContentManager content)
        {
            mapSprites = SpriteMap.FromJson("Content/map.json");
            mapSprites.SetSpriteMap(content.Load<Texture2D>("map"));
        }

        public void Update(GameTime gameTime)
        {
            mapSprites.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            Rectangle dest = tileRect;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            for (var y = 0; y < 30; y++)
            {
                dest.X = 0;
                for (var x = 0; x < 50; x++)
                {
                    mapSprites.Draw(spriteBatch, "BrickPit.CyanWater.Middle", dest);
                    dest.X += 16;
                }
                dest.Y += 16;
            }
            spriteBatch.End();
        }
    }
}
