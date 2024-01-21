namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS initial value.
    /// </summary>
    /// <remarks>
    /// Creates a new initial value using the provided data.
    /// </remarks>
    /// <param name="value">The initial data.</param>
    readonly struct CssInitialValue(ICssValue? value) : ICssSpecialValue, IEquatable<CssInitialValue>
    {
        #region Fields

        private readonly ICssValue? _value = value;

        #endregion

        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the initial value.
        /// </summary>
        public ICssValue? Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Initial;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssInitialValue other)
        {
            return _value == other._value || _value is not null && other._value is not null && _value.Equals(other._value);
        }

        ICssValue ICssValue.Compute(ICssComputeContext context) => this;

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssInitialValue o && Equals(o);

        #endregion
    }
}
