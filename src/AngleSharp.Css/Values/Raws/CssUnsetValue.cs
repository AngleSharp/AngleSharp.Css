namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS unset value.
    /// </summary>
    /// <remarks>
    /// Creates a new unset value using the given value.
    /// </remarks>
    /// <param name="value">The value to unset to.</param>
    readonly struct CssUnsetValue(ICssValue value) : ICssSpecialValue, IEquatable<CssUnsetValue>
    {
        #region Fields

        private readonly ICssValue _value = value;

        #endregion

        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unset value.
        /// </summary>
        public ICssValue Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Unset;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssUnsetValue other)
        {
            return _value == other._value || _value is not null && _value.Equals(other._value);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssUnsetValue o && Equals(o);

        #endregion
    }
}
