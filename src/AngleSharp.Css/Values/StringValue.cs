namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS string value.
    /// </summary>
    class StringValue : ICssValue
    {
        #region Fields

        private readonly String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS string value.
        /// </summary>
        /// <param name="value">The string to represent.</param>
        public StringValue(String value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the string value.
        /// </summary>
        public String Value => _value;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _value.CssString();

        #endregion
    }
}
