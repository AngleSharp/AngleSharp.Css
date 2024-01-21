namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS counter.
    /// </summary>
    /// <remarks>
    /// Specifies a counter value.
    /// </remarks>
    /// <param name="identifier">The identifier of the counter.</param>
    /// <param name="listStyle">The used list style.</param>
    /// <param name="separator">The separator of the counter.</param>
    public readonly struct CssCounterDefinitionValue(String identifier, String listStyle, String separator) : ICssPrimitiveValue, IEquatable<CssCounterDefinitionValue>
    {
        #region Fields

        private readonly String _identifier = identifier;
        private readonly String _listStyle = listStyle;
        private readonly String _separator = separator;

        #endregion
        
        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _separator == null ?
            FunctionNames.Counter.CssFunction(Combine(_identifier)) :
            FunctionNames.Counters.CssFunction(Combine(String.Concat(_identifier, " ", _separator)));

        /// <summary>
        /// Gets the identifier of the counter.
        /// </summary>
        public String CounterIdentifier => _identifier;

        /// <summary>
        /// Gets the style of the counter.
        /// </summary>
        public String ListStyle => _listStyle;

        /// <summary>
        /// Gets the defined separator of the counter.
        /// </summary>
        public String DefinedSeparator => _separator;

        #endregion

        #region Methods

        /// <summary>
        /// Checks the two counter definitions for equality.
        /// </summary>
        /// <param name="other">The other counter to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssCounterDefinitionValue other) =>
            CounterIdentifier.Is(other.CounterIdentifier) &&
            ListStyle.Is(other.ListStyle) &&
            DefinedSeparator.Is(other.DefinedSeparator);

        Boolean IEquatable<ICssValue>.Equals(ICssValue? other) => other is CssCounterDefinitionValue value && Equals(value);

        /// <summary>
        /// Checks for equality against the given object, if
        /// the provided object is no counter definition the
        /// result is false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object? obj) => obj is CssCounterDefinitionValue cd && Equals(cd);

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => CssText.GetHashCode();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Equality check.
        /// </summary>
        private String Combine(String head) => _listStyle != CssKeywords.Decimal ? String.Concat(head, ", ", _listStyle) : head;

        /// <summary>
        /// Equality check.
        /// </summary>
        public static bool operator ==(CssCounterDefinitionValue left, CssCounterDefinitionValue right) => left.Equals(right);

        /// <summary>
        /// Inequality check.
        /// </summary>
        public static bool operator !=(CssCounterDefinitionValue left, CssCounterDefinitionValue right) => !(left == right);

        #endregion
    }
}
