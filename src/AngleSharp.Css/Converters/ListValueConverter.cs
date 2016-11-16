namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

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
            
            return new ListValue(values.ToArray());
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
