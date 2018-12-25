namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class CounterValueConverter : IValueConverter
    {
        private readonly Int32 _defaultValue;

        public CounterValueConverter(Int32 defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public ICssValue Convert(StringSource source)
        {
            var counters = new List<ICssValue>();

            if (!source.IsIdentifier(CssKeywords.None))
            {
                while (!source.IsDone)
                {
                    var name = source.ParseIdent();
                    source.SkipSpacesAndComments();
                    var value = source.ParseInteger() ?? _defaultValue;
                    source.SkipSpacesAndComments();

                    if (name == null)
                    {
                        return null;
                    }

                    counters.Add(new CounterValue(name, value));
                }

                return new Counters(counters.ToArray());
            }

            return new Counters();
        }
    }
}
