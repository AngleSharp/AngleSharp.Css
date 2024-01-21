namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a ratio (top to bottom) value.
    /// </summary>
    /// <remarks>
    /// Creates a new ratio value.
    /// </remarks>
    /// <param name="top">The first number.</param>
    /// <param name="bottom">The second number.</param>
    public readonly struct CssRatioValue(ICssValue top, ICssValue bottom) : ICssCompositeValue, IEquatable<CssRatioValue>
    {
        #region Fields

        private readonly ICssValue _top = top;
        private readonly ICssValue _bottom = bottom;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_top.CssText, "/", _bottom.CssText);

        /// <summary>
        /// Gets the first number of the ratio.
        /// </summary>
        public ICssValue Top => _top;

        /// <summary>
        /// Gets the second number of the ratio.
        /// </summary>
        public ICssValue Bottom => _bottom;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssRatioValue other)
        {
            return _top.Is(other._top) && _bottom.Is(other._bottom);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssRatioValue value && Equals(value);

        ICssValue? ICssValue.Compute(ICssComputeContext context)
        {
            var top = _top.Compute(context);
            var bottom = _bottom.Compute(context);

            if (top is null || bottom is null)
            {
                return null;
            }

            if (top != _top || bottom != _bottom)
            {
                return new CssRatioValue(top, bottom);
            }

            return this;
        }

        #endregion
    }
}
