namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS origin definition.
    /// </summary>
    class Point3 : ICssValue
    {
        private readonly ICssValue _x;
        private readonly ICssValue _y;
        private readonly ICssValue _z;

        /// <summary>
        /// Creates a new Point3 (origin).
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public Point3(ICssValue x, ICssValue y, ICssValue z)
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
                return _z != null ? String.Concat(pt, " ", _z.CssText) : pt;
            }
        }

        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        public ICssValue X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        public ICssValue Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the z coordinate.
        /// </summary>
        public ICssValue Z
        {
            get { return _z; }
        }
    }
}
