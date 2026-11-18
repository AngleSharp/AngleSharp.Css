namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The size of the paint image to draw (the "paint size" of the specification) in
    /// CSS pixels, e.g., the size of the background positioning area of a background.
    /// AngleSharp.Css does no layout, so the size is given by whoever draws the image.
    /// https://www.w3.org/TR/css-paint-api-1/#paintsize
    /// </summary>
    [DomName("PaintSize")]
    public sealed class PaintSize
    {
        /// <summary>
        /// Creates a new paint size.
        /// </summary>
        /// <param name="width">The width in CSS pixels.</param>
        /// <param name="height">The height in CSS pixels.</param>
        public PaintSize(Double width, Double height)
        {
            if (Double.IsNaN(width) || Double.IsInfinity(width) || width < 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            if (Double.IsNaN(height) || Double.IsInfinity(height) || height < 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets the width in CSS pixels.
        /// </summary>
        [DomName("width")]
        public Double Width { get; }

        /// <summary>
        /// Gets the height in CSS pixels.
        /// </summary>
        [DomName("height")]
        public Double Height { get; }
    }
}
