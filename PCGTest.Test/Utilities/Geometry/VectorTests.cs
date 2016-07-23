using System;
using PCGTest.Utilities.Geometry;
using NUnit.Framework;
using FluentAssertions;

namespace PCGTest.Test.Utilities.Geometry
{
    [TestFixture]
    class VectorTests
    {
        [Test]
        public void Equality_Works()
        {
            var rand = new Random();
            var x = rand.Next();
            var y = rand.Next();
            var z = rand.Next();
            var vec1 = new Vector(x, y, z);
            var vec2 = new Vector(x, y, z);
            (vec1 == vec2).Should().BeTrue("because all vector elements are equal");
            (Vector.Xaxis == Vector.Yaxis).Should().BeFalse("because X != Y");
            (Vector.Xaxis != Vector.Yaxis).Should().BeTrue("because X != Y");
        }

        [Test]
        public void Addition_Works()
        {
            Vector vec1, vec2, res, expect;
            // Positive addition
            vec1 = new Vector(1, 2, 3);
            vec2 = new Vector(3, 2, 1);
            res = vec1 + vec2;
            expect = new Vector(4, 4, 4);
            res.Should().Be(expect, "because addition is element-wise");
            // Negative addition
            vec1 = new Vector(-1, 2, -3);
            vec2 = new Vector(3, -2, 1);
            res = vec1 + vec2;
            expect = new Vector(2, 0, -2);
            res.Should().Be(expect, "because adding negatives subtracts");
            // In-place addition
            vec1 += vec2;
            vec1.Should().Be(expect, "because adding negatives in-place subtracts");
        }

        [Test]
        public void Subtraction_Works()
        {
            Vector vec1, vec2, res, expect;
            vec1 = new Vector(1, 2, 3);
            vec2 = new Vector(3, 2, 1);
            res = vec1 - vec2;
            expect = new Vector(-2, 0, 2);
            res.Should().Be(expect, "because subtraction is order dependent");
            vec1 -= vec2;
            vec1.Should().Be(expect, "because in-place subtraction is order dependent");
        }

        [Test]
        public void Negation_Works()
        {
            var vec = new Vector(1, 2, 3);
            var res = -vec;
            res.Should().Be(new Vector(-1, -2, -3), "because negative of positive is negative");
            res = -res;
            res.Should().Be(vec, "because negative of negatives is positive");
        }

        [Test]
        public void Vector_Muliplication_Works()
        {
            Vector vec1, vec2, res, expect;
            vec1 = new Vector(1, 2, 3);
            vec2 = new Vector(3, 2, 1);
            res = vec1 * vec2;
            expect = new Vector(3, 4, 3);
            res.Should().Be(expect, "because element-wise multiplication");
            vec1 *= vec2;
            vec1.Should().Be(expect, "because in-place element-wise multiplication");
        }

        [Test]
        public void Scalar_Muliplication_Works()
        {
            Vector vec, res, expect;
            vec = new Vector(1, 2, 3);
            var scale = 5;
            res = vec * scale;
            expect = new Vector(5, 10, 15);
            res.Should().Be(expect, "because scalar multiplication");
            res = scale * vec;
            res.Should().Be(expect, "because reverse scalar multiplication");
            vec *= scale;
            vec.Should().Be(expect, "because in-place scalar multiplication");
        }

        [Test]
        public void Vector_Division_Works()
        {
            Vector vec1, vec2, res, expect;
            vec1 = new Vector(1, 2, 3);
            vec2 = new Vector(3, 2, 1);
            res = vec1 / vec2;
            expect = new Vector(0, 1, 3);
            res.Should().Be(expect, "because element-wise division");
            vec1 /= vec2;
            vec1.Should().Be(expect, "because in-place element-wise division");
        }

        [Test]
        public void Scalar_Division_Works()
        {
            Vector vec, res, expect;
            vec = new Vector(3, 6, 9);
            var scale = 3;
            res = vec / scale;
            expect = new Vector(1, 2, 3);
            res.Should().Be(expect, "because scalar division");
            vec /= scale;
            vec.Should().Be(expect, "because in-place scalar division");
        }

        [Test]
        public void Vector_Modulo_Works()
        {
            Vector vec1, vec2, res, expect;
            vec1 = new Vector(9, 9, 9);
            vec2 = new Vector(3, 2, 1);
            res = vec1 % vec2;
            expect = new Vector(0, 1, 0);
            res.Should().Be(expect, "because element-wise modulo");
            vec1 %= vec2;
            vec1.Should().Be(expect, "because in-place element-wise modulo");
        }

        [Test]
        public void Scalar_Modulo_Works()
        {
            Vector vec, res, expect;
            vec = new Vector(7, 8, 9);
            var scale = 3;
            res = vec % scale;
            expect = new Vector(1, 2, 0);
            res.Should().Be(expect, "because scalar modulo");
            vec %= scale;
            vec.Should().Be(expect, "because in-place scalar modulo");
        }

        [Test]
        public void Dot_Product_Works()
        {
            Vector vec1, vec2;
            int res, expect;
            vec1 = new Vector(1, 2, 3);
            vec2 = new Vector(3, 2, 1);
            res = Vector.Dot(vec1, vec2);
            expect = 3 + 4 + 3;
            res.Should().Be(expect, "because dot product is sum of products");
        }

        [Test]
        public void Abs_Works()
        {
            Vector vec, res, expect;
            vec = new Vector(-1, -2, -3);
            res = vec.Abs();
            expect = new Vector(1, 2, 3);
            res.Should().Be(expect, "because abs of negative is positive");
            res = expect.Abs();
            res.Should().Be(expect, "because abs of positive is positive");
        }

        [Test]
        public void Chebyshev_Is_Girded_Distance()
        {
            var rand = new Random();
            Vector vec;
            int dist;
            var scale = rand.Next();
            // Basic orthogonal tests
            vec = Vector.Xaxis * scale;
            dist = vec.Chebyshev;
            dist.Should().Be(scale, "because Cheby is the same as orthogonal (X)");
            vec = -vec;
            dist = vec.Chebyshev;
            dist.Should().Be(scale, "because Cheby is the same as orthogonal (-X)");
            vec = Vector.Yaxis * scale;
            dist = vec.Chebyshev;
            dist.Should().Be(scale, "because Cheby is the same as orthogonal (Y)");
            vec = -vec;
            dist = vec.Chebyshev;
            dist.Should().Be(scale, "because Cheby is the same as orthogonal (-Y)");
            // Test non-orthogonal vectors
            vec = new Vector(15, 27);
            dist = vec.Chebyshev;
            dist.Should().Be(27, "because [15 27] reaches 27 cells away");
            vec = -vec;
            dist.Should().Be(27, "because [-15 -27] reaches 27 cells away");
            vec = new Vector(65, 27);
            dist = vec.Chebyshev;
            dist.Should().Be(65, "because [65 27] reaches 65 cells away");
            vec = -vec;
            dist.Should().Be(65, "because [-65 -27] reaches 65 cells away");
        }

        [Test]
        public void Manhattan_Is_Diagonal_Distance()
        {
            var rand = new Random();
            Vector vec;
            int dist;
            var scale = rand.Next();
            int expect;
            // Basic orthogonal tests
            vec = Vector.Xaxis * scale;
            dist = vec.Manhattan;
            dist.Should().Be(scale, "because Manhattan is the same as orthogonal (X)");
            vec = -vec;
            dist = vec.Manhattan;
            dist.Should().Be(scale, "because Manhattan is the same as orthogonal (-X)");
            vec = Vector.Yaxis * scale;
            dist = vec.Manhattan;
            dist.Should().Be(scale, "because Manhattan is the same as orthogonal (Y)");
            vec = -vec;
            dist = vec.Manhattan;
            dist.Should().Be(scale, "because Manhattan is the same as orthogonal (-Y)");
            // Test non-orthogonal vectors
            vec = new Vector(15, 27);
            dist = vec.Manhattan;
            expect = 42;
            dist.Should().Be(expect, $"because [15 27] goes through {expect} cells");
            vec = -vec;
            dist.Should().Be(expect, $"because [-15 -27] goes through {expect} cells");
            vec = new Vector(65, 27);
            dist = vec.Manhattan;
            expect = 92;
            dist.Should().Be(expect, $"because [65 27] goes through {expect} cells");
            vec = -vec;
            dist.Should().Be(expect, $"because [-65 -27] goes through {expect} cells");
        }

        [Test]
        public void SquareLength_Is_Square_Of_Length()
        {
            // Using 3-4-5 triangle for known length
            var vec = new Vector(3, 4);
            vec.SquareLength.Should().Be(25, "because 5^2 is 25");
        }

        [Test]
        public void Length_Is_Correct()
        {
            // Using 3-4-5 triangle for known length
            var vec = new Vector(3, 4);
            vec.Length.Should().Be(5, "because sqrt(3^2 + 4^2) = 5");
        }

        [Test]
        public void Right_Is_Righthand_Vector()
        {
            var vec = Vector.Yaxis;
            vec.Right.Should().Be(Vector.Xaxis, "because X is right of Y");
        }

        [Test]
        public void Left_Is_Lefthand_Vector()
        {
            var vec = Vector.Yaxis;
            vec.Left.Should().Be(-Vector.Xaxis, "because -X is left of Y");
        }

        [Test]
        public void Hashes_Are_Different()
        {
            int h1, h2, h3;
            h1 = Vector.Xaxis.GetHashCode();
            h2 = Vector.Yaxis.GetHashCode();
            h3 = Vector.Zaxis.GetHashCode();
            (h1 == h2).Should().BeFalse("because X is different than Y");
            (h1 == h3).Should().BeFalse("because X is different than Z");
            (h2 == h3).Should().BeFalse("because Y is different than Z");
            // Two different random vectors shouldn't have a hash collision (often)
            var rand = new Random();
            var vec1 = new Vector(rand.Next(), rand.Next(), rand.Next());
            var vec2 = new Vector(rand.Next(), rand.Next(), rand.Next());
            h1 = vec1.GetHashCode();
            h2 = vec2.GetHashCode();
            if (vec1 != vec2)
            {
                (h1 == h2).Should().BeFalse("because different vectors should have different hashes");
            }
        }
    }
}
