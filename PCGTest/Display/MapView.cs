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
            var key = new Dictionary<char, string>() {
                { '~', "BrickPit.CyanWater.Middle" },
                { '0', "BrickPit.CyanWater.OutTopRight" },
                { '1', "BrickPit.CyanWater.BottomMiddle" },
                { '2', "BrickPit.CyanWater.OutTopLeft" },
                { '3', "BrickPit.CyanWater.MiddleRight" },
                { '4', "BrickPit.CyanWater.MiddleLeft" },
                { '5', "BrickPit.CyanWater.OutBottomRight" },
                { '6', "BrickPit.CyanWater.TopMiddle" },
                { '7', "BrickPit.CyanWater.OutBottomLeft" },
                { '{', "BrickPit.CyanWater.TopLeft" },
                { '}', "BrickPit.CyanWater.TopRight" },
                { '[', "BrickPit.CyanWater.BottomLeft" },
                { ']', "BrickPit.CyanWater.BottomRight" },
                { 'g', "BrickFloor.Gray.TopLeft" },
                { 'h', "BrickFloor.Gray.TopMiddle" },
                { 'i', "BrickFloor.Gray.TopRight" },
                { 'j', "BrickFloor.Gray.MiddleLeft" },
                { 'k', "BrickFloor.Gray.MiddleRight" },
                { 'l', "BrickFloor.Gray.BottomLeft" },
                { 'm', "BrickFloor.Gray.BottomMiddle" },
                { 'n', "BrickFloor.Gray.BottomRight" },
                { '.', "BrickFloor.Gray.Middle" },
                { '#', "BrickFloor.Gray.Single" },
                { 'a', "BrickWall.LiteBlue.TopLeft" },
                { 'b', "BrickWall.LiteBlue.Horizontal" },
                { 'c', "BrickWall.LiteBlue.TopRight" },
                { 'd', "BrickWall.LiteBlue.Vertical" },
                { 'e', "BrickWall.LiteBlue.BottomLeft" },
                { 'f', "BrickWall.LiteBlue.BottomRight" },
                { 'v', "BrickWall.LiteBlue.BottomCap" },
            };
            var map = new string[] {
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~011111111111112~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3ghhhhhhhhhhhi4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3j.abbbbbc...k4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3j.dghhhid...k4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3j.dj...kd...k4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3j.dlmmmnd...k4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3j.eb#bbbf...k4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3j...........k4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~3lmmmmmmmmmmmn4~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~566666666666667~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~~~~~~~~~~~~~~~~~~~01111111111111111111111112~~~~",
                "~~~~~~~~~~~~~~~~~~~~3ghhhhhhhhhhhhhhhhhhhhhhi4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j......................k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j.............ebbbc....k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j..{66666}........d....k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j..4~~~~~3..abbbbbf....k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j..4~~~~~3..d..........k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j..4~~~~~3..d.abbbbbbc.k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j..4~~~~~3..d.v......d.k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j..[11111]..d........d.k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3j...........ebbbbbbbbf.k4~~~~",
                "~~~~~~~~~~~~~~~~~~~~3lmmmmmmmmmmmmmmmmmmmmmmn4~~~~",
                "~~~~~~~~~~~~~~~~~~~~56666666666666666666666667~~~~",
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                };
            Rectangle dest = tileRect;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            string row;
            string tile;
            for (var y = 0; y < 30; y++)
            {
                row = map[y];
                dest.X = 0;
                for (var x = 0; x < 50; x++)
                {
                    tile = key[row[x]];
                    mapSprites.Draw(spriteBatch, tile, dest);
                    dest.X += 16;
                }
                dest.Y += 16;
            }
            spriteBatch.End();
        }
    }
}
