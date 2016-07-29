using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Simulation;
using PCGTest.Simulation.Map;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Director
{

    /* Control map viewport location in simulation world, the loaded simulation
     * chunks, provide sprite collections for a given map view tile coordinate. */
    public class MapViewController: IDisposable
    {
        // Observables
        public IObservable<KeyValuePair<int, string>> WhenAddTileKey;
        public IObservable<int> WhenSubTileKey;
        // Other members
        GameManager _parent;
        List<Viewport> Viewports;

        public MapViewController(GameManager parent)
        {
            _parent = parent;
            WhenAddTileKey = parent.SimMan.WhenMaterialAdd;
            WhenSubTileKey = parent.SimMan.WhenMaterialRemove;
            Viewports = new List<Viewport>();

            // Register for sim map notifications
            parent.SimMan.WhenCellUpdates.Subscribe(CellUpdate);
        }


        public Viewport AddViewport(Rect rect)
        {
            var view = new Viewport(_parent.SimMan, rect);
            Viewports.Add(view);
            return view;
        }

        public void CellUpdate(KeyValuePair<Vector, Cell> pair)
        {
            //pass
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
            foreach (var vp in Viewports)
            {
                vp.Dispose();
            }
        }
    }

    public class Viewport: IDisposable
    {
        public Rect Bounds;
        SimulationManager _sim;
        // Streams
        private readonly Subject<int[,]> _mapChange;
        public IObservable<int[,]> WhenMapChanges;
        // State
        int[,] _tiles;
        Dictionary<int, string> _tileKeys;

        public Viewport(SimulationManager sim, Rect rect)
        {
            Bounds = rect;
            _sim = sim;
            _tiles = new int[rect.Width, rect.Height];
            _tileKeys = new Dictionary<int, string>();
            // Setup streams
            _mapChange = new Subject<int[,]>();
            WhenMapChanges = _mapChange.AsObservable();
            // Register for chunk events
            sim.WhenCellUpdates.Subscribe(CellUpdated);
        }

        public void Initialize()
        {
            // Trigger chunk load/touch
            _sim.LoadArea(Bounds);
        }

        public void CellUpdated(KeyValuePair<Vector, Cell> cell)
        {
            if (Bounds.Contains(cell.Key))
            {
                // Ensure tile key is registered
                // Notify tile update
                var coord = cell.Key - Bounds.TopLeft;
                //FIXME: Convert from cell data to tile key
                int tile;
                if (cell.Value.Fill == 0)
                {
                    tile = cell.Value.Floor;
                } else
                {
                    tile = cell.Value.Fill;
                }
                if (_tiles[coord.X, coord.Y] != tile)
                {
                    _tiles[coord.X, coord.Y] = tile;
                    _mapChange.OnNext(_tiles);
                }
            }
        }

        public void Dispose()
        {
            _mapChange.OnCompleted();
        }
    }
}
