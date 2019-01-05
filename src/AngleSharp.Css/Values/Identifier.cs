namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS identifier value.
    /// </summary>
    struct Identifier : ICssValue
    {
        #region Fields

        private readonly String _text;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS identifier using the text.
        /// </summary>
        /// <param name="text">The text to use as identifier.</param>
        public Identifier(String text)
        {
            _text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _text;

        #endregion
    }
}
