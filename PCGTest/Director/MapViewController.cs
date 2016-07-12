using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Director
{
    public delegate void UpdateTileKeysHandler(Dictionary<int, string> keys);
    public delegate void AddTileKeyHandler(int key, string value);
    public delegate void RemoveTileKeyHandler(int key);
    public delegate void UpdateMapHandler(IEnumerable<int> keys);

    /* Control map viewport location in simulation world, the loaded simulation
     * chunks, provide sprite collections for a given map view tile coordinate. */
    class MapViewController
    {
        public event UpdateTileKeysHandler UpdateTileKeys;
        public event AddTileKeyHandler AddTileKey;
        public event RemoveTileKeyHandler RemoveTileKey;
        List<Viewport> Viewports;

        public MapViewController()
        {
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
                { 1, "BrickPit.CyanWater" },
                { 2, "BrickFloor.Gray" },
                { 3, "BrickWall.LiteBlue" },
            };
            OnUpdateTileKeys(keys);
            // Create fake data and feed it to the viewports
            int[] map;
            foreach (var vp in Viewports)
            {
                int i;
                var bounds = vp.Bounds;
                var qheight = bounds.Height / 4;
                var qwidth = bounds.Width / 4;
                map = Enumerable.Repeat(1, bounds.Width * bounds.Height).ToArray();
                // Set inner half of map to brick
                for (var y = qheight; y < 3 * qheight; y++)
                {
                    for (var x = qwidth; x < 3 * qwidth; x++)
                    {
                        i = (y * bounds.Width) + x;
                        map[i] = 2;
                    }
                }
                vp.Initialize(map);
            }
        }

        public void OnUpdateTileKeys(Dictionary<int, string> keys)
        {
            UpdateTileKeys(keys);
        }

        public void OnAddTileKey(int key, string value)
        {
            AddTileKey(key, value);
        }

        public void OnRemoveTileKey(int key)
        {
            RemoveTileKey(key);
        }
    }

    public class Viewport
    {
        public event UpdateMapHandler UpdateMap;
        public Rect Bounds;

        public Viewport(Rect rect)
        {
            Bounds = rect;
        }

        public void Initialize(IEnumerable<int> mapData)
        {
            OnUpdateMap(mapData);
        }

        public void OnUpdateMap(IEnumerable<int> keys)
        {
            UpdateMap(keys);
        }
    }
}
