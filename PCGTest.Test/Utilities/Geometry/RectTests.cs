using System;
using PCGTest.Utilities.Geometry;
using NUnit.Framework;
using FluentAssertions;

namespace PCGTest.Test.Utilities.Geometry
{
    [TestFixture]
    class RectTests
    {
        [Test]
        public void Initialization_Creates_Valid_Rects()
        {
            Rect r;
            r = new Rect();
            r.Should().Be(Rect.EmptyRect, "because the empty constructor creates an empty rect");
            r = new Rect(1, 2, 3, 4);
            r.Left.Should().Be(1, "because of x initialization");
            r.Top.Should().Be(2, "because of y initialization");
            r.Width.Should().Be(3, "because of width initialization");
            r.Height.Should().Be(4, "because of height initialization");
            r.Right.Should().Be(1 + 3, "because right is x + width");
            r.Bottom.Should().Be(2 + 4, "because bottom is y + height");
            r = new Rect(-4, -3, 2, 1);
            r.Left.Should().Be(-4, "because of x initialization (negative)");
            r.Top.Should().Be(-3, "because of y initialization (negative)");
            r.Width.Should().Be(2, "because of width initialization (negative)");
            r.Height.Should().Be(1, "because of height initialization (negative)");
            r.Right.Should().Be(-4 + 2, "because right is x + width (negative)");
            r.Bottom.Should().Be(-3 + 1, "because bottom is y + height (negative)");
            r = new Rect(0, 0, -5, -4);
            r.Left.Should().Be(-5, "because negative width replaces x");
            r.Top.Should().Be(-4, "because negative height replaces y");
            r.Width.Should().Be(5, "because width is absolute value of width");
            r.Height.Should().Be(4, "because height is absolute value of height");
            r.Right.Should().Be(0, "because left becomes right with negative width");
            r.Bottom.Should().Be(0, "because top becomes bottom with negative height");
        }

        [Test]
        public void Center_Is_In_Middle()
        {
            Rect rect;
            Vector expect;
            rect = new Rect(0, 0, 5, 5);
            expect = new Vector(2, 2);
            rect.Center.Should().Be(expect, "because tile [2 2] is middle of 5x5 grid");
            rect = new Rect(-5, -5, 5, 5);
            expect = new Vector(-3, -3); // I would expect this to be [-2 -2]...
            rect.Center.Should().Be(expect, "because tile [-3 -3] is middle of -5x-5 grid");
            rect = new Rect(0, 0, 6, 6);
            expect = new Vector(3, 3);
            rect.Center.Should().Be(expect, "because tile [2 2] is approx middle of 6x6 grid");
        }

        [Test]
        public void Setting_Center_Moves_Rect()
        {
            Rect rect;
            Vector expect;
            rect = new Rect(0, 0, 5, 5);
            rect.Center += new Vector(2, 2);
            expect = new Vector(4, 4);
            rect.Center.Should().Be(expect, "because tile [2 2] + [2 2] is [4 4]");
        }

        [Test]
        public void Coordinate_Containment_Works()
        {
            var rect = new Rect(2, 2, 4, 4);
            // Positive tests
            rect.Contains(4, 4).Should().BeTrue("because [4 4] is inside 2->6 rect");
            rect.Contains(new Vector(4, 4)).Should().BeTrue("because [4 4] is inside 2->6 rect");
            rect.Contains(2, 2).Should().BeTrue("because [2 2] is inside 2->6 rect");
            rect.Contains(new Vector(2, 2)).Should().BeTrue("because [2 2] is inside 2->6 rect");
            rect.Contains(3, 4).Should().BeTrue("because [3 4] is inside 2->6 rect");
            rect.Contains(new Vector(3, 4)).Should().BeTrue("because [3 24 is inside 2->6 rect");
            // Negative tests
            rect.Contains(3, 7).Should().BeFalse("because [3 7] is outside 2->6 rect");
            rect.Contains(new Vector(3, 7)).Should().BeFalse("because [3 7] is outside 2->6 rect");
            rect.Contains(7, 3).Should().BeFalse("because [7 3] is outside 2->6 rect");
            rect.Contains(new Vector(7, 3)).Should().BeFalse("because [7 3] is outside 2->6 rect");
        }

        [Test]
        public void Rect_Containment_Works()
        {
            Rect r1, r2;
            r1 = new Rect(2, 2, 4, 4);
            // Positive tests
            r1.Contains(r1).Should().BeTrue("because a rect contains itself");
            r2 = Rect.UnitRect;
            r2.X += 2;
            r2.Y += 2;
            r1.Contains(r2).Should().BeTrue("because 2->3 is inside 2->6 rect");
            r2.X++;
            r1.Contains(r2).Should().BeTrue("because 3,2->4,3 is inside 2->6 rect");
            r2.Y++;
            r1.Contains(r2).Should().BeTrue("because 3->4 is inside 2->6 rect");
            // Negative tests
            r2.Width += 4;
            r1.Contains(r2).Should().BeFalse("because 3->8,4 overlaps 2->6 rect");
            r2 = Rect.UnitRect;
            r1.Contains(r2).Should().BeFalse("because 0->1 is outside 2->6 rect");
        }

        [Test]
        public void Rect_Intersect_Works()
        {
            Rect r1, r2;
            r1 = new Rect(2, 2, 4, 4);
            r1.Intersects(r1).Should().BeTrue("because a rect intersects itself");
            r2 = new Rect(0, 0, 4, 4);
            r1.Intersects(r2).Should().BeTrue("because 0->4 overlaps 2->6");
            r2.Width += 4;
            r2.Height += 4;
            r1.Intersects(r2).Should().BeTrue("because 0->8 contains 2->6");
            r2 = new Rect(0, 0, 2, 2);
            r1.Intersects(r2).Should().BeTrue("because 0->2 touches 2->6");
            r2.Width--;
            r2.Height--;
            r1.Intersects(r2).Should().BeFalse("because 0->1 is outside 2->6");
        }

        [Test]
        public void Rect_Intersection_Works()
        {
            Rect r1, r2, res, expect;
            r1 = new Rect(2, 2, 4, 4);
            r2 = new Rect(2, 2, 4, 4);
            res = r1.Intersection(r2);
            res.Should().Be(r1, "because intersecting same rect returns whole rect");
            r2 = new Rect(2, 2, 1, 1);
            res = r1.Intersection(r2);
            res.Should().Be(r2, "because intersecting a sub-rect returns the sub-rect");
            r2 = new Rect(0, 3, 8, 2);
            res = r1.Intersection(r2);
            expect = new Rect(2, 3, 4, 2);
            res.Should().Be(expect, "because intersection is overlapped area");
            r2 = new Rect(-4, -3, 4, 4);
            res = r1.Intersection(r2);
            expect = Rect.EmptyRect;
            res.Should().Be(expect, "because non-overlaping intersection is empty rect");
            r1 = new Rect(0, -3, 1, 6);
            r2 = new Rect(-3, 0, 6, 1);
            expect = Rect.UnitRect;
            res = r1.Intersection(r2);
            res.Should().Be(expect, "because 1xN and Nx1 overlap at 0->1");
        }

        [Test]
        public void Rect_Equality_Works()
        {
            Rect r1, r2;
            r1 = new Rect(2, 2, 4, 4);
            (r1 == r1).Should().BeTrue("because a rect equals itself");
            r2 = new Rect(0, 0, 4, 4);
            (r1 == r2).Should().BeFalse("because rects are different");
        }
    }
}
