namespace AngleSharp.Css.Values
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Quotes : IEnumerable<Quote>
    {
        private readonly Quote[] _quotes;

        public Quotes(IEnumerable<Quote> quotes)
        {
            _quotes = quotes.ToArray();
        }

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
    }
}
