namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents an unknown (any) value.
    /// </summary>
    public sealed class CssAnyValue : ICssValue
    {
        private readonly String _text;

        /// <summary>
        /// Creates a new unknown value with the given literal content.
        /// </summary>
        /// <param name="text">The serialized value representation..</param>
        public CssAnyValue(String text)
        {
            _text = text;
        }

        /// <summary>
        /// Gets the contained value. This is the same as CssText.
        /// </summary>
        public String Value
        {
            get { return _text; }
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
