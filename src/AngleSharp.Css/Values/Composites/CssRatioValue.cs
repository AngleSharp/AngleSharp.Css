namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a ratio (top to bottom) value.
    /// </summary>
    public readonly struct CssRatioValue : ICssCompositeValue, IEquatable<CssRatioValue>
    {
        #region Fields

        private readonly ICssValue _top;
        private readonly ICssValue _bottom;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new ratio value.
        /// </summary>
        /// <param name="top">The first number.</param>
        /// <param name="bottom">The second number.</param>
        public CssRatioValue(ICssValue top, ICssValue bottom)
        {
            _top = top;
            _bottom = bottom;
        }

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
            var comparer = EqualityComparer<ICssValue>.Default;
            return comparer.Equals(_top, other._top) && comparer.Equals(_bottom, other._bottom);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var top = _top.Compute(context);
            var bottom = _bottom.Compute(context);

            if (top != _top || bottom != _bottom)
            {
                return new CssRatioValue(top, bottom);
            }

            return this;
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssRatioValue value && Equals(value);

        #endregion
    }
}
