using System;

namespace PCGTest.Utilities.Geometry
{
    public struct Vector: IEquatable<Vector>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public static readonly Vector Zero = new Vector(0, 0, 0);
        public static readonly Vector Xaxis = new Vector(1, 0, 0);
        public static readonly Vector Yaxis = new Vector(0, 1, 0);
        public static readonly Vector Zaxis = new Vector(0, 0, 1);

        public Vector(int value)
        {
            X = value;
            Y = value;
            Z = 0;
        }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector operator +(Vector v1, Vector v2) =>
            new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        public static Vector operator -(Vector v) =>
            new Vector(-v.X, -v.Y, -v.Z);
        public static Vector operator -(Vector v1, Vector v2) =>
            new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        public static Vector operator *(Vector v1, Vector v2) =>
            new Vector(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);

        public static Vector operator *(int s, Vector v) => v * s;
        public static Vector operator *(Vector v, int s) =>
            new Vector(v.X * s, v.Y * s, v.Z * s);

        public static Vector operator /(Vector v1, Vector v2) =>
            new Vector(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);

        public static Vector operator /(Vector v, int s) =>
            new Vector(v.X / s, v.Y / s, v.Z / s);

        public static Vector operator %(Vector v1, Vector v2) =>
            new Vector(v1.X % v2.X, v1.Y % v2.Y, v1.Z % v2.Z);

        public static Vector operator %(Vector v, int s) =>
            new Vector(v.X % s, v.Y % s, v.Z % s);

        public static int Dot(Vector v1, Vector v2) =>
            (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);

        public Vector Abs() => new Vector(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));

        public int Chebyshev
        {
            get
            {
                var a = Abs();
                return Math.Max(Math.Max(a.X, a.Y), a.Z);
            }
        }
        public int Manhattan
        {
            get
            {
                var a = Abs();
                return (a.X + a.Y + a.Z);
            }
        }

        public int SquareLength => Dot(this, this);
        public double Length => Math.Sqrt(SquareLength);

        public Vector Left => new Vector(-Y, X, Z);
        public Vector Right => new Vector(Y, -X, Z);

        public static bool operator ==(Vector a, Vector b)
        {
            return (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z);
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        public bool Equals(Vector other)
        {
            return this == other;
        }
        public override bool Equals(object obj)
        {
            return (obj is Vector) ? this == ((Vector)obj) : false;
        }
        public override int GetHashCode()
        {
            // Bitwise xor rotated fields
            return (X ^ Y << 2 ^ Z >> 2);
        }

        public override string ToString()
        {
            return $"Vector({X}, {Y}, {Z})";
        }
    }
}
