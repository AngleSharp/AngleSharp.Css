namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The operations building a path (the CanvasPath mixin), shared by the rendering
    /// context and <see cref="Path2D"/>. Operations with non-finite arguments are ignored.
    /// https://html.spec.whatwg.org/multipage/canvas.html#canvaspath
    /// </summary>
    public interface ICanvasPath
    {
        /// <summary>
        /// Closes the current subpath, i.e., adds a line to its start.
        /// </summary>
        [DomName("closePath")]
        void ClosePath();

        /// <summary>
        /// Starts a new subpath at the given point.
        /// </summary>
        [DomName("moveTo")]
        void MoveTo(Double x, Double y);

        /// <summary>
        /// Adds a straight line to the given point.
        /// </summary>
        [DomName("lineTo")]
        void LineTo(Double x, Double y);

        /// <summary>
        /// Adds a quadratic Bézier curve to the given point.
        /// </summary>
        [DomName("quadraticCurveTo")]
        void QuadraticCurveTo(Double cpx, Double cpy, Double x, Double y);

        /// <summary>
        /// Adds a cubic Bézier curve to the given point.
        /// </summary>
        [DomName("bezierCurveTo")]
        void BezierCurveTo(Double cp1x, Double cp1y, Double cp2x, Double cp2y, Double x, Double y);

        /// <summary>
        /// Adds an arc with the given radius touching the lines to both points.
        /// </summary>
        /// <exception cref="AngleSharp.Dom.DomException">An IndexSizeError for a negative radius.</exception>
        [DomName("arcTo")]
        void ArcTo(Double x1, Double y1, Double x2, Double y2, Double radius);

        /// <summary>
        /// Adds a closed subpath of the rectangle.
        /// </summary>
        [DomName("rect")]
        void Rect(Double x, Double y, Double w, Double h);

        /// <summary>
        /// Adds a closed subpath of the rectangle with rounded corners, which has one to
        /// four radii (all corners; top-left and bottom-right, top-right and bottom-left;
        /// top-left, top-right and bottom-left, bottom-right; each corner).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The RangeError for a negative radius or not one to four radii.
        /// </exception>
        [DomName("roundRect")]
        void RoundRect(Double x, Double y, Double w, Double h, params Double[] radii);

        /// <summary>
        /// Adds an arc of the circle.
        /// </summary>
        /// <exception cref="AngleSharp.Dom.DomException">An IndexSizeError for a negative radius.</exception>
        [DomName("arc")]
        void Arc(Double x, Double y, Double radius, Double startAngle, Double endAngle, Boolean counterclockwise = false);

        /// <summary>
        /// Adds an arc of the ellipse.
        /// </summary>
        /// <exception cref="AngleSharp.Dom.DomException">An IndexSizeError for a negative radius.</exception>
        [DomName("ellipse")]
        void Ellipse(Double x, Double y, Double radiusX, Double radiusY, Double rotation, Double startAngle, Double endAngle, Boolean counterclockwise = false);
    }
}
