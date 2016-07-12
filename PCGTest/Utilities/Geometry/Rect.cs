using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Utilities.Geometry
{
    /// <summary>
    /// 2D Rectangle class largely cribbed from MonoGame: 
    /// </summary>
    public struct Rect : IEquatable<Rect>
    {
       private static Rect emptyRect = new Rect();

        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Rect Empty
        {
            get { return emptyRect; }
        }
        public bool Equals(Rect other)
        {
            return this == other;
        }

        public static bool operator ==(Rect a, Rect b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        public static bool operator !=(Rect a, Rect b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return (obj is Rect) ? this == ((Rect)obj) : false;
        }
        public override int GetHashCode()
        {
            // Bitwise or fields
            return (X ^ Y ^ Width ^ Height);
        }
    }
}
