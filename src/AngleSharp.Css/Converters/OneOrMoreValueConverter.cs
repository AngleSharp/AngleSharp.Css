namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class OneOrMoreValueConverter(IValueConverter converter, Int32 minimum, Int32 maximum, String separator) : IValueConverter
    {
        private readonly IValueConverter _converter = converter;
        private readonly Int32 _minimum = minimum;
        private readonly Int32 _maximum = maximum;
        private readonly String _separator = separator;

        public ICssValue? Convert(StringSource source)
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

            return values.Count >= _minimum ? new CssTupleValue([.. values], _separator) : null;
        }
    }
}
