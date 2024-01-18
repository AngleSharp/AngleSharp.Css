namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS origin definition.
    /// </summary>
    public sealed class CssOriginValue : ICssCompositeValue, IEquatable<CssOriginValue>
    {
        #region Fields

        private readonly ICssValue _x;
        private readonly ICssValue _y;
        private readonly ICssValue _z;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Point3 (origin).
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public CssOriginValue(ICssValue x, ICssValue y, ICssValue z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var pt = new CssPoint2D(_x, _y).CssText;
                return _z != null ? String.Concat(pt, " ", _z.CssText) : pt;
            }
        }

        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        public ICssValue X => _x;

        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        public ICssValue Y => _y;

        /// <summary>
        /// Gets the z coordinate.
        /// </summary>
        public ICssValue Z => _z;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssOriginValue other)
        {
            return _x.Equals(other._x) && _y.Equals(other._y) && _z.Equals(other._z);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var x = _x.Compute(context);
            var y = _y.Compute(context);
            var z = _z.Compute(context);

            if (x != _x || y != _y || z != _z)
            {
                return new CssOriginValue(x, y, z);
            }

            return this;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssOriginValue value && Equals(value);

        #endregion
    }
}
