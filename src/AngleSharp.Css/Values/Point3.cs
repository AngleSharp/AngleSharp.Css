namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS origin definition.
    /// </summary>
    public sealed class Point3 : ICssValue
    {
        private readonly Length _x;
        private readonly Length _y;
        private readonly Length _z;

        /// <summary>
        /// Creates a new Point3 (origin).
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public Point3(Length x, Length y, Length z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var pt = new Point(_x, _y).CssText;
                return _z != Length.Zero ? String.Concat(pt, " ", _z.CssText) : pt;
            }
        }
    }
}
