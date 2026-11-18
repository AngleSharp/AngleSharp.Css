namespace AngleSharp.Css.Painting
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The options of the rendering context a paint definition draws with.
    /// https://www.w3.org/TR/css-paint-api-1/#dictdef-paintrenderingcontext2dsettings
    /// </summary>
    [DomName("PaintRenderingContext2DSettings")]
    public sealed class PaintRenderingContext2DSettings
    {
        /// <summary>
        /// Gets or sets if the image has an alpha channel. Without one,
        /// the image is opaque. True by default.
        /// </summary>
        [DomName("alpha")]
        public Boolean Alpha { get; set; } = true;
    }
}
