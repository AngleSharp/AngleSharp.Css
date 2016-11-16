namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

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
            var last = false;
            var values = new List<ICssValue>();

            while (!source.IsDone)
            {
                var index = source.Index;
                var value = _listConverter.Convert(source);
                source.SkipSpaces();

                if (value == null || source.IsDone)
                {
                    source.BackTo(index);
                    value = _endConverter.Convert(source);
                    source.SkipSpaces();

                    if (value != null)
                    {
                        values.Add(value);
                        last = true;
                    }

                    break;
                }
                
                if (source.SkipSpaces() != Symbols.Comma)
                {
                    return null;
                }

                source.SkipCurrentAndSpaces();
                values.Add(value);
            }

            if (last)
            {
                return new ListValue(values.ToArray());
            }

            return null;
        }

        private sealed class ListValue : ICssValue
        {
            private readonly ICssValue[] _items;

            public ListValue(ICssValue[] items)
            {
                _items = items;
            }

            public String CssText
            {
                get { return _items.Join(", "); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
