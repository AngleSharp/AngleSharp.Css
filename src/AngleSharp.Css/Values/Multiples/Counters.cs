namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the CSS counters definition.
    /// </summary>
    class Counters : ICssMultipleValue
    {
        #region Fields

        private readonly ICssValue[] _counters;

        #endregion

        #region ctor
        
        public Counters()
        {
            _counters = null;
        }
        
        public Counters(ICssValue[] counters)
        {
            _counters = counters;
        }

        public ICssValue this[Int32 index] => _counters[index];

        #endregion

        #region Properties
        
        public ICssValue[] Values => _counters;
        
        public String CssText => _counters != null ? _counters.Join(" ") : CssKeywords.None;

        public Int32 Count => _counters.Length;

        #endregion

        #region Methods

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator() =>
            _counters.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _counters.GetEnumerator();

        #endregion
    }
}
