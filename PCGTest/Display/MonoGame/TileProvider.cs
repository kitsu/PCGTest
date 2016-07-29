using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Display.MonoGame
{
    enum TileType
    {
        Border,
        Single,
        SingleTop,
        SingleBottom,
        SingleLeft,
        SingleRight,
        TeeTop,
        TeeBottom,
        TeeLeft,
        TeeRight,
        Horizontal,
        Vertical,
        Cross,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        OutTopLeft,
        OutTopRight,
        OutBottomLeft,
        OutBottomRight,
    }

    // Data container class
    class TileExpansions
    {
        public static KeyValuePair<int, string>[] Wall = new []
        {
            new KeyValuePair<int, string>( 1, "TopLeft" ),
            new KeyValuePair<int, string>( 2, "Horizontal" ),
            new KeyValuePair<int, string>( 3, "TopRight" ),
            new KeyValuePair<int, string>( 4, "Vertical" ),
            new KeyValuePair<int, string>( 5, "TopCap" ),
            new KeyValuePair<int, string>( 6, "MiddleRight" ),
            new KeyValuePair<int, string>( 7, "BottomLeft" ),
            new KeyValuePair<int, string>( 8, "BottomCap" ),
            new KeyValuePair<int, string>( 9, "BottomRight" ),
            new KeyValuePair<int, string>( 10, "Field" ),
            new KeyValuePair<int, string>( 11, "TeeTop" ),
            new KeyValuePair<int, string>( 12, "TeeLeft" ),
            new KeyValuePair<int, string>( 13, "Cross" ),
            new KeyValuePair<int, string>( 14, "TeeRight" ),
            new KeyValuePair<int, string>( 15, "TeeBottom" ),
        };

        public static Dictionary<TileType, int> WallKinds = new Dictionary<TileType, int>()
        {
            { TileType.Single, 8 },
            { TileType.SingleTop, 5 },
            { TileType.SingleBottom, 8 },
            { TileType.SingleLeft, 7 },
            { TileType.SingleRight, 9 },
            { TileType.TeeTop, 11 },
            { TileType.TeeBottom, 15 },
            { TileType.TeeLeft, 12 },
            { TileType.TeeRight, 14 },
            { TileType.Horizontal, 2 },
            { TileType.Vertical, 4 },
            { TileType.Cross, 13 },
            { TileType.TopLeft, 1 },
            { TileType.TopRight, 3 },
            { TileType.BottomLeft, 7 },
            { TileType.BottomRight, 9 },
            { TileType.OutTopLeft, 1 },
            { TileType.OutTopRight, 3 },
            { TileType.OutBottomLeft, 7 },
            { TileType.OutBottomRight, 9 }
        };

        public static KeyValuePair<int, string>[] Floor = new []
        {
            new KeyValuePair<int, string>( 1, "TopLeft" ),
            new KeyValuePair<int, string>( 2, "TopMiddle" ),
            new KeyValuePair<int, string>( 3, "TopRight" ),
            new KeyValuePair<int, string>( 4, "MiddleLeft" ),
            new KeyValuePair<int, string>( 5, "Middle" ),
            new KeyValuePair<int, string>( 6, "MiddleRight" ),
            new KeyValuePair<int, string>( 7, "BottomLeft" ),
            new KeyValuePair<int, string>( 8, "BottomMiddle" ),
            new KeyValuePair<int, string>( 9, "BottomRight" ),
            new KeyValuePair<int, string>( 10, "SingleTop" ),
            new KeyValuePair<int, string>( 11, "SingleVertical" ),
            new KeyValuePair<int, string>( 12, "SingleBottom" ),
            new KeyValuePair<int, string>( 13, "SingleLeft" ),
            new KeyValuePair<int, string>( 14, "SingleHorizontal" ),
            new KeyValuePair<int, string>( 15, "SingleRight" ),
            new KeyValuePair<int, string>( 16, "Single" ),
        };

        public static Dictionary<TileType, int> FloorKinds = new Dictionary<TileType, int>()
        {
            { TileType.Single, 16 },
            { TileType.SingleTop, 10 },
            { TileType.SingleBottom, 12 },
            { TileType.SingleLeft, 13 },
            { TileType.SingleRight, 15 },
            { TileType.TeeTop, 2 },
            { TileType.TeeBottom, 8 },
            { TileType.TeeLeft, 4 },
            { TileType.TeeRight, 6 },
            { TileType.Horizontal, 14 },
            { TileType.Vertical, 11 },
            { TileType.Cross, 5 },
            { TileType.TopLeft, 1 },
            { TileType.TopRight, 3 },
            { TileType.BottomLeft, 7 },
            { TileType.BottomRight, 9 },
            { TileType.OutTopLeft, 5 },
            { TileType.OutTopRight, 5 },
            { TileType.OutBottomLeft, 5 },
            { TileType.OutBottomRight, 5 }
        };

        public static KeyValuePair<int, string>[] Pit = new []
        {
            new KeyValuePair<int, string>( 1, "TopLeft" ),
            new KeyValuePair<int, string>( 2, "TopMiddle" ),
            new KeyValuePair<int, string>( 3, "TopRight" ),
            new KeyValuePair<int, string>( 4, "MiddleLeft" ),
            new KeyValuePair<int, string>( 5, "Middle" ),
            new KeyValuePair<int, string>( 6, "MiddleRight" ),
            new KeyValuePair<int, string>( 7, "BottomLeft" ),
            new KeyValuePair<int, string>( 8, "BottomMiddle" ),
            new KeyValuePair<int, string>( 9, "BottomRight" ),
            new KeyValuePair<int, string>( 10, "SingleTop" ),
            new KeyValuePair<int, string>( 11, "SingleVertical" ),
            new KeyValuePair<int, string>( 12, "SingleBottom" ),
            new KeyValuePair<int, string>( 13, "OutBottomRight" ),
            new KeyValuePair<int, string>( 14, "OutBottomLeft" ),
            new KeyValuePair<int, string>( 15, "OutTopRight" ),
            new KeyValuePair<int, string>( 16, "OutTopLeft" ),
            new KeyValuePair<int, string>( 17, "TeeTop" ),
            new KeyValuePair<int, string>( 18, "TeeBottom" ),
        };

        public static Dictionary<TileType, int> PitKinds = new Dictionary<TileType, int>()
        {
            { TileType.Single, 5 },
            { TileType.SingleTop, 10 },
            { TileType.SingleBottom, 12 },
            { TileType.SingleLeft, 1 },
            { TileType.SingleRight, 3 },
            { TileType.TeeTop, 2 },
            { TileType.TeeBottom, 8 },
            { TileType.TeeLeft, 4 },
            { TileType.TeeRight, 6 },
            { TileType.Horizontal, 2 },
            { TileType.Vertical, 11 },
            { TileType.Cross, 5 },
            { TileType.TopLeft, 1 },
            { TileType.TopRight, 3 },
            { TileType.BottomLeft, 7 },
            { TileType.BottomRight, 9 },
            { TileType.OutTopLeft, 16 },
            { TileType.OutTopRight, 15 },
            { TileType.OutBottomLeft, 14 },
            { TileType.OutBottomRight, 13 }
        };

        public static KeyValuePair<int, string>[] UI = new []
        {
            new KeyValuePair<int, string>( 1, "TopLeft" ),
            new KeyValuePair<int, string>( 2, "TopMiddle" ),
            new KeyValuePair<int, string>( 3, "TopRight" ),
            new KeyValuePair<int, string>( 4, "MiddleLeft" ),
            new KeyValuePair<int, string>( 5, "Middle" ),
            new KeyValuePair<int, string>( 6, "MiddleRight" ),
            new KeyValuePair<int, string>( 7, "BottomLeft" ),
            new KeyValuePair<int, string>( 8, "BottomMiddle" ),
            new KeyValuePair<int, string>( 9, "BottomRight" ),
            new KeyValuePair<int, string>( 10, "Single" ),
        };

        public static Dictionary<TileType, int> UIKinds = new Dictionary<TileType, int>()
        {
            { TileType.Single, 10 },
            { TileType.SingleTop, 10 },
            { TileType.SingleBottom, 10 },
            { TileType.SingleLeft, 10 },
            { TileType.SingleRight, 10 },
            { TileType.TeeTop, 2 },
            { TileType.TeeBottom, 8 },
            { TileType.TeeLeft, 4 },
            { TileType.TeeRight, 6 },
            { TileType.Horizontal, 10 },
            { TileType.Vertical, 10 },
            { TileType.Cross, 5 },
            { TileType.TopLeft, 1 },
            { TileType.TopRight, 3 },
            { TileType.BottomLeft, 7 },
            { TileType.BottomRight, 9 },
            { TileType.OutTopLeft, 1 },
            { TileType.OutTopRight, 3 },
            { TileType.OutBottomLeft, 7 },
            { TileType.OutBottomRight, 9 }
        };
    }

    class MapSolver
    {
        static Tuple<int, int>[] offsets = new []
        {
            new Tuple<int, int>(-1, -1),
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(1, -1),
            new Tuple<int, int>(-1, 0),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(-1, 1),
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(1, 1),
        };

        // 256 possible tile patterns mapped to tile type
        // NOTE: with careful selection of bit order trie lookup could be an option
        static Dictionary<TileType, int[]> Patterns = new Dictionary<TileType, int[]>()
        {
            { TileType.Border,
                new[] { -1 } },
            {TileType.Single,
                    new[] { 0, 1, 4, 5, 32, 33, 36, 37, 128, 129, 132, 133, 160,
                            161, 164, 165 }},
            {TileType.SingleTop,
                    new[] { 2, 3, 6, 7, 34, 35, 38, 39, 130, 131, 134, 135, 162, 163,
                            166, 167 } },
            {TileType.SingleBottom,
                    new[] { 64, 65, 68, 69, 96, 97, 100, 101, 192, 193, 196, 197,
                            224, 225, 228, 229 } },
            {TileType.SingleLeft,
                    new[] { 8, 9, 12, 13, 40, 41, 44, 45, 136, 137, 140, 141, 168,
                            169, 172, 173 } },
            {TileType.SingleRight,
                    new[] { 16, 17, 20, 21, 48, 49, 52, 53, 144, 145, 148, 149, 176,
                            177, 180, 181 } },
            {TileType.TeeTop,
                    new[] { 26, 27, 30, 31, 58, 59, 62, 63, 154, 155, 158, 159, 186,
                            187, 190, 191 } },
            {TileType.TeeBottom,
                    new[] { 88, 89, 92, 93, 120, 121, 124, 125, 216, 217, 220, 221,
                            248, 249, 252, 253 } },
            {TileType.TeeLeft,
                    new[] { 74, 75, 78, 79, 106, 107, 110, 111, 202, 203, 206, 207,
                            234, 235, 238, 239, 242, 243 } },
            {TileType.TeeRight,
                    new[] { 82, 83, 86, 87, 114, 115, 118, 119, 210, 211, 214, 215,
                            246, 247 } },
            {TileType.Horizontal,
                    new[] { 24, 25, 28, 29, 56, 57, 60, 61, 152, 153, 156, 157,
                            184, 185, 188, 189, 230, 231 } },
            {TileType.Vertical,
                    new[] { 66, 67, 70, 71, 98, 99, 102, 103, 194, 195, 198, 199,
                            226, 227 } },
            {TileType.Cross,
                    new[] { 90, 91, 94, 95, 122, 123, 126, 218, 219, 222,
                            250, 255 } },
            {TileType.TopLeft,
                    new[] { 11, 15, 43, 47, 139, 143, 171, 175 }},
            {TileType.TopRight,
                    new[] { 22, 23, 24, 54, 55, 150, 151, 182, 183 }},
            {TileType.BottomLeft,
                    new[] { 104, 105, 108, 109, 232, 233, 236, 237 }},
            {TileType.BottomRight,
                    new[] { 208, 209, 212, 213, 240, 241, 244, 245 }},
            {TileType.OutTopLeft,
                    new[] { 10, 14, 42, 46, 138, 142, 170, 174, 251 }},
            {TileType.OutTopRight,
                    new[] { 18, 19, 50, 51, 146, 147, 178, 179, 254 }},
            {TileType.OutBottomLeft,
                    new[] { 72, 73, 76, 77, 200, 201, 204, 205, 127 }},
            {TileType.OutBottomRight,
                    new[] { 80, 81, 84, 85, 112, 113, 116, 117, 223 }},
        };

        public static bool IsBorder(int x, int y, int[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            return x == 0 || y == 0 || x == width - 1 || y == height - 1;
        }

        public static int CalculateNeighborhood(int x, int y, int[,] map)
        {
            var tile = map[x, y];
            int bits = 0;
            foreach (var pair in offsets)
            {
                bits <<= 1;
                if (map[x + pair.Item1, y + pair.Item2] == tile)
                    bits += 1;
            }
            return bits;
        }

        public static TileType SolveTile(int x, int y, int[,] map)
        {
            if (IsBorder(x, y, map))
            {
                return TileType.Border;
            }
            else
            {
                var hood = (CalculateNeighborhood(x, y, map));
                foreach (var pair in Patterns)
                {
                    if (pair.Value.Contains(hood))
                        return pair.Key;
                }
            }
            return TileType.Border;
        }
    }

    public class TileProvider
    {
        Dictionary<int, string> _tileKeys;
        public readonly int _shift;

        public TileProvider(int shift = 8)
        {
            _tileKeys = new Dictionary<int, string>
            {
                // Ensure void always maps to Solid Black
                {0, "SolidBlack" }
            };
            _shift = shift;
        }

        public string this[int key] => _tileKeys[key];

        /// <summary>
        /// Given a base tile key expand it to all
        /// </summary>
        /// <param name="tileKey">The key to be expanded.</param>
        /// <param name="tileKeys">The dictionary to be populated.</param>
        public void AddTileType(KeyValuePair<int, string> tileKey)
        {
            var shifted = tileKey.Key << _shift;
            if (_tileKeys.ContainsKey(shifted))
                return;
            // Always add tileKey
            _tileKeys[shifted] = tileKey.Value;
            // Look up key in map of keys to <offset, suffix> pairs
            var expansion = GetExpansionData(tileKey.Value);
            foreach (var pair in expansion)
            {
                // Add each (key << 8) + offset = value + suffix to tileKeys
                _tileKeys[(shifted) + pair.Key] =
                    $"{tileKey.Value}.{pair.Value}";
            }
        }

        public static KeyValuePair<int, string>[] GetExpansionData(string key)
        {
            var kind = key.Split('.').First().ToLower();
            switch (kind)
            {
                case "wall":
                    return TileExpansions.Wall;
                case "floor":
                    return TileExpansions.Floor;
                case "pit":
                    return TileExpansions.Pit;
                case "ui":
                    return TileExpansions.UI;
            }
            return new KeyValuePair<int, string>[0];
        }

        int PickTile(int key, TileType variation)
        {
            var kind = _tileKeys[key].Split('.').First().ToLower();
            int offset = 0;
            switch (kind)
            {
                case "wall":
                    offset = TileExpansions.WallKinds[variation];
                    break;
                case "floor":
                    offset = TileExpansions.FloorKinds[variation];
                    break;
                case "pit":
                    offset = TileExpansions.PitKinds[variation];
                    break;
                case "ui":
                    offset = TileExpansions.UIKinds[variation];
                    break;
            }
            return key + offset;
        }

        /// <summary>
        /// Convert an array of tile type keys into an array of expanded tile keys.
        /// </summary>
        /// <param name="map">2D Int array of map tile type keys.</param>
        /// <returns></returns>
        public int[,] ResolveMap(int[,] map)
        {
            TileType variation;
            int id, shifted;
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            var result = new int[width, height];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    id = map[x, y];
                    shifted = id << _shift;
                    if (!_tileKeys.ContainsKey(shifted))
                    {
                        // Substitute solid black for missing tiles
                        result[x, y] = 0;
                    }
                    else if (!_tileKeys.ContainsKey(shifted + 1))
                    {
                        // If the tile doesn't have alternates just use shifted value
                        if (MapSolver.IsBorder(x, y, map))
                        {
                            result[x, y] = 0;
                        } else
                        {
                            result[x, y] = shifted;
                        }
                    }
                    else
                    {
                        // Figure out which alternate to use by matching neighbor array
                        variation = MapSolver.SolveTile(x, y, map);
                        if (variation == TileType.Border)
                        {
                            result[x, y] = 0;
                        } else
                        {
                            result[x, y] = PickTile(shifted, variation);
                        }
                    }
                }
            }
            return result;
        }
    }
}
