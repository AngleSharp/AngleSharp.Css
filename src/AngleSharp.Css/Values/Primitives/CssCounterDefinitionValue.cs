namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS counter.
    /// </summary>
    public readonly struct CssCounterDefinitionValue : ICssPrimitiveValue, IEquatable<CssCounterDefinitionValue>
    {
        #region Fields

        private readonly String _identifier;
        private readonly String _listStyle;
        private readonly String _separator;

        #endregion

        #region ctor

        /// <summary>
        /// Specifies a counter value.
        /// </summary>
        /// <param name="identifier">The identifier of the counter.</param>
        /// <param name="listStyle">The used list style.</param>
        /// <param name="separator">The separator of the counter.</param>
        public CssCounterDefinitionValue(String identifier, String listStyle, String separator)
        {
            _identifier = identifier;
            _listStyle = listStyle;
            _separator = separator;
        }

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

        /// <summary>
        /// Checks for equality against the given object, if
        /// the provided object is no counter definition the
        /// result is false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is CssCounterDefinitionValue cd && Equals(cd);

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

        private String Combine(String head) => _listStyle != CssKeywords.Decimal ? String.Concat(head, ", ", _listStyle) : head;

        #endregion
    }
}
