namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A path that can be built once and then filled, stroked, or used for clipping and
    /// hit testing. Its operations are recorded, e.g., to replay them in a renderer.
    /// https://html.spec.whatwg.org/multipage/canvas.html#path2d-objects
    /// </summary>
    [DomName("Path2D")]
    public sealed class Path2D : ICanvasPath
    {
        private readonly List<PaintCommand> _commands;
        private readonly CanvasPathBuilder _builder;

        /// <summary>
        /// Creates a new empty path.
        /// </summary>
        [DomConstructor]
        public Path2D()
        {
            _commands = new List<PaintCommand>();
            _builder = new CanvasPathBuilder(_commands, new PathGeometry());
        }

        /// <summary>
        /// Creates a copy of the given path.
        /// </summary>
        /// <param name="path">The path to copy.</param>
        [DomConstructor]
        public Path2D(Path2D path)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            _commands = new List<PaintCommand>(path._commands);
            _builder = new CanvasPathBuilder(_commands, path.Geometry.Clone());
        }

        /// <summary>
        /// Creates a path of the given SVG path data, which is kept as it is (as a command
        /// named d) for the renderer. The data does not contribute to hit testing.
        /// </summary>
        /// <param name="d">The SVG path data, e.g., M 0 0 L 10 10.</param>
        [DomConstructor]
        public Path2D(String d)
            : this()
        {
            if (d is null)
                throw new ArgumentNullException(nameof(d));

            _commands.Add(new PaintCommand("d", d));
        }

        /// <summary>
        /// Gets the recorded operations that built the path.
        /// </summary>
        public IReadOnlyList<PaintCommand> Commands => _commands;

        internal PathGeometry Geometry => _builder.Geometry;

        /// <summary>
        /// Adds the subpaths of the given path.
        /// </summary>
        /// <param name="path">The path to add.</param>
        [DomName("addPath")]
        public void AddPath(Path2D path)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            Geometry.Append(path.Geometry, PaintMatrix.Identity);
            _commands.Add(new PaintCommand("addPath", new Path2D(path)));
        }

        /// <inheritdoc />
        public void ClosePath() => _builder.ClosePath();

        /// <inheritdoc />
        public void MoveTo(Double x, Double y) => _builder.MoveTo(x, y, PaintMatrix.Identity);

        /// <inheritdoc />
        public void LineTo(Double x, Double y) => _builder.LineTo(x, y, PaintMatrix.Identity);

        /// <inheritdoc />
        public void QuadraticCurveTo(Double cpx, Double cpy, Double x, Double y) =>
            _builder.QuadraticCurveTo(cpx, cpy, x, y, PaintMatrix.Identity);

        /// <inheritdoc />
        public void BezierCurveTo(Double cp1x, Double cp1y, Double cp2x, Double cp2y, Double x, Double y) =>
            _builder.BezierCurveTo(cp1x, cp1y, cp2x, cp2y, x, y, PaintMatrix.Identity);

        /// <inheritdoc />
        public void ArcTo(Double x1, Double y1, Double x2, Double y2, Double radius) =>
            _builder.ArcTo(x1, y1, x2, y2, radius, PaintMatrix.Identity);

        /// <inheritdoc />
        public void Rect(Double x, Double y, Double w, Double h) => _builder.Rect(x, y, w, h, PaintMatrix.Identity);

        /// <inheritdoc />
        public void RoundRect(Double x, Double y, Double w, Double h, params Double[] radii) =>
            _builder.RoundRect(x, y, w, h, radii, PaintMatrix.Identity);

        /// <inheritdoc />
        public void Arc(Double x, Double y, Double radius, Double startAngle, Double endAngle, Boolean counterclockwise = false) =>
            _builder.Arc(x, y, radius, startAngle, endAngle, counterclockwise, PaintMatrix.Identity);

        /// <inheritdoc />
        public void Ellipse(Double x, Double y, Double radiusX, Double radiusY, Double rotation, Double startAngle, Double endAngle, Boolean counterclockwise = false) =>
            _builder.Ellipse(x, y, radiusX, radiusY, rotation, startAngle, endAngle, counterclockwise, PaintMatrix.Identity);

        /// <summary>
        /// Gets the operations of the path, e.g., Path2D(moveTo(0, 0), lineTo(10, 10)).
        /// </summary>
        public override String ToString() => $"Path2D({String.Join(", ", _commands)})";
    }
}
