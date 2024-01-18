namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a border radius value.
    /// </summary>
    sealed class CssBorderRadiusValue : ICssCompositeValue, IEquatable<CssBorderRadiusValue>
    {
        #region Fields

        private readonly CssPeriodicValue _horizontal;
        private readonly CssPeriodicValue _vertical;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new border radius value.
        /// </summary>
        public CssBorderRadiusValue(CssPeriodicValue horizontal, CssPeriodicValue vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal values.
        /// </summary>
        public CssPeriodicValue Horizontal => _horizontal;

        /// <summary>
        /// Gets the vertical values.
        /// </summary>
        public CssPeriodicValue Vertical => _vertical;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                var h = _horizontal.CssText;

                if (!Object.ReferenceEquals(_horizontal, _vertical))
                {
                    var v = _vertical.CssText;

                    if (!String.IsNullOrEmpty(v) && h != v)
                    {
                        return String.Concat(h, " / ", v);
                    }
                }

                return h;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssBorderRadiusValue other)
        {
            return _horizontal.Equals(other._horizontal) && _vertical.Equals(other._vertical);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssBorderRadiusValue value && Equals(value);

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var h = ((ICssValue)_horizontal).Compute(context);
            var v = ((ICssValue)_vertical).Compute(context);
            return new CssBorderRadiusValue((CssPeriodicValue)h, (CssPeriodicValue)v);
        }

        #endregion
    }
}
