namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a tuple of CSS values.
    /// </summary>
    class CssTupleValue : ICssValue, IEnumerable<ICssValue>
    {
        #region Fields

        private readonly ICssValue[] _items;
        private readonly String _separator;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new tuple value.
        /// </summary>
        /// <param name="items">The items to contain.</param>
        /// <param name="separator">The optional connection string.</param>
        public CssTupleValue(ICssValue[] items, String separator = null)
        {
            _items = items;
            _separator = separator ?? " ";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained items.
        /// </summary>
        public ICssValue[] Items => _items;

        /// <summary>
        /// Gets the used separator.
        /// </summary>
        public String Separator => _separator;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText => _items.Join(_separator);

        #endregion

        #region Methods

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator()
        {
            return _items.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion
    }
}
