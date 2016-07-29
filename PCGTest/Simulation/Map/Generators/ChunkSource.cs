using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Utilities.Geometry;
using SharpNoise.Modules;
using Troschuetz.Random;

namespace PCGTest.Simulation.Map.Generators
{
    class ChunkSource
    {
        int _seed;
        TRandom _rand;
        Module _cellNoise;
        Module _biomeNoise;
        Module _structNoise;

        public ChunkSource(int seed)
        {
            _seed = seed;
            _rand = new TRandom(seed);
            // This would be a good place to load config and customize noise gen
            _cellNoise = new Perlin
            {
                Seed = _rand.Next(),
                Frequency = 0.05
            };
            _biomeNoise = new Perlin
            {
                Seed = _rand.Next(),
                Frequency = 0.001
            };
            _structNoise = new Perlin
            {
                Seed = _rand.Next(),
                Frequency = 0.0001
            };
        }

        public Chunk GetChunk(Vector index)
        {
            // Determine appropriate generation module, return result of its execution
            //var biome = _biomeNoise.GetValue(index.X, index.Y, index.Z);
            return new Chunk(index, new Grassland(this));
        }

        //FIXME: These methods should convert to correct coordinate systems
        public double SampleCell(Vector coord)
        {
            return _cellNoise.GetValue(coord.X, coord.Y, coord.Z);
        }
        public double SampleBiome(Vector coord)
        {
            return _biomeNoise.GetValue(coord.X, coord.Y, coord.Z);
        }
        public double SampleStruct(Vector coord)
        {
            return _structNoise.GetValue(coord.X, coord.Y, coord.Z);
        }
    }
}
