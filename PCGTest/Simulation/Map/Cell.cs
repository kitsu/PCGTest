using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Simulation.Map
{
    public class Cell
    {
        public string Floor;
        public string Fill;
        public List<string> Contents;

        public Cell(string floor, string fill)
        {
            Floor = floor;
            Fill = fill;
        }
    }
}
