using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Simulation.Map;
using PCGTest.Utilities.Geometry;
using SharpNoise.Modules;

namespace PCGTest.Simulation
{
    public class SimulationManager
    {
        public Dictionary<Vector, Chunk> LoadedChunks;
        Random _rand;
        Module _noise;
        // Reactive streams
        private readonly Subject<KeyValuePair<Vector, Cell>> _cellUpdate;
        public IObservable<KeyValuePair<Vector, Cell>> WhenCellUpdates;

        public SimulationManager(int seed)
        {
            _rand = new Random(seed);
            var noise = new Perlin
            {
                Seed = seed,
                Frequency = 0.1
            };
            _noise = noise;
            LoadedChunks = new Dictionary<Vector, Chunk>();
            // Setup reactive streams
            //WhenCellUpdates = Observable.Never<KeyValuePair<Vector, Cell>>();
            _cellUpdate = new Subject<KeyValuePair<Vector, Cell>>();
            WhenCellUpdates = _cellUpdate.AsObservable();
        }

        public Chunk this[Vector index]
        {
            get
            {
                if (LoadedChunks.ContainsKey(index))
                {
                    return LoadedChunks[index];
                }
                return null;
            }
        }

        public void LoadArea(Rect area)
        {
            bool triggered;
            Vector index;
            for (var y = area.Y; y < area.Bottom; y += Chunk.ChunkSize)
            {
                for (var x = area.X; x < area.Right; x += Chunk.ChunkSize)
                {
                    index = Chunk.Coord2Chunk(x, y);
                    triggered = LoadChunk(index);
                    // Ensure update event for every chunk in area
                    if (!triggered)
                    {
                        this[index].TouchArea(area);
                    }
                }
            }
        }

        public bool LoadChunk(Vector index)
        {
            if (this[index] == null)
            {
                var chunk = new Chunk(index, _rand, _noise);
                chunk.WhenCellUpdates.Subscribe( p => _cellUpdate.OnNext(p));
                LoadedChunks[index] = chunk;
                chunk.Initialize();
                return true;
            }
            return false;
        }

        public void Step()
        {
            foreach (var chunk in LoadedChunks)
            {
                chunk.Value.Step();
            }
        }
    }
}
