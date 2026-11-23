namespace AngleSharp.Css
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The source picked for an image element (see "select an image source" in HTML): the
    /// URL of its image and the density of its pixels, by which the natural size of the
    /// image is divided (e.g. an image of 800 pixels picked as 2x is 400 CSS pixels wide).
    /// </summary>
    public sealed class ImageSourceSelection
    {
        /// <summary>
        /// Creates a new selection.
        /// </summary>
        /// <param name="url">The URL of the image.</param>
        /// <param name="density">The density of its pixels (image pixels per CSS pixel).</param>
        public ImageSourceSelection(Url url, Double density)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Density = density > 0 && !Double.IsInfinity(density) ? density : 1.0;
        }

        /// <summary>
        /// Gets the URL of the image.
        /// </summary>
        public Url Url { get; }

        /// <summary>
        /// Gets the density of the pixels of the image: 1 for a plain source or an `1x`
        /// candidate, 2 for `2x`, and the width of a `w` candidate per CSS pixel of the
        /// size the image is shown at (its sizes).
        /// </summary>
        public Double Density { get; }
    }
}
