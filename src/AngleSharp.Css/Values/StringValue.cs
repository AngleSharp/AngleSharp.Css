namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS string value.
    /// </summary>
    public sealed class StringValue : ICssValue
    {
        private readonly String _value;

        /// <summary>
        /// Creates a new CSS string value.
        /// </summary>
        /// <param name="value">The string to represent.</param>
        public StringValue(String value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return _value.CssString(); }
        }
    }
}
