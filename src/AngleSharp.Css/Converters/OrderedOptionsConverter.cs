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
            var start = source.Index;
            var options = new ICssValue[_converters.Length];

            for (var i = 0; i < _converters.Length; i++)
            {
                var option = _converters[i].Convert(source);

                if (option == null)
                {
                    return null;
                }

                source.SkipSpaces();
                options[i] = option;
            }

            return new OptionsValue(source.Substring(start), options);
        }

        private sealed class OptionsValue : BaseValue
        {
            private readonly ICssValue[] _options;

            public OptionsValue(String value, ICssValue[] options)
                : base(value)
            {
                _options = options;
            }
        }
    }
}
