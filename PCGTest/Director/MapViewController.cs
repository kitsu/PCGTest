using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Director
{

    /* Control map viewport location in simulation world, the loaded simulation
     * chunks, provide sprite collections for a given map view tile coordinate. */
    public class MapViewController: IDisposable
    {
        // Pairs of subjects and public observables
        private readonly Subject<KeyValuePair<int, string>> _addTileKey;
        public IObservable<KeyValuePair<int, string>> WhenAddTileKey;
        private readonly Subject<int> _subTileKey;
        public IObservable<int> WhenSubTileKey;
        // Other members
        GameManager _parent;
        List<Viewport> Viewports;

        public MapViewController(GameManager parent)
        {
            _parent = parent;
            _addTileKey = new Subject<KeyValuePair<int, string>>();
            WhenAddTileKey = _addTileKey.AsObservable();
            _subTileKey = new Subject<int>();
            WhenSubTileKey = _subTileKey.AsObservable();
            Viewports = new List<Viewport>();
        }


        public Viewport AddViewport(Rect rect)
        {
            var view = new Viewport(rect);
            Viewports.Add(view);
            return view;
        }

        public void Initialize()
        {
            var keys = new Dictionary<int, string>() {
                { 0, "SolidBlack" },
                { 1, "Pit.Brick.CyanWater" },
                { 2, "Floor.Brick.Gray" },
                { 3, "Wall.Brick.LiteBlue" },
            };
            foreach (var key in keys)
            {
                _addTileKey.OnNext(key);
            }
            //OnUpdateTileKeys(keys);
            // Create fake data and feed it to the viewports
            int[,] map;
            foreach (var vp in Viewports)
            {
                int tile;
                var bounds = vp.Bounds;
                var qheight = bounds.Height / 4;
                var qwidth = bounds.Width / 4;
                map = new int[bounds.Width,bounds.Height];
                // Set inner half of map to brick
                for (var y = 0; y < bounds.Height; y++)
                {
                    for (var x = 0; x < bounds.Width; x++)
                    {
                        if (x >= qwidth && x <= 3 * qwidth &&
                            y >= qheight && y <= 3 * qheight)
                        {
                            // Island in middle
                            if ((x == qwidth + 3 || x == (3 * qwidth) - 3) &&
                                (y >= qheight + 3 && y <= (3 * qheight) - 3))
                            {
                                // Wall offset 3 from shore
                                tile = 3;
                            } else if ((x >= qwidth + 3 && x <= (3 * qwidth) - 3) &&
                                (y == qheight + 3 || y == (3 * qheight) - 3))
                            {
                                // Wall offset 3 from shore
                                tile = 3;
                            } else
                            {
                                tile = 2;
                            }
                        } else
                        {
                            tile = 1;
                        }
                            map[x, y] = tile;
                    }
                }
                vp.Initialize(map);
            }
        }

        public void Dispose()
        {
            _addTileKey.OnCompleted();
            _subTileKey.OnCompleted();
            foreach (var vp in Viewports)
            {
                vp.Dispose();
            }
        }
    }

    public class Viewport: IDisposable
    {
        public Rect Bounds;
        private readonly Subject<int[,]> _map;
        public IObservable<int[,]> WhenMap;

        public Viewport(Rect rect)
        {
            Bounds = rect;
            _map = new Subject<int[,]>();
            WhenMap = _map.AsObservable();
        }

        public void Initialize(int[,] map)
        {
            _map.OnNext(map);
        }

        public void Dispose()
        {
            _map.OnCompleted();
        }
    }
}
