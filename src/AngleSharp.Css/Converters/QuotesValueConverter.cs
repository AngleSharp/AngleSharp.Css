namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

    sealed class QuotesValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
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

            return new StringsValue(quotes.ToArray());
        }

        private sealed class StringsValue : ICssValue
        {
            private readonly Quote[] _quotes;

            public StringsValue(Quote[] quotes)
            {
                _quotes = quotes;
            }

            public String CssText
            {
                get { return String.Join(" ", _quotes); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }

        struct Quote
        {
            public String Open;
            public String Close;

            public override String ToString()
            {
                return String.Concat(Open.CssString(), " ", Close.CssString());
            }
        }
    }
}
