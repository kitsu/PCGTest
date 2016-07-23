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
