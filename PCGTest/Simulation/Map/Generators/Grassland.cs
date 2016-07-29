using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Simulation.Map.Generators
{
    class Grassland: AbstractGenerator
    {
        Dictionary<int, string> _mats;

        public Grassland(ChunkSource source): base(source)
        {
        }

        override public IDictionary<int, string> UsedMaterials
        {
            get
            {
                if (_mats == null)
                {
                    _mats = new Dictionary<int, string>
                    {
                        {0, "Void" }
                    };
                    // This array may be replaced by a probabilities field
                    // if I can figure out a sane way to store probabilities
                    var ids = new int[]
                        {
                        Materials.GetPit("FreshWater.Rock"),
                        Materials.GetId("Beach"),
                        Materials.GetFloor("Dirt"),
                        Materials.GetFloor("Dirt.Grass"),
                    };
                    foreach (var id in ids)
                    {
                        _mats[id] = Materials.GetName(id);
                    };
                }
                return _mats;
            }
        }

        public override Cell GetCell(Vector coord)
        {
            var local = Chunk.LocalCoord(coord);
            var value = _source.SampleCell(coord);
            var floor = PickFloor(value);
            //var fill = (0.5 <= value && value <= 0.6) ? "wall" : "";
            return new Cell(floor, 0);
        }

        int PickFloor(double value)
        {
            // Given a number [-1.0 1.0] map ranges to floor material
            if (value < -.8)
                return Materials.GetPit("FreshWater.Rock");
            if (value < -.4)
                return Materials.GetId("Beach");
            if (value < -.25)
                return Materials.GetFloor("Dirt");
            if (value >= -.25)
                return Materials.GetFloor("Dirt.Grass");
            return 0;
        }
    }
}
