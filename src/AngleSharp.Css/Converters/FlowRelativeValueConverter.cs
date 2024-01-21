namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class FlowRelativeValueConverter(IValueConverter converter) : IValueConverter
    {
        private readonly IValueConverter _converter = converter;

        public ICssValue? Convert(StringSource source)
        {
            var options = new ICssValue[2];
            var length = 0;

            for (var i = 0; i < options.Length; i++)
            {
                var option = _converter.Convert(source);
                source.SkipSpacesAndComments();

                if (option is not null)
                {
                    options[length++] = option;
                }
            }

            if (length > 0)
            {
                var values = new ICssValue[length];
                Array.Copy(options, values, length);
                return new CssFlowRelativeValue(values);
            }

            return null;
        }
    }
}
