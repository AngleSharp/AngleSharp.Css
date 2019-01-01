namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class OneOrMoreValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;
        private readonly Int32 _minimum;
        private readonly Int32 _maximum;
        private readonly String _separator;

        public OneOrMoreValueConverter(IValueConverter converter, Int32 minimum, Int32 maximum, String separator)
        {
            _converter = converter;
            _minimum = minimum;
            _maximum = maximum;
            _separator = separator;
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

            return values.Count >= _minimum ? new CssTupleValue(values.ToArray(), _separator) : null;
        }
    }
}
