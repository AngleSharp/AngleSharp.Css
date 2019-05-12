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
    public class CssTupleValue<T> : ICssMultipleValue
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _items;
        private readonly String _separator;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS tuple value.
        /// </summary>
        /// <param name="items">The items to contain.</param>
        /// <param name="separator">The custom separator, if any.</param>
        public CssTupleValue(T[] items = null, String separator = null)
        {
            _items = items ?? Array.Empty<T>();
            _separator = separator ?? " ";
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue this[Int32 index] => _items[index];

        /// <inheritdoc />
        public T[] Items => _items;

        /// <inheritdoc />
        public String Separator => _separator;

        /// <inheritdoc />
        public String CssText => _items
            .Where(m => m is CssInitialValue == false)
            .Join(_separator);

        /// <inheritdoc />
        public Int32 Count => _items.Length;

        #endregion

        #region Methods

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator() =>
            _items.OfType<ICssValue>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion
    }

    /// <summary>
    /// Represents a tuple of CSS values.
    /// </summary>
    sealed class CssTupleValue : CssTupleValue<ICssValue>
    {
        #region ctor
        
        public CssTupleValue(ICssValue[] items = null, String separator = null)
            : base(items, separator)
        {
        }

        #endregion
    }
}
