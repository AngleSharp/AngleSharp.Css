namespace AngleSharp.Css.Painting
{
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// A drawn paint image: the rendering context the paint definition drew with, which
    /// holds the output, e.g., the drawing operations of a
    /// <see cref="RecordingPaintRenderingContext2D"/> or the bitmap of a context created
    /// by a custom <see cref="IPaintRenderingContextFactory"/>.
    /// </summary>
    public sealed class PaintImage
    {
        internal PaintImage(CssPaintValue source, PaintSize size, Double devicePixelRatio, IPaintRenderingContext2D context)
        {
            Source = source;
            Size = size;
            DevicePixelRatio = devicePixelRatio;
            Context = context;
        }

        /// <summary>
        /// Gets the paint() function the image was drawn for.
        /// </summary>
        public CssPaintValue Source { get; }

        /// <summary>
        /// Gets the name of the paint definition that drew the image.
        /// </summary>
        public String Name => Source.PainterName;

        /// <summary>
        /// Gets the size of the image in CSS pixels.
        /// </summary>
        public PaintSize Size { get; }

        /// <summary>
        /// Gets the number of device pixels per CSS pixel the image was drawn for.
        /// </summary>
        public Double DevicePixelRatio { get; }

        /// <summary>
        /// Gets the rendering context that has been drawn with, i.e., the output.
        /// </summary>
        public IPaintRenderingContext2D Context { get; }
    }
}
