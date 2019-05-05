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
    class CssTupleValue : ICssMultipleValue
    {
        #region Fields

        private readonly ICssValue[] _items;
        private readonly String _separator;

        #endregion

        #region ctor
        
        public CssTupleValue(ICssValue[] items, String separator = null)
        {
            _items = items;
            _separator = separator ?? " ";
        }

        #endregion

        #region Properties

        public ICssValue this[Int32 index] => _items[index];

        public ICssValue[] Items => _items;

        public String Separator => _separator;

        public String CssText => _items.Join(_separator);

        public Int32 Count => _items.Length;

        #endregion

        #region Methods

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator() =>
            _items.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion
    }
}
