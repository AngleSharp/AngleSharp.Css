namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS identifier value.
    /// </summary>
    public struct Identifier : ICssValue
    {
        private readonly String _text;

        /// <summary>
        /// Creates a new CSS identifier using the text.
        /// </summary>
        /// <param name="text">The text to use as identifier.</param>
        public Identifier(String text)
        {
            _text = text;
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return _text; }
        }
    }
}
