namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a default value.
    /// </summary>
    sealed class CssDefaultValue : ICssValue
    {
        #region Fields

        private readonly ICssValue _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new default value.
        /// </summary>
        /// <param name="value">The used value.</param>
        public CssDefaultValue(ICssValue value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => String.Empty;

        /// <summary>
        /// Gets the default value.
        /// </summary>
        public ICssValue Value => _value;

        #endregion
    }
}
