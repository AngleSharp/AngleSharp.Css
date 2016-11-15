namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class ListValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public ListValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var values = new List<ICssValue>();
            var start = source.Index;

            while (!source.IsDone)
            {
                var index = source.Index;
                var value = _converter.Convert(source);
                source.SkipSpaces();

                if (value == null || (!source.IsDone && source.Current != Symbols.Comma))
                {
                    return null;
                }

                source.SkipCurrentAndSpaces();
                values.Add(value);
            }

            return new ListValue(source.Substring(start), values.ToArray());
        }

        private sealed class ListValue : BaseValue
        {
            private readonly ICssValue[] _items;

            public ListValue(String value, ICssValue[] items)
                : base(value)
            {
                _items = items;
            }
        }
    }
}
