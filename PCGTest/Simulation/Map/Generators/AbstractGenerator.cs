using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Simulation.Map.Generators
{
    abstract class AbstractGenerator : ICellGenerator
    {
        protected ChunkSource _source;

        abstract public IDictionary<int, string> UsedMaterials { get; }

        public AbstractGenerator(ChunkSource source)
        {
            _source = source;
        }

        public virtual Cell GetCell(Vector coord)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<Vector, Cell>> GetArea(Rect area)
        {
            foreach (var coord in area.Coordinates())
            {
                yield return new KeyValuePair<Vector, Cell>(coord, GetCell(coord));
            }
        }
    }
}
