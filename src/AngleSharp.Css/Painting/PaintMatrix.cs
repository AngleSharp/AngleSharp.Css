namespace AngleSharp.Css.Painting
{
    using System;

    /// <summary>
    /// A 2D affine transform (a, b, c, d, e, f) as used by the canvas API, which maps
    /// (x, y) to (a x + c y + e, b x + d y + f).
    /// </summary>
    readonly struct PaintMatrix
    {
        public static readonly PaintMatrix Identity = new(1, 0, 0, 1, 0, 0);

        public PaintMatrix(Double a, Double b, Double c, Double d, Double e, Double f)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
        }

        public Double A { get; }

        public Double B { get; }

        public Double C { get; }

        public Double D { get; }

        public Double E { get; }

        public Double F { get; }

        public Double Determinant => A * D - B * C;

        public Boolean IsInvertible => Determinant != 0 && !Double.IsNaN(Determinant) && !Double.IsInfinity(Determinant);

        /// <summary>
        /// Gets the factor lengths are scaled with (exactly for a uniform scale).
        /// </summary>
        public Double Scale => Math.Sqrt(Math.Abs(Determinant));

        /// <summary>
        /// Gets the transform that first applies the other one and then this one.
        /// </summary>
        public PaintMatrix Multiply(PaintMatrix m) => new(
            A * m.A + C * m.B,
            B * m.A + D * m.B,
            A * m.C + C * m.D,
            B * m.C + D * m.D,
            A * m.E + C * m.F + E,
            B * m.E + D * m.F + F);

        public PaintMatrix Invert()
        {
            var det = Determinant;
            return new(
                D / det,
                -B / det,
                -C / det,
                A / det,
                (C * F - D * E) / det,
                (B * E - A * F) / det);
        }

        public PaintPoint Apply(Double x, Double y) => new(A * x + C * y + E, B * x + D * y + F);

        public PaintPoint Apply(PaintPoint p) => Apply(p.X, p.Y);
    }

    readonly struct PaintPoint
    {
        public PaintPoint(Double x, Double y)
        {
            X = x;
            Y = y;
        }

        public Double X { get; }

        public Double Y { get; }
    }
}
