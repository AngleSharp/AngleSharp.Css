namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS unset value.
    /// </summary>
    struct CssUnsetValue : ICssSpecialValue
    {
        #region Fields

        private readonly ICssValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unset value using the given value.
        /// </summary>
        /// <param name="value">The value to unset to.</param>
        public CssUnsetValue(ICssValue value)
        {
            _value = value;
        }

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
    }
}
