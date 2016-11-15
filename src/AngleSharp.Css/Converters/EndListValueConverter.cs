namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class EndListValueConverter : IValueConverter
    {
        private readonly IValueConverter _listConverter;
        private readonly IValueConverter _endConverter;

        public EndListValueConverter(IValueConverter listConverter, IValueConverter endConverter)
        {
            _listConverter = listConverter;
            _endConverter = endConverter;
        }

        public ICssValue Convert(StringSource source)
        {
            var values = new List<ICssValue>();
            var endValue = default(ICssValue);
            var start = source.Index;

            while (!source.IsDone)
            {
                var index = source.Index;
                var value = _listConverter.Convert(source);
                source.SkipSpaces();

                if (value == null || source.IsDone)
                {
                    source.BackTo(index);
                    endValue = _endConverter.Convert(source);
                    source.SkipSpaces();
                    break;
                }
                
                if (source.SkipSpaces() != Symbols.Comma)
                {
                    return null;
                }

                source.SkipCurrentAndSpaces();
                values.Add(value);
            }

            if (endValue != null)
            {
                return new ListValue(source.Substring(start), values.ToArray(), endValue);
            }

            return null;
        }

        private sealed class ListValue : BaseValue
        {
            private readonly ICssValue[] _items;
            private readonly ICssValue _last;

            public ListValue(String value, ICssValue[] items, ICssValue last)
                : base(value)
            {
                _items = items;
                _last = last;
            }
        }
    }
}
