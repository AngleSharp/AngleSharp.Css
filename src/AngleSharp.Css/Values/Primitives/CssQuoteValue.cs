namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS quote.
    /// </summary>
    public readonly struct CssQuoteValue : ICssPrimitiveValue, IEquatable<CssQuoteValue>
    {
        #region Fields

        private readonly String _open;
        private readonly String _close;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS quote.
        /// </summary>
        /// <param name="open">The open quote character(s).</param>
        /// <param name="close">The close quote character(s).</param>
        public CssQuoteValue(String open, String close)
        {
            _open = open;
            _close = close;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the open quote character(s).
        /// </summary>
        public String Open => _open;

        /// <summary>
        /// Gets the close quote character(s).
        /// </summary>
        public String Close => _close;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Concat(_open.CssString(), " ", _close.CssString());

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            return this;
        }

        /// <summary>
        /// Checks the two quotes for equality.
        /// </summary>
        /// <param name="other">The other quote to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssQuoteValue other) => Open.Is(other.Open) && Close.Is(other.Close);

        /// <summary>
        /// Checks for equality against the given object,
        /// if the provided object is no quote the result
        /// is false.
        /// </summary>
        /// <param name="obj">The object to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj) =>
            obj is CssQuoteValue quote && Equals(quote);
        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssQuoteValue value && Equals(value);

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode() => CssText.GetHashCode();

        #endregion
    }
}
