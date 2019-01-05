namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS value list.
    /// </summary>
    class CssListValue : ICssValue, IEnumerable<ICssValue>
    {
        private readonly ICssValue[] _items;

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="items">The items in the list.</param>
        public CssListValue(ICssValue[] items)
        {
            _items = items;
        }

        /// <summary>
        /// Gets the contained values.
        /// </summary>
        public ICssValue[] Items
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

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
