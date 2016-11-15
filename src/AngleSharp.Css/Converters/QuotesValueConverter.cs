namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class QuotesValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var index = source.Index;
            var quotes = new List<Quote>();

            while (!source.IsDone)
            {
                var open = source.ParseString();
                var close = source.ParseString();

                if (open == null || close == null)
                {
                    return null;
                }

                quotes.Add(new Quote { Open = open, Close = close });
            }

            var value = source.Substring(index);
            return new StringsValue(value, quotes.ToArray());
        }

        private sealed class StringsValue : BaseValue
        {
            private readonly Quote[] _quotes;

            public StringsValue(String value, Quote[] quotes)
                : base(value)
            {
                _quotes = quotes;
            }
        }

        struct Quote
        {
            public String Open;
            public String Close;
        }
    }
}
