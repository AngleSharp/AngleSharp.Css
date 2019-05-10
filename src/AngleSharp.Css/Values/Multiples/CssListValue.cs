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
    public class CssListValue<T> : ICssMultipleValue
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _items;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list value.
        /// </summary>
        /// <param name="items">The items to contain.</param>
        public CssListValue(T[] items = null)
        {
            _items = items ?? Array.Empty<T>();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue this[Int32 index] => _items[index];

        /// <inheritdoc />
        public T[] Items => _items;

        /// <inheritdoc />
        public String CssText => _items.Join(", ");

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
    /// Represents a CSS value list.
    /// </summary>
    sealed class CssListValue : CssListValue<ICssValue>
    {
        #region ctor

        public CssListValue(ICssValue[] items = null)
            : base(items)
        {
        }

        #endregion
    }
}
