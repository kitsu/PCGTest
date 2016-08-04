using System;
using System.Collections.Generic;

namespace PCGTest.Utilities.Geometry
{
    /// <summary>
    /// 2D Rectangle class largely cribbed from MonoGame: 
    /// </summary>
    public struct Rect: IEquatable<Rect>
    {
       public static Rect EmptyRect = new Rect();
       public static Rect UnitRect = new Rect(0, 0, 1, 1);

        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Rect(int x, int y, int width, int height)
        {
            if (width < 0)
            {
                X = x + width;
                Width = -width;
            } else
            {
                X = x;
                Width = width;
            }
            if (height < 0)
            {
                Y = y + height;
                Height = -height;
            } else
            {
                Y = y;
                Height = height;
            }
        }

        public int Left => X;
        public int Right => X + Width - 1;
        public int Top => Y;
        public int Bottom => Y + Height - 1;

        public Vector TopLeft => new Vector(Left, Top);
        public Vector TopRight => new Vector(Right, Top);
        public Vector BottomLeft => new Vector(Left, Bottom);
        public Vector BottomRight => new Vector(Right, Bottom);

        public Vector Center
        {
            get
            {
                return new Vector(X + Width/2, Y + Height/2);
            }

            set
            {
                X = value.X - Width/2;
                Y = value.Y - Height/2;
            }
        }

        public bool Contains(Vector coord) => Contains(coord.X, coord.Y);
        public bool Contains(int x, int y)
        {
            return Left <= x && x < Right &&
                   Top <= y && y < Bottom;
        }

        public bool Contains(Rect other)
        {
            return other.Left >= Left &&
                   other.Right <= Right &&
                   other.Top >= Top &&
                   other.Bottom <= Bottom;
        }

        public bool Intersects(Rect other)
        {
            return other.Left <= Right &&
                   Left <= other.Right &&
                   other.Top <= Bottom &&
                   Top <= other.Bottom;
        }

        public Rect Intersection(Rect other)
        {
            if (!Intersects(other))
                return EmptyRect;
            var left = Math.Max(Left, other.Left);
            var top = Math.Max(Top, other.Top);
            var right = Math.Min(Right, other.Right);
            var bottom = Math.Min(Bottom, other.Bottom);
            return new Rect(left, top, 1 + right - left, 1 + bottom - top);
        }

        public IEnumerable<Vector> Coordinates()
        {
            for (var y = Y; y <= Bottom; y++)
            {
                for (var x = X; x <= Right; x++)
                {
                    yield return new Vector(x, y);
                }
            }
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
            // Bitwise xor fields
            return (X ^ Y ^ Width ^ Height);
        }

        public override string ToString()
        {
            return $"Rect({X}, {Y}, {Width}, {Height})";
        }
    }
}
