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
            var start = source.Index;
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
                var value = source.Substring(start);
                return new MultipleValue(value, values.ToArray());
            }

            return null;
        }
        
        private sealed class MultipleValue : BaseValue
        {
            private readonly ICssValue[] _items;

            public MultipleValue(String value, ICssValue[] items)
                : base(value)
            {
                _items = items;
            }
        }
    }
}
