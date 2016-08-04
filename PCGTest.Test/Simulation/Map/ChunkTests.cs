using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using PCGTest.Utilities.Geometry;
using PCGTest.Simulation.Map;
using PCGTest.Simulation.Map.Generators;
using Moq;

namespace PCGTest.Test.Simulation.Map
{
    [TestFixture]
    class ChunkTests
    {
        [Test]
        public void Global_Coord_Conversion_Correct_At_Origin_Chunk()
        {
            var mock = new Mock<ICellGenerator>();
            var chunk = new Chunk(Vector.Zero, mock.Object);
            chunk.GlobalCoord(0, 0).Should().Be(Vector.Zero,
                "because the origin of the origin chunk is the origin");
            chunk.GlobalCoord(5, 9).Should().Be(new Vector(5, 9),
                "because global matches local in the origin chunk");
            var vec = new Vector(Chunk.ChunkSize, Chunk.ChunkSize);
            chunk.GlobalCoord(vec).Should().Be(vec,
                "because global matches local in the origin chunk");
        }

        [Test]
        public void Global_Coord_Conversion_Correct_In_Negative_Chunk()
        {
            var mock = new Mock<ICellGenerator>();
            var vec = new Vector(Chunk.ChunkSize, Chunk.ChunkSize);
            var chunk = new Chunk(new Vector(-10, -10), mock.Object);
            chunk.GlobalCoord(0, 0).Should().Be(-10 * vec,
                "because the origin of the negative chunk is the ChunkSize * index");
            chunk.GlobalCoord(5, 9).Should().Be((-10 * vec) + new Vector(5, 9),
                "because global coord is ChunkSize * index plus local offset");
        }

        [Test]
        public void Local_Coord_Conversion_Correct()
        {
            Chunk.LocalCoord(0, 0).Should().Be(Vector.Zero,
                "because the origin of the origin chunk is the origin");
            Chunk.LocalCoord(5, 9).Should().Be(new Vector(5, 9),
                "because global matches local in the origin chunk");
            var size = new Vector(Chunk.ChunkSize, Chunk.ChunkSize);
            Chunk.LocalCoord(size).Should().Be(Vector.Zero,
                "because local coords wrap at chunk boundaries");
            var vec = new Vector(5, 9);
            Chunk.LocalCoord(size + vec).Should().Be(vec,
                "because local is global less chunk size");
            vec = new Vector(-5, -9);
            Chunk.LocalCoord(size + vec).Should().Be(size + vec,
                "because local is global less chunk size regardless of sign");
            vec = new Vector(0, -31);
            Chunk.LocalCoord(size + vec).Should().Be(new Vector(0, 1),
                "because local is global less chunk size by axis regardless of sign");
        }

        [Test]
        public void Coord_To_Chunk_Conversion_Correct()
        {
            Chunk.Coord2Chunk(0, 0).Should().Be(Vector.Zero,
                "because the origin of the origin chunk is the origin");
            Chunk.Coord2Chunk(5, 9).Should().Be(Vector.Zero,
                "because [5 9] lies in the zeroth chunk");
            var size = new Vector(Chunk.ChunkSize, Chunk.ChunkSize);
            Chunk.Coord2Chunk(size).Should().Be(new Vector(1, 1),
                "because chunk indices increase at chunk boundaries");
            var vec = new Vector(-5, -9);
            Chunk.Coord2Chunk(vec).Should().Be(new Vector(-1, -1),
                "because negative coords lay in negative chunks");
        }
    }
}
