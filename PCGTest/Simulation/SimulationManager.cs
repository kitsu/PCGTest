using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Simulation.Map;
using PCGTest.Simulation.Map.Generators;
using PCGTest.Utilities.Geometry;
using SharpNoise.Modules;

namespace PCGTest.Simulation
{
    public class SimulationManager
    {
        ChunkSource _generator;
        public Dictionary<Vector, Chunk> LoadedChunks;
        Dictionary<int, string> _mats;
        // Reactive streams
        private readonly Subject<KeyValuePair<Vector, Cell>> _cellUpdate;
        public IObservable<KeyValuePair<Vector, Cell>> WhenCellUpdates;
        private readonly Subject<KeyValuePair<int, string>> _matAdd;
        public IObservable<KeyValuePair<int, string>> WhenMaterialAdd;
        private readonly Subject<int> _matRemove;
        public IObservable<int> WhenMaterialRemove;

        public SimulationManager(int seed)
        {
            LoadedChunks = new Dictionary<Vector, Chunk>();
            _mats = new Dictionary<int, string>();
            _generator = new ChunkSource(seed);
            // Setup reactive streams
            _cellUpdate = new Subject<KeyValuePair<Vector, Cell>>();
            WhenCellUpdates = _cellUpdate.AsObservable();
            _matAdd = new Subject<KeyValuePair<int, string>>();
            WhenMaterialAdd = _matAdd.AsObservable();
            _matRemove = new Subject<int>();
            WhenMaterialRemove = _matRemove.AsObservable();
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
            var unit = new Vector(1, 1);
            // Quantize area TL to chunk coord, then move up & left by one chunk
            var start = Chunk.Coord2Chunk(area.TopLeft) - unit;
            // Quantize area end coord to chunk, move down & right by one chunk
            var end = Chunk.Coord2Chunk(area.BottomRight) + unit;
            // Step by chunk
            for (var y = start.Y; y <= end.Y; y++)
            {
                for (var x = start.X; x <= end.X; x++)
                {
                    index = new Vector(x, y);
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
                var chunk = _generator.GetChunk(index);
                chunk.WhenCellUpdates.Subscribe( p => _cellUpdate.OnNext(p));
                LoadedChunks[index] = chunk;
                chunk.Initialize();
                UpdateMaterials(chunk);
                return true;
            }
            return false;
        }

        void UpdateMaterials(Chunk chunk)
        {
            foreach (var mat in chunk.UsedMaterials)
            {
                if (!_mats.ContainsKey(mat.Key))
                {
                    _mats[mat.Key] = mat.Value;
                    _matAdd.OnNext(mat);
                }
            }
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
