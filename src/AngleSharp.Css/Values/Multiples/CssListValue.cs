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
    class CssListValue<T> : ICssMultipleValue
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _items;

        #endregion

        #region ctor

        public CssListValue(T[] items)
        {
            _items = items;
        }

        #endregion

        #region Properties

        public ICssValue this[Int32 index] => _items[index];

        public T[] Items => _items;

        public String CssText => _items.Join(", ");

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
    class CssListValue : CssListValue<ICssValue>
    {
        #region ctor

        public CssListValue(ICssValue[] items)
            : base(items)
        {
        }

        #endregion
    }
}
