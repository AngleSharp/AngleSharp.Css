namespace AngleSharp.Css.Painting
{
    using AngleSharp.Css.TypedOm;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Draws a paint image.
    /// </summary>
    /// <param name="context">The rendering context to draw with.</param>
    /// <param name="size">The size of the image.</param>
    /// <param name="properties">The input properties of the element.</param>
    /// <param name="arguments">The arguments of the paint() function.</param>
    public delegate void PaintCallback(IPaintRenderingContext2D context, PaintSize size, IStylePropertyMapReadOnly properties, IReadOnlyList<CssStyleValue> arguments);

    /// <summary>
    /// A paint definition implemented in .NET by a callback, e.g., to provide
    /// painters without any scripting engine.
    /// </summary>
    public sealed class PaintDefinition : IPaintDefinition
    {
        private readonly PaintCallback _paint;

        /// <summary>
        /// Creates a new paint definition.
        /// </summary>
        /// <param name="paint">The callback drawing the image.</param>
        public PaintDefinition(PaintCallback paint)
        {
            _paint = paint ?? throw new ArgumentNullException(nameof(paint));
        }

        /// <inheritdoc />
        public IEnumerable<String> InputProperties { get; set; } = Array.Empty<String>();

        /// <inheritdoc />
        public IEnumerable<String> InputArguments { get; set; } = Array.Empty<String>();

        /// <inheritdoc />
        public PaintRenderingContext2DSettings ContextOptions { get; set; } = new();

        /// <inheritdoc />
        public void Paint(IPaintRenderingContext2D context, PaintSize size, IStylePropertyMapReadOnly properties, IReadOnlyList<CssStyleValue> arguments) =>
            _paint.Invoke(context, size, properties, arguments);
    }
}
