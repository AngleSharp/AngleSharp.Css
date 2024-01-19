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
        private static readonly CssCounterValue[] NoneValue = Array.Empty<CssCounterValue>();

        private readonly ICssValue _defaultValue;

        public CounterValueConverter(ICssValue defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public ICssValue Convert(StringSource source)
        {
            var counters = new List<CssCounterValue>();

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

                    counters.Add(new CssCounterValue(name, value));
                }

                return new CssTupleValue<CssCounterValue>(counters.ToArray());
            }

            return new CssConstantValue<CssCounterValue[]>(CssKeywords.None, NoneValue);
        }
    }
}
