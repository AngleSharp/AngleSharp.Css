namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The kind of a gradient.
    /// </summary>
    public enum CanvasGradientKind : byte
    {
        /// <summary>
        /// Along the line between two points (x0, y0, x1, y1).
        /// </summary>
        Linear,
        /// <summary>
        /// Between two circles (x0, y0, r0, x1, y1, r1).
        /// </summary>
        Radial,
        /// <summary>
        /// Around a point (startAngle, x, y).
        /// </summary>
        Conic,
    }

    /// <summary>
    /// A color stop of a gradient.
    /// </summary>
    public sealed class CanvasColorStop
    {
        internal CanvasColorStop(Double offset, String color)
        {
            Offset = offset;
            Color = color;
        }

        /// <summary>
        /// Gets the offset, from 0 to 1.
        /// </summary>
        public Double Offset { get; }

        /// <summary>
        /// Gets the serialized color, e.g., #ff0000.
        /// </summary>
        public String Color { get; }
    }

    /// <summary>
    /// A gradient, which can be used as a fill or stroke style.
    /// https://html.spec.whatwg.org/multipage/canvas.html#canvasgradient
    /// </summary>
    [DomName("CanvasGradient")]
    public sealed class CanvasGradient
    {
        private readonly List<CanvasColorStop> _stops = new();

        internal CanvasGradient(CanvasGradientKind kind, params Double[] parameters)
        {
            Kind = kind;
            Parameters = parameters;
        }

        /// <summary>
        /// Gets the kind of the gradient.
        /// </summary>
        public CanvasGradientKind Kind { get; }

        /// <summary>
        /// Gets the parameters the gradient has been created with, as described by its kind.
        /// </summary>
        public IReadOnlyList<Double> Parameters { get; }

        /// <summary>
        /// Gets the color stops in the order they have been added.
        /// </summary>
        public IReadOnlyList<CanvasColorStop> ColorStops => _stops;

        /// <summary>
        /// Adds a color stop.
        /// </summary>
        /// <param name="offset">The offset, from 0 to 1.</param>
        /// <param name="color">The CSS color.</param>
        /// <exception cref="DomException">
        /// An IndexSizeError if the offset is out of range, or a SyntaxError if the color is not valid.
        /// </exception>
        [DomName("addColorStop")]
        public void AddColorStop(Double offset, String color)
        {
            if (Double.IsNaN(offset) || offset < 0 || offset > 1)
                throw new DomException(DomError.IndexSizeError);

            var normalized = CanvasColor.Normalize(color) ?? throw new DomException(DomError.Syntax);
            _stops.Add(new CanvasColorStop(offset, normalized));
        }
    }
}
