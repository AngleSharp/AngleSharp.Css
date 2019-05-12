namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the CSS initial value.
    /// </summary>
    struct CssInitialValue : ICssSpecialValue
    {
        #region Fields

        private readonly ICssValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new initial value using the provided data.
        /// </summary>
        /// <param name="value">The initial data.</param>
        public CssInitialValue(ICssValue value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the initial value.
        /// </summary>
        public ICssValue Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => CssKeywords.Initial;

        #endregion
    }
}
