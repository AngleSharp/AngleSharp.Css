namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class PeriodicValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public PeriodicValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var options = new ICssValue[4];
            var length = 0;

            for (var i = 0; i < options.Length; i++)
            {
                options[i] = _converter.Convert(source);
                source.SkipSpacesAndComments();

                if (options[length] != null)
                {
                    length++;
                }
            }

            if (length > 0)
            {
                var values = new ICssValue[length];
                Array.Copy(options, values, length);
                return new PeriodicValue<ICssValue>(values);
            }

            return null;
        }
    }
}
