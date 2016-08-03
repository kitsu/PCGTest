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
    class TitleView: BaseView, ITitleView
    {
        SpriteMap mapSprites;
        TileProvider _tiles;
        Rectangle tileRect;
        int[,] _tileMap;
        // Observables
        private readonly Subject<string> _selection;
        public IObservable<string> WhenSelected => _selection.AsObservable();

        public TitleView(GraphicsDevice screen, int width, int height):
            base(screen, width, height)
        {
            _selection = new Subject<string>();
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
            _tiles = new TileProvider();
            // Create map as string array with prefab "title island"
            var island = new string[] {
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~....................................~~",
                @"~~....................................~~",
                @"~~.....#####.###.#####.#.....#####....~~",
                @"~~.......#....#....#...#.....#........~~",
                @"~~.......#....#....#...#.....###......~~",
                @"~~.......#....#....#...#.....#........~~",
                @"~~.......#...###...#...#####.#####....~~",
                @"~~....................................~~",
                @"~~..################################..~~",
                @"~~....................................~~",
                @"~~..################################..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#..~~",
                @"~~..################################..~~",
                @"~~....................................~~",
                @"~~....................................~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
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
            // Convert string map to int array
            var tileMap = new int[Width, Height];
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    tileMap[x, y] = map[y][x];
                }
            }
            _tiles.AddTileType(new KeyValuePair<int, string>('~', "Pit.FreshWater.Brick"));
            _tiles.AddTileType(new KeyValuePair<int, string>('.', "Floor.Brick.Gray"));
            _tiles.AddTileType(new KeyValuePair<int, string>('#', "Wall.Brick.LiteBlue"));
            _tileMap = _tiles.ResolveMap(tileMap);
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
