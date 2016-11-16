namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

    sealed class OneOrMoreValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;
        private readonly Int32 _minimum;
        private readonly Int32 _maximum;

        public OneOrMoreValueConverter(IValueConverter converter, Int32 minimum, Int32 maximum)
        {
            _converter = converter;
            _minimum = minimum;
            _maximum = maximum;
        }

        public ICssValue Convert(StringSource source)
        {
            var values = new List<ICssValue>();

            for (var i = 0; i < _maximum; i++)
            {
                var value = _converter.Convert(source);
                source.SkipSpaces();

                if (value == null)
                    break;

                values.Add(value);
            }

            if (values.Count >= _minimum)
            {
                return new MultipleValue(values.ToArray());
            }

            return null;
        }
        
        private sealed class MultipleValue : ICssValue
        {
            private readonly ICssValue[] _items;

            public MultipleValue(ICssValue[] items)
            {
                _items = items;
            }

            public String CssText
            {
                get { return _items.Join(" "); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
