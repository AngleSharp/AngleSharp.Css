namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS grid definition.
    /// </summary>
    public sealed class Grid : ICssValue
    {
        /// <summary>
        /// Creates a new CSS grid.
        /// </summary>
        public Grid()
        {

        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                return String.Empty;
            }
        }
    }
}
