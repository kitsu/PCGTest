using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Simulation.Map.Generators;
using PCGTest.Utilities.Geometry;
using SharpNoise.Modules;

namespace PCGTest.Simulation.Map
{
    public class Chunk
    {
        public const int ChunkSize = 32;
        public Vector Index;
        ICellGenerator _generator;
        Cell[,] Cells;
        // Reactive streams
        private readonly Subject<KeyValuePair<Vector, Cell>> _cellUpdate;
        public IObservable<KeyValuePair<Vector, Cell>> WhenCellUpdates;

        public Chunk(Vector index, ICellGenerator generator)
        {
            // Setup reactive streams
            _cellUpdate = new Subject<KeyValuePair<Vector, Cell>>();
            WhenCellUpdates = _cellUpdate.AsObservable();
            // Initialize
            Index = index;
            _generator = generator;
            Cells = new Cell[ChunkSize, ChunkSize];
        }

        public void Initialize()
        {
            Task.Run(() => GenerateCells());
        }

        void GenerateCells()
        {
            Vector coord;
            foreach (var pair in _generator.GetArea(Rect))
            {
                coord = LocalCoord(pair.Key);
                Cells[coord.X, coord.Y] = pair.Value;
                _cellUpdate.OnNext(pair);
            }
        }

        public IDictionary<int, string> UsedMaterials => _generator.UsedMaterials;

        public Cell this[Vector index] => Cells[index.X, index.Y];

        public Rect Rect
        {
            get
            {
                var corner = Index * ChunkSize;
                return new Rect(corner.X, corner.Y, ChunkSize, ChunkSize);
            }
        }

        /// <summary>
        /// Fire update event on unchanged cell.
        /// </summary>
        /// <param name="area"></param>
        public void TouchArea(Rect area)
        {
            var overlap = Rect.Intersection(area);
            Cell cell;
            foreach (var coord in overlap.Coordinates())
            {
                cell = this[LocalCoord(coord)];
                _cellUpdate.OnNext(new KeyValuePair<Vector, Cell>(coord, cell));
            }
        }

        public void Step()
        {
        }

        public Vector GlobalCoord(int x, int y) => GlobalCoord(new Vector(x, y));
        public Vector GlobalCoord(Vector loc) => (Index * ChunkSize) + loc;

        public static Vector LocalCoord(int x, int y) => LocalCoord(new Vector(x, y));
        public static Vector LocalCoord(Vector coord) => (coord % ChunkSize).Abs();

        public static Vector Coord2Chunk(int x, int y) => Coord2Chunk(new Vector(x, y));
        public static Vector Coord2Chunk(Vector coord) {
            var sign = new Vector(
                coord.X < 0 ? -1 : 0,
                coord.Y < 0 ? -1 : 0
                );
            return sign + (coord / ChunkSize);
        }
        public static Vector Chunk2Coord(int x, int y) => Coord2Chunk(new Vector(x, y));
        public static Vector Chunk2Coord(Vector coord) => coord * ChunkSize;
    }
}
