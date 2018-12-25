namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents ordered options of CSS values.
    /// </summary>
    public sealed class OrderedOptions : ICssValue
    {
        private readonly ICssValue[] _options;

        /// <summary>
        /// Creates a new ordered options value.
        /// </summary>
        public OrderedOptions(ICssValue[] options)
        {
            _options = options;
        }

        /// <summary>
        /// Gets the contained options.
        /// </summary>
        public ICssValue[] Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return _options.Join(" "); }
        }
    }
}
