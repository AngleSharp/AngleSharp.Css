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
    class CssListValue : ICssMultipleValue
    {
        #region Fields

        private readonly ICssValue[] _items;

        #endregion

        #region ctor

        public CssListValue(ICssValue[] items)
        {
            _items = items;
        }

        #endregion

        #region Properties

        public ICssValue this[Int32 index] => _items[index];

        public ICssValue[] Items => _items;
        
        public String CssText => _items.Join(", ");

        public Int32 Count => _items.Length;

        #endregion

        #region Methods

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator() =>
            _items.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion
    }
}
