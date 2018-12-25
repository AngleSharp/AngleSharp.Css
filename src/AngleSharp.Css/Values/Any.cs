namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents an unknown (any) value.
    /// </summary>
    public sealed class Any : ICssValue
    {
        private readonly String _text;

        /// <summary>
        /// Creates a new unknown value with the given literal content.
        /// </summary>
        /// <param name="text">The serialized value representation..</param>
        public Any(String text)
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
