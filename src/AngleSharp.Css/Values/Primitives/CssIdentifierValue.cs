namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS identifier value.
    /// </summary>
    public readonly struct CssIdentifierValue : ICssPrimitiveValue, IEquatable<CssIdentifierValue>
    {
        #region Fields

        private readonly String _text;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS identifier using the text.
        /// </summary>
        /// <param name="text">The text to use as identifier.</param>
        public CssIdentifierValue(String text)
        {
            _text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the identifier.
        /// </summary>
        public String Value => _text;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _text;

        #endregion

        #region Methods

        /// <summary>
        /// Checks the two identifiers for equality.
        /// </summary>
        /// <param name="other">The other identifier to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssIdentifierValue other) => Value.Is(other.Value);

        /// <summary>
        /// Checks for equality against the given object, if
        /// the provided object is no identifier the result is
        /// false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is CssIdentifierValue ident && Equals(ident);

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssIdentifierValue value && Equals(value);

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => _text.GetHashCode();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        #endregion
    }
}
