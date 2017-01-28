namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class OrderedOptionsConverter: IValueConverter
    {
        private readonly IValueConverter[] _converters;

        public OrderedOptionsConverter(params IValueConverter[] converters)
        {
            _converters = converters;
        }

        public ICssValue Convert(StringSource source)
        {
            var options = new ICssValue[_converters.Length];

            for (var i = 0; i < _converters.Length; i++)
            {
                var option = _converters[i].Convert(source);

                if (option == null)
                    break;

                source.SkipSpacesAndComments();
                options[i] = option;
            }

            return new OptionsValue(options);
        }

        private sealed class OptionsValue : ICssValue
        {
            private readonly ICssValue[] _options;

            public OptionsValue(ICssValue[] options)
            {
                _options = options;
            }

            public String CssText
            {
                get { return _options.Join(" "); }
            }
        }
    }
}
