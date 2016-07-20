using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using PCGTest.Display.MonoGame;

namespace PCGTest.Test.Display.MonoGame
{
    // NOTE: These test the internal implementation of the map solver!
    [TestFixture]
    class MapSolverTest
    {
        [Test]
        public void IsBorder_Returns_True_For_Borders()
        {
            var map = new int[,] {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            for (var y = 0; y < 3; y++)
            {
                for (var x = 0; x < 3; x++)
                {
                    if (x == 1 && y == 1)
                    {
                        MapSolver.IsBorder(x, y, map).Should().BeFalse();
                    } else
                    {
                        MapSolver.IsBorder(x, y, map).Should().BeTrue();
                    }
                }
            }
        }

        [Test]
        public void CalculateNeighborhood_Returns_Matched_Tile_Bits()
        {
            var map = new int[3, 3];
            var bits = new Dictionary<string, int>()
            {
                //   Pivot
                //     V
                { "000010001", 1 },
                { "010111010", 90 },
                { "100010001", 129 },
                { "000111000", 24 },
                { "011010110", 102 },
                { "100010000", 128 }
            };
            int x, y, hood;
            foreach (var pair in bits)
            {
                for (var i = 0; i < 9; i++)
                {
                    x = i % 3;
                    y = i / 3;
                    map[x, y] = pair.Key[i];
                }
                hood = MapSolver.CalculateNeighborhood(1, 1, map);
                hood.Should().Be(pair.Value);
            }
        }

        [Test]
        public void SolveTile_Returns_Expected_TileTypes()
        {
            var map = new int[3, 3];
            var bits = new Dictionary<string, TileType>()
            {
                //   Pivot
                //     V
                { "000010001", TileType.Single },
                { "010111010", TileType.Cross },
                { "100010001", TileType.Single },
                { "000111000", TileType.Horizontal },
                { "011010110", TileType.Vertical },
                { "100010000", TileType.Single }
            };
            int x, y;
            TileType kind;
            foreach (var pair in bits)
            {
                for (var i = 0; i < 9; i++)
                {
                    x = i % 3;
                    y = i / 3;
                    map[x, y] = pair.Key[i];
                }
                kind = MapSolver.SolveTile(1, 1, map);
                kind.Should().Be(pair.Value);
            }
        }
    }

    [TestFixture]
    class TileProviderTest
    {
        [Test]
        public void GetExpansionData_Returns_Data_For_Known_Types()
        {
            KeyValuePair<int, string>[] expansion;
            // Walls
            expansion = TileProvider.GetExpansionData("Wall.something");
            expansion.Should().NotBeEmpty();
            expansion = TileProvider.GetExpansionData("wall.anything");
            expansion.Should().NotBeEmpty();
            // Floors
            expansion = TileProvider.GetExpansionData("Floor.something");
            expansion.Should().NotBeEmpty();
            expansion = TileProvider.GetExpansionData("floor.anything");
            expansion.Should().NotBeEmpty();
            // Pits
            expansion = TileProvider.GetExpansionData("Pit.something");
            expansion.Should().NotBeEmpty();
            expansion = TileProvider.GetExpansionData("pit.anything");
            expansion.Should().NotBeEmpty();
            // UI
            expansion = TileProvider.GetExpansionData("UI.something");
            expansion.Should().NotBeEmpty();
            expansion = TileProvider.GetExpansionData("ui.anything");
            expansion.Should().NotBeEmpty();
            // Anything else
            expansion = TileProvider.GetExpansionData("stuff.something");
            expansion.Should().BeEmpty();
            expansion = TileProvider.GetExpansionData("NoPeriod");
            expansion.Should().BeEmpty();
        }

        [Test]
        public void AddTileType_Should_Register_Shifted_Key()
        {
            var pair = new KeyValuePair<int, string>(1, "single");
            var provider = new TileProvider();
            provider.AddTileType(pair);
            var shifted = pair.Key << provider._shift;
            provider[shifted].Should().Be(pair.Value);
        }

        [Test]
        public void AddTileType_Should_Register_Expanded_Keys()
        {
            var pair = new KeyValuePair<int, string>(1, "wall.foo");
            var provider = new TileProvider();
            provider.AddTileType(pair);
            var shifted = pair.Key << provider._shift;
            var result = provider[shifted + 1];
            result.Should().StartWith(pair.Value);
        }

        [Test]
        public void ResolveMap_Returns_Map_Using_Expanded_Keys()
        {
            var keys = new Dictionary<int, string>()
            {
                { 0, "Blank" },
                { 1, "Floor" },
                { 2, "Wall.one" },
                { 3, "wall.two" },
            };
            var provider = new TileProvider();
            foreach (var key in keys)
            {
                provider.AddTileType(key);
            }
            var map = new int[,] {
                { 1, 1, 1, 2, 1, 1, 1 },
                { 1, 2, 2, 2, 2, 2, 1 },
                { 1, 2, 1, 1, 1, 3, 1 },
                { 2, 2, 1, 9, 1, 3, 3 },
                { 1, 2, 1, 1, 1, 3, 1 },
                { 1, 3, 3, 3, 3, 3, 1 },
                { 1, 1, 1, 3, 1, 1, 1 }
            };
            var result = provider.ResolveMap(map);
            result.GetLength(0).Should().Be(7, "because that is the input width");
            result.GetLength(1).Should().Be(7, "because that is the input height");
            result[3, 3].Should().Be(0, "because unknown keys resolve to 0");
            int source, res, unshifted, lower;
            for (var y = 0; y < 7; y++)
            {
                for (var x = 0; x < 7; x++)
                {
                    if ((x == 3 && y == 3) || MapSolver.IsBorder(x, y, map))
                        continue;
                    source = map[x, y];
                    res = result[x, y];
                    unshifted = res >> provider._shift;
                    unshifted.Should().Be(source, "because right shift drops lower bits");
                    lower = (source << provider._shift) ^ res;
                    lower.Should().NotBe(0);
                }
            }
        }
    }
}
