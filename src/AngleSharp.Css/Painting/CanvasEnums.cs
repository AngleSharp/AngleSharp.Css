namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The rule deciding if a point is inside a path.
    /// </summary>
    [DomName("CanvasFillRule")]
    public enum CanvasFillRule : byte
    {
        /// <summary>
        /// The non-zero winding rule.
        /// </summary>
        [DomName("nonzero")]
        NonZero,
        /// <summary>
        /// The even-odd rule.
        /// </summary>
        [DomName("evenodd")]
        EvenOdd,
    }

    /// <summary>
    /// The shape of the ends of lines.
    /// </summary>
    [DomName("CanvasLineCap")]
    public enum CanvasLineCap : byte
    {
        /// <summary>
        /// The line ends at its end point.
        /// </summary>
        [DomName("butt")]
        Butt,
        /// <summary>
        /// A half circle is added to the end.
        /// </summary>
        [DomName("round")]
        Round,
        /// <summary>
        /// A half square is added to the end.
        /// </summary>
        [DomName("square")]
        Square,
    }

    /// <summary>
    /// The shape of the corners where lines meet.
    /// </summary>
    [DomName("CanvasLineJoin")]
    public enum CanvasLineJoin : byte
    {
        /// <summary>
        /// The corner is rounded.
        /// </summary>
        [DomName("round")]
        Round,
        /// <summary>
        /// The corner is cut off.
        /// </summary>
        [DomName("bevel")]
        Bevel,
        /// <summary>
        /// The corner is extended to a point (up to the miter limit).
        /// </summary>
        [DomName("miter")]
        Miter,
    }

    /// <summary>
    /// The quality of smoothing scaled images.
    /// </summary>
    [DomName("ImageSmoothingQuality")]
    public enum ImageSmoothingQuality : byte
    {
        /// <summary>
        /// Low quality.
        /// </summary>
        [DomName("low")]
        Low,
        /// <summary>
        /// Medium quality.
        /// </summary>
        [DomName("medium")]
        Medium,
        /// <summary>
        /// High quality.
        /// </summary>
        [DomName("high")]
        High,
    }
}
