using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Utilities.Geometry;
using SharpNoise.Modules;

namespace PCGTest.Simulation.Map
{
    public class Chunk
    {
        public const int ChunkSize = 32;
        public Vector Index;
        Random _rand;
        Module _noise;
        Cell[,] Cells;
        // Reactive streams
        private readonly Subject<KeyValuePair<Vector, Cell>> _cellUpdate;
        public IObservable<KeyValuePair<Vector, Cell>> WhenCellUpdates;

        public Chunk(Vector index, Random rand, Module noise)
        {
            // Setup reactive streams
            _cellUpdate = new Subject<KeyValuePair<Vector, Cell>>();
            WhenCellUpdates = _cellUpdate.AsObservable();
            // Initialize
            Index = index;
            _rand = rand;
            _noise = noise;
            Cells = new Cell[ChunkSize, ChunkSize];
        }

        public void Initialize()
        {
            GenerateCells();
        }

        void GenerateCells()
        {
            double value;
            Vector coord;
            Cell cell;
            string floor, fill;
            foreach (var global in Rect.Coordinates())
            {
                coord = LocalCoord(global);
                value = _noise.GetValue(global.X, global.Y, 0);
                floor = (value <= 0.1) ? "Water" : "Brick";
                fill = (0.5 <= value && value <= 0.6) ? "Wall" : "";
                cell = new Cell(floor, fill);
                Cells[coord.X, coord.Y] = cell;
                _cellUpdate.OnNext(new KeyValuePair<Vector, Cell>(global, cell));
            }
        }

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
            var overlap = this.Rect.Intersection(area);
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
        public static Vector LocalCoord(Vector coord) => coord % ChunkSize;

        public static Vector Coord2Chunk(int x, int y) => Coord2Chunk(new Vector(x, y));
        public static Vector Coord2Chunk(Vector coord) => coord / ChunkSize;
    }
}
