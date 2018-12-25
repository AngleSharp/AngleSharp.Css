namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS value list.
    /// </summary>
    public sealed class Multiple : ICssValue
    {
        private readonly ICssValue[] _items;

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="items">The items in the list.</param>
        public Multiple(ICssValue[] items)
        {
            _items = items;
        }

        /// <summary>
        /// Gets the contained values.
        /// </summary>
        public ICssValue[] Values
        {
            get { return _items; }
        }

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return _items.Join(", "); }
        }
    }
}
