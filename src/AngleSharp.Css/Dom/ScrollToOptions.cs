namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Container for the DOM ScrollToOptions interface.
    /// </summary>
    [DomName("ScrollToOptions")]
    public class ScrollToOptions : ScrollOptions
    {
        /// <summary>
        /// Gets the horizontal position in pixels.
        /// </summary>
        [DomName("left")]
        public Double Left { get; set; }

        /// <summary>
        /// Gets the vertical position in pixels.
        /// </summary>
        [DomName("top")]
        public Double Top { get; set; }
    }
}
