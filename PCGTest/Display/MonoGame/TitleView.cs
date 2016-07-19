using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace PCGTest.Display.MonoGame
{
    class TitleView: BaseView
    {
        SpriteMap mapSprites;
        Rectangle tileRect;
        Dictionary<char, string> _tileKeys;
        string[] _tileMap;
        private readonly Subject<string> _selection;
        public IObservable<string> WhenSelected;

        public TitleView(GraphicsDevice screen, int width, int height):
            base(screen, width, height)
        {
            _selection = new Subject<string>();
            WhenSelected = _selection.AsObservable();
        }

        public override void LoadContent(object contentSource)
        {
            var assets = (AssetProvider)contentSource;
            mapSprites = assets.GetMapSprites();
            Size = mapSprites.Size;
            // Convert width/height from pixels to tiles
            Width /= Size;
            Height /= Size;
            tileRect = new Rectangle(0, 0, Size, Size);
            var island = new string[] {
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~[------------------------------------]~",
                @"~/1==================================2\~",
                @"~/(..................................)\~",
                @"~/(....L#T#R.LTR.L#T#R.^.....l###R...)\~",
                @"~/(......*....*....*...*.....*.......)\~",
                @"~/(......*....*....*...*.....E##.....)\~",
                @"~/(......*....*....*...*.....*.......)\~",
                @"~/(......V...LtR...V...L###R.L###R...)\~",
                @"~/(..................................)\~",
                @"~/(.<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>.)\~",
                @"~/(..................................)\~",
                @"~/(.l##############################r.)\~",
                @"~/(.*5____________________________6*.)\~",
                @"~/(.*\~~~~~~~~~~~~~~~~~~~~~~~~~~~~/*.)\~",
                @"~/(.*\~~~~~~~~~~~~~~~~~~~~~~~~~~~~/*.)\~",
                @"~/(.*\~~~~~~~~~~~~~~~~~~~~~~~~~~~~/*.)\~",
                @"~/(.*\~~~~~~~~~~~~~~~~~~~~~~~~~~~~/*.)\~",
                @"~/(.*\~~~~~~~~~~~~~~~~~~~~~~~~~~~~/*.)\~",
                @"~/(.*\~~~~~~~~~~~~~~~~~~~~~~~~~~~~/*.)\~",
                @"~/(.*7----------------------------8*.)\~",
                @"~/(.L##############################R.)\~",
                @"~/(..................................)\~",
                @"~/3++++++++++++++++++++++++++++++++++4\~",
                @"~{____________________________________}~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            };
            var row = new string('~', Width);
            var map = new List<string>();
            map.Add(row);
            var pad = Width - island[0].Count();
            var lpad = new string('~', pad / 2);
            var rpad = new string('~', pad - (pad / 2));
            foreach (var line in island)
            {
                map.Add($"{lpad}{line}{rpad}");
            }
            for (var i = map.Count(); i < Height; i++)
            {
                map.Add(row);
            }
            _tileMap = map.ToArray();
            _tileKeys = new Dictionary<char, string>()
            {
                {'~', "Pit.Brick.CyanWater.Middle" },
                {'[', "Pit.Brick.CyanWater.OutTopRight" },
                {'-', "Pit.Brick.CyanWater.BottomMiddle" },
                {']', "Pit.Brick.CyanWater.OutTopLeft" },
                {'/', "Pit.Brick.CyanWater.MiddleRight" },
                {'\\', "Pit.Brick.CyanWater.MiddleLeft" },
                {'{', "Pit.Brick.CyanWater.OutBottomRight" },
                {'_', "Pit.Brick.CyanWater.TopMiddle" },
                {'}', "Pit.Brick.CyanWater.OutBottomLeft" },
                {'5', "Pit.Brick.CyanWater.TopLeft" },
                {'6', "Pit.Brick.CyanWater.TopRight" },
                {'7', "Pit.Brick.CyanWater.BottomLeft" },
                {'8', "Pit.Brick.CyanWater.BottomRight" },
                {'.', "Floor.Brick.Gray.Middle" },
                {'1', "Floor.Brick.Gray.TopLeft" },
                {'=', "Floor.Brick.Gray.TopMiddle" },
                {'2', "Floor.Brick.Gray.TopRight" },
                {'(', "Floor.Brick.Gray.MiddleLeft" },
                {')', "Floor.Brick.Gray.MiddleRight" },
                {'3', "Floor.Brick.Gray.BottomLeft" },
                {'+', "Floor.Brick.Gray.BottomMiddle" },
                {'4', "Floor.Brick.Gray.BottomRight" },
                {'<', "Floor.Brick.Gray.SingleLeft" },
                {',', "Floor.Brick.Gray.SingleHorizontal" },
                {'>', "Floor.Brick.Gray.SingleRight" },
                {'#', "Wall.Brick.LiteBlue.Horizontal" },
                {'*', "Wall.Brick.LiteBlue.Vertical" },
                {'T', "Wall.Brick.LiteBlue.TeeTop" },
                {'t', "Wall.Brick.LiteBlue.TeeBottom" },
                {'E', "Wall.Brick.LiteBlue.TeeLeft" },
                {'L', "Wall.Brick.LiteBlue.BottomLeft" },
                {'l', "Wall.Brick.LiteBlue.TopLeft" },
                {'R', "Wall.Brick.LiteBlue.BottomRight" },
                {'r', "Wall.Brick.LiteBlue.TopRight" },
                {'^', "Wall.Brick.LiteBlue.TopCap" },
                {'V', "Wall.Brick.LiteBlue.BottomCap" },
            };
        }

        public override void Update(int elapsed)
        {
            mapSprites.Update(elapsed);
        }

        public override void Draw(int elapsed)
        {
            DrawBackground();
            DrawMenu();
        }

        void DrawBackground()
        {
            Rectangle dest = tileRect;
            string tile;
            int x = 0;
            int y = 0;
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            foreach (string row in _tileMap)
            {
                foreach (char id in row)
                {
                    tile = _tileKeys[id];
                    dest.X = Size * x;
                    dest.Y = Size * y;
                    mapSprites.Draw(spriteBatch, tile, dest);
                    x += 1;
                }
                x = 0;
                y += 1;
            }
            spriteBatch.End();
        }
        private void DrawMenu()
        {
        }

        /// <summary>
        /// Move highlight to next menu item, loop at bottom
        /// </summary>
        public void NextMenuItem()
        {
        }

        /// <summary>
        /// Move highlight to previous menu item, loop at top
        /// </summary>
        public void PrevMenuItem()
        {
        }

        /// <summary>
        /// Send menu item selected message indicating selected item
        /// </summary>
        public void MenuSelect()
        {
            _selection.OnNext("Item");
        }
    }
}
