namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a keyword, e.g., auto, block, or inherit.
    /// https://www.w3.org/TR/css-typed-om-1/#csskeywordvalue
    /// </summary>
    [DomName("CSSKeywordValue")]
    public sealed class CssKeywordValue : CssStyleValue
    {
        private String _value;

        /// <summary>
        /// Creates a new keyword value.
        /// </summary>
        /// <param name="value">The keyword, e.g., auto.</param>
        /// <exception cref="ArgumentException">The keyword is empty.</exception>
        [DomConstructor]
        public CssKeywordValue(String value)
        {
            _value = Validate(value);
        }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <exception cref="ArgumentException">The keyword is empty.</exception>
        [DomName("value")]
        public String Value
        {
            get => _value;
            set => _value = Validate(value);
        }

        /// <summary>
        /// Serializes the keyword as an identifier.
        /// </summary>
        public override String ToString() => CssHelpers.Escape(_value);

        private static String Validate(String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("A keyword must not be empty.", nameof(value));
            }

            return value;
        }
    }
}
