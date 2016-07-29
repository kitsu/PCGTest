using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Simulation.Map.Generators
{
    public interface ICellGenerator
    {
        Cell GetCell(Vector coord);
        IEnumerable<KeyValuePair<Vector, Cell>> GetArea(Rect area);
        IDictionary<int, string> UsedMaterials { get; }
    }
}
