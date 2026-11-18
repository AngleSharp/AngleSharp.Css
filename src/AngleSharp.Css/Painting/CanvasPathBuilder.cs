namespace AngleSharp.Css.Painting
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Validates and records the operations of the CanvasPath mixin, and builds the
    /// geometry of the path from them, which is shared by the rendering context (in the
    /// coordinates of the output, i.e., with the current transform) and Path2D (in its
    /// own coordinates).
    /// </summary>
    sealed class CanvasPathBuilder
    {
        private readonly List<PaintCommand> _commands;

        public CanvasPathBuilder(List<PaintCommand> commands, PathGeometry geometry)
        {
            _commands = commands;
            Geometry = geometry;
        }

        public PathGeometry Geometry { get; }

        public void ClosePath()
        {
            Geometry.ClosePath();
            Record("closePath");
        }

        public void MoveTo(Double x, Double y, PaintMatrix m)
        {
            if (Finite(x, y))
            {
                Geometry.MoveTo(m.Apply(x, y));
                Record("moveTo", x, y);
            }
        }

        public void LineTo(Double x, Double y, PaintMatrix m)
        {
            if (Finite(x, y))
            {
                Geometry.LineTo(m.Apply(x, y));
                Record("lineTo", x, y);
            }
        }

        public void QuadraticCurveTo(Double cpx, Double cpy, Double x, Double y, PaintMatrix m)
        {
            if (Finite(cpx, cpy, x, y))
            {
                Geometry.QuadraticCurveTo(m.Apply(cpx, cpy), m.Apply(x, y));
                Record("quadraticCurveTo", cpx, cpy, x, y);
            }
        }

        public void BezierCurveTo(Double cp1x, Double cp1y, Double cp2x, Double cp2y, Double x, Double y, PaintMatrix m)
        {
            if (Finite(cp1x, cp1y, cp2x, cp2y, x, y))
            {
                Geometry.BezierCurveTo(m.Apply(cp1x, cp1y), m.Apply(cp2x, cp2y), m.Apply(x, y));
                Record("bezierCurveTo", cp1x, cp1y, cp2x, cp2y, x, y);
            }
        }

        public void ArcTo(Double x1, Double y1, Double x2, Double y2, Double radius, PaintMatrix m)
        {
            if (Finite(x1, y1, x2, y2, radius))
            {
                if (radius < 0)
                    throw new DomException(DomError.IndexSizeError);

                Geometry.ArcTo(x1, y1, x2, y2, radius, m);
                Record("arcTo", x1, y1, x2, y2, radius);
            }
        }

        public void Rect(Double x, Double y, Double w, Double h, PaintMatrix m)
        {
            if (Finite(x, y, w, h))
            {
                Geometry.Rect(x, y, w, h, m);
                Record("rect", x, y, w, h);
            }
        }

        public void RoundRect(Double x, Double y, Double w, Double h, Double[] radii, PaintMatrix m)
        {
            radii ??= new Double[] { 0 };

            if (radii.Length < 1 || radii.Length > 4)
                throw new ArgumentOutOfRangeException(nameof(radii), "Between one and four radii are required.");

            if (Finite(x, y, w, h) && Finite(radii))
            {
                foreach (var radius in radii)
                {
                    if (radius < 0)
                        throw new ArgumentOutOfRangeException(nameof(radii), "A radius must not be negative.");
                }

                var corners = radii.Length switch
                {
                    1 => new[] { radii[0], radii[0], radii[0], radii[0] },
                    2 => new[] { radii[0], radii[1], radii[0], radii[1] },
                    3 => new[] { radii[0], radii[1], radii[2], radii[1] },
                    _ => new[] { radii[0], radii[1], radii[2], radii[3] },
                };

                Geometry.RoundRect(x, y, w, h, corners, m);
                Record("roundRect", x, y, w, h, (Double[])radii.Clone());
            }
        }

        public void Arc(Double x, Double y, Double radius, Double startAngle, Double endAngle, Boolean counterclockwise, PaintMatrix m)
        {
            if (Finite(x, y, radius, startAngle, endAngle))
            {
                if (radius < 0)
                    throw new DomException(DomError.IndexSizeError);

                Geometry.Ellipse(x, y, radius, radius, 0, startAngle, endAngle, counterclockwise, m);
                Record("arc", x, y, radius, startAngle, endAngle, counterclockwise);
            }
        }

        public void Ellipse(Double x, Double y, Double radiusX, Double radiusY, Double rotation, Double startAngle, Double endAngle, Boolean counterclockwise, PaintMatrix m)
        {
            if (Finite(x, y, radiusX, radiusY, rotation, startAngle, endAngle))
            {
                if (radiusX < 0 || radiusY < 0)
                    throw new DomException(DomError.IndexSizeError);

                Geometry.Ellipse(x, y, radiusX, radiusY, rotation, startAngle, endAngle, counterclockwise, m);
                Record("ellipse", x, y, radiusX, radiusY, rotation, startAngle, endAngle, counterclockwise);
            }
        }

        private void Record(String name, params Object[] arguments) => _commands.Add(new PaintCommand(name, arguments));

        public static Boolean Finite(params Double[] values)
        {
            foreach (var value in values)
            {
                if (Double.IsNaN(value) || Double.IsInfinity(value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
