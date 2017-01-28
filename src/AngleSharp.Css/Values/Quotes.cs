namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Quotes : IEnumerable<Quote>, ICssValue
    {
        #region Fields

        private readonly Quote[] _quotes;

        #endregion

        #region ctor

        public Quotes(IEnumerable<Quote> quotes)
        {
            _quotes = quotes.ToArray();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        #endregion

        #region Methods

        public IEnumerator<Quote> GetEnumerator()
        {
            foreach (var quote in _quotes)
            {
                yield return quote;
            }
        }

        public override String ToString()
        {
            return String.Join(" ", _quotes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
