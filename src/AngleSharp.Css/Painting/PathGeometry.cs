namespace AngleSharp.Css.Painting
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The geometry of a path, flattened to polygons, which is only used for hit testing
    /// (isPointInPath and isPointInStroke). The points are given in the coordinates of
    /// the path, i.e., after applying the transform the operations were called with.
    /// </summary>
    sealed class PathGeometry
    {
        private const Int32 CurveSegments = 16;
        private const Int32 CircleSegments = 64;
        private const Double Epsilon = 1e-9;

        private readonly List<SubPath> _subpaths = new();
        private Boolean _needsNewSubpath = true;

        public Boolean IsEmpty => _subpaths.Count == 0;

        public PathGeometry Clone()
        {
            var clone = new PathGeometry();
            clone.Append(this, PaintMatrix.Identity);
            return clone;
        }

        public void Clear()
        {
            _subpaths.Clear();
            _needsNewSubpath = true;
        }

        public void Append(PathGeometry other, PaintMatrix m)
        {
            foreach (var subpath in other._subpaths)
            {
                var copy = new SubPath { Closed = subpath.Closed };

                foreach (var point in subpath.Points)
                {
                    copy.Points.Add(m.Apply(point));
                }

                _subpaths.Add(copy);
            }

            _needsNewSubpath = _needsNewSubpath && other._needsNewSubpath;
        }

        #region Building

        public void MoveTo(PaintPoint p)
        {
            var subpath = new SubPath();
            subpath.Points.Add(p);
            _subpaths.Add(subpath);
            _needsNewSubpath = false;
        }

        public void LineTo(PaintPoint p)
        {
            if (_needsNewSubpath)
            {
                MoveTo(p);
            }
            else
            {
                Current.Points.Add(p);
            }
        }

        public void ClosePath()
        {
            if (!_needsNewSubpath && _subpaths.Count > 0)
            {
                var current = Current;
                current.Closed = true;
                MoveTo(current.Points[0]);
            }
        }

        public void QuadraticCurveTo(PaintPoint cp, PaintPoint end)
        {
            EnsureSubpath(cp);
            var start = Last;

            for (var i = 1; i <= CurveSegments; i++)
            {
                var t = (Double)i / CurveSegments;
                var u = 1 - t;
                LineTo(new PaintPoint(
                    u * u * start.X + 2 * u * t * cp.X + t * t * end.X,
                    u * u * start.Y + 2 * u * t * cp.Y + t * t * end.Y));
            }
        }

        public void BezierCurveTo(PaintPoint cp1, PaintPoint cp2, PaintPoint end)
        {
            EnsureSubpath(cp1);
            var start = Last;

            for (var i = 1; i <= CurveSegments; i++)
            {
                var t = (Double)i / CurveSegments;
                var u = 1 - t;
                LineTo(new PaintPoint(
                    u * u * u * start.X + 3 * u * u * t * cp1.X + 3 * u * t * t * cp2.X + t * t * t * end.X,
                    u * u * u * start.Y + 3 * u * u * t * cp1.Y + 3 * u * t * t * cp2.Y + t * t * t * end.Y));
            }
        }

        public void ArcTo(Double x1, Double y1, Double x2, Double y2, Double radius, PaintMatrix m)
        {
            EnsureSubpath(m.Apply(x1, y1));

            if (!m.IsInvertible)
            {
                LineTo(m.Apply(x1, y1));
                return;
            }

            var p0 = m.Invert().Apply(Last);
            var v1x = p0.X - x1;
            var v1y = p0.Y - y1;
            var v2x = x2 - x1;
            var v2y = y2 - y1;
            var l1 = Math.Sqrt(v1x * v1x + v1y * v1y);
            var l2 = Math.Sqrt(v2x * v2x + v2y * v2y);
            var cross = v1x * v2y - v1y * v2x;

            // the same points, no radius, or a straight line: just a line to the first point
            if (l1 < Epsilon || l2 < Epsilon || radius == 0 || Math.Abs(cross) < Epsilon * l1 * l2)
            {
                LineTo(m.Apply(x1, y1));
                return;
            }

            var u1x = v1x / l1;
            var u1y = v1y / l1;
            var u2x = v2x / l2;
            var u2y = v2y / l2;
            var theta = Math.Acos(Math.Max(-1, Math.Min(1, u1x * u2x + u1y * u2y)));
            var distance = radius / Math.Tan(theta / 2);
            var bx = u1x + u2x;
            var by = u1y + u2y;
            var bl = Math.Sqrt(bx * bx + by * by);
            var centerDistance = radius / Math.Sin(theta / 2);
            var cx = x1 + bx / bl * centerDistance;
            var cy = y1 + by / bl * centerDistance;
            var t1x = x1 + u1x * distance;
            var t1y = y1 + u1y * distance;
            var t2x = x1 + u2x * distance;
            var t2y = y1 + u2y * distance;
            var start = Math.Atan2(t1y - cy, t1x - cx);
            var end = Math.Atan2(t2y - cy, t2x - cx);
            Ellipse(cx, cy, radius, radius, 0, start, end, cross > 0, m);
        }

        public void Ellipse(Double x, Double y, Double rx, Double ry, Double rotation, Double start, Double end, Boolean counterclockwise, PaintMatrix m)
        {
            var sweep = GetSweep(start, end, counterclockwise);
            var segments = Math.Max(1, (Int32)Math.Ceiling(Math.Abs(sweep) / (2 * Math.PI) * CircleSegments));
            var cos = Math.Cos(rotation);
            var sin = Math.Sin(rotation);

            for (var i = 0; i <= segments; i++)
            {
                var t = start + sweep * i / segments;
                var px = rx * Math.Cos(t);
                var py = ry * Math.Sin(t);
                var point = m.Apply(x + px * cos - py * sin, y + px * sin + py * cos);

                // the arc is connected to the current subpath, if there is one
                if (i == 0)
                {
                    LineTo(point);
                }
                else
                {
                    Current.Points.Add(point);
                }
            }
        }

        public void Rect(Double x, Double y, Double w, Double h, PaintMatrix m)
        {
            MoveTo(m.Apply(x, y));
            Current.Points.Add(m.Apply(x + w, y));
            Current.Points.Add(m.Apply(x + w, y + h));
            Current.Points.Add(m.Apply(x, y + h));
            Current.Closed = true;
            MoveTo(m.Apply(x, y));
        }

        /// <summary>
        /// Adds the rectangle with the (normalized) radii of the top-left, top-right,
        /// bottom-right, and bottom-left corners.
        /// </summary>
        public void RoundRect(Double x, Double y, Double w, Double h, Double[] radii, PaintMatrix m)
        {
            var tl = radii[0];
            var tr = radii[1];
            var br = radii[2];
            var bl = radii[3];

            // a negative size flips the rectangle, and the corners with it
            if (w < 0)
            {
                x += w;
                w = -w;
                (tl, tr) = (tr, tl);
                (bl, br) = (br, bl);
            }

            if (h < 0)
            {
                y += h;
                h = -h;
                (tl, bl) = (bl, tl);
                (tr, br) = (br, tr);
            }

            // overlapping corners are scaled down
            var scale = Math.Min(1, Math.Min(
                Math.Min(Ratio(w, tl + tr), Ratio(h, tr + br)),
                Math.Min(Ratio(w, bl + br), Ratio(h, tl + bl))));
            tl *= scale;
            tr *= scale;
            br *= scale;
            bl *= scale;

            MoveTo(m.Apply(x + tl, y));
            Corner(x + w - tr, y + tr, tr, -Math.PI / 2, m);
            Corner(x + w - br, y + h - br, br, 0, m);
            Corner(x + bl, y + h - bl, bl, Math.PI / 2, m);
            Corner(x + tl, y + tl, tl, Math.PI, m);
            Current.Closed = true;
            MoveTo(m.Apply(x, y));
        }

        private void Corner(Double cx, Double cy, Double r, Double start, PaintMatrix m)
        {
            const Int32 segments = CircleSegments / 4;

            for (var i = 0; i <= segments; i++)
            {
                var t = start + Math.PI / 2 * i / segments;
                Current.Points.Add(m.Apply(cx + r * Math.Cos(t), cy + r * Math.Sin(t)));

                if (r == 0)
                {
                    break;
                }
            }
        }

        private static Double Ratio(Double length, Double sum) => sum > 0 ? length / sum : 1;

        private static Double GetSweep(Double start, Double end, Boolean counterclockwise)
        {
            const Double Tau = 2 * Math.PI;

            if (!counterclockwise && end - start >= Tau)
            {
                return Tau;
            }
            else if (counterclockwise && start - end >= Tau)
            {
                return -Tau;
            }

            var sweep = (end - start) % Tau;

            if (!counterclockwise && sweep < 0)
            {
                sweep += Tau;
            }
            else if (counterclockwise && sweep > 0)
            {
                sweep -= Tau;
            }

            return sweep;
        }

        private void EnsureSubpath(PaintPoint p)
        {
            if (_needsNewSubpath)
            {
                MoveTo(p);
            }
        }

        private SubPath Current => _subpaths[_subpaths.Count - 1];

        private PaintPoint Last => Current.Points[Current.Points.Count - 1];

        #endregion

        #region Hit Testing

        /// <summary>
        /// Checks if the point is inside the area of the path, where each subpath is
        /// closed implicitly. Points on the path are inside.
        /// </summary>
        public Boolean Contains(PaintPoint p, CanvasFillRule rule)
        {
            var winding = 0;
            var crossings = 0;

            foreach (var subpath in _subpaths)
            {
                var points = subpath.Points;

                for (var i = 0; i < points.Count; i++)
                {
                    var a = points[i];
                    var b = points[(i + 1) % points.Count];

                    if (DistanceToSegment(p, a, b) < Epsilon)
                    {
                        return true;
                    }

                    if (a.Y <= p.Y)
                    {
                        if (b.Y > p.Y && IsLeft(a, b, p) > 0)
                        {
                            winding++;
                            crossings++;
                        }
                    }
                    else if (b.Y <= p.Y && IsLeft(a, b, p) < 0)
                    {
                        winding--;
                        crossings++;
                    }
                }
            }

            return rule == CanvasFillRule.EvenOdd ? crossings % 2 == 1 : winding != 0;
        }

        /// <summary>
        /// Checks if the point is on a line of the path with the given width. The joins,
        /// caps and dashes of the lines are not considered.
        /// </summary>
        public Boolean StrokeContains(PaintPoint p, Double width)
        {
            var half = width / 2;

            foreach (var subpath in _subpaths)
            {
                var points = subpath.Points;
                var count = subpath.Closed ? points.Count : points.Count - 1;

                for (var i = 0; i < count; i++)
                {
                    if (DistanceToSegment(p, points[i], points[(i + 1) % points.Count]) <= half)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static Double IsLeft(PaintPoint a, PaintPoint b, PaintPoint p) =>
            (b.X - a.X) * (p.Y - a.Y) - (p.X - a.X) * (b.Y - a.Y);

        private static Double DistanceToSegment(PaintPoint p, PaintPoint a, PaintPoint b)
        {
            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            var length = dx * dx + dy * dy;
            var t = length > 0 ? Math.Max(0, Math.Min(1, ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / length)) : 0;
            var x = a.X + t * dx - p.X;
            var y = a.Y + t * dy - p.Y;
            return Math.Sqrt(x * x + y * y);
        }

        #endregion

        private sealed class SubPath
        {
            public List<PaintPoint> Points { get; } = new();

            public Boolean Closed { get; set; }
        }
    }
}
