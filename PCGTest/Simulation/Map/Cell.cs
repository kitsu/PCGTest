using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Simulation.Map
{
    public class Cell
    {
        public int Floor;
        public int Fill;
        public int Occupant;
        public List<string> Contents;

        public Cell(int floor, int fill)
        {
            Floor = floor;
            Fill = fill;
        }
    }
}
