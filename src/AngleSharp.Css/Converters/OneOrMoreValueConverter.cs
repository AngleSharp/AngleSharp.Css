namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

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

            for (var i = 0; i < _maximum && !source.IsDone; i++)
            {
                var value = _converter.Convert(source);
                source.SkipSpacesAndComments();

                if (value == null)
                    break;

                values.Add(value);
            }

            return values.Count >= _minimum ? new MultipleValue(values.ToArray()) : null;
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
        }
    }
}
