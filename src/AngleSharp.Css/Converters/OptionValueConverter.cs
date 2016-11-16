namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;

    sealed class OptionValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public OptionValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var result = _converter.Convert(source);

            if (result == null)
            {
                result = new OptionValue();
                source.BackTo(start);
            }

            return result;
        }

        private sealed class OptionValue : BaseValue
        {
            public OptionValue()
                : base(String.Empty)
            {
            }
        }
    }

    sealed class OptionValueConverter<T> : IValueConverter
    {
        private readonly IValueConverter _converter;
        private readonly T _defaultValue;

        public OptionValueConverter(IValueConverter converter, T defaultValue)
        {
            _converter = converter;
            _defaultValue = defaultValue;
        }

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var result = _converter.Convert(source);

            if (result == null)
            {
                result = new OptionValue(_defaultValue);
                source.BackTo(start);
            }

            return result;
        }

        private sealed class OptionValue : BaseValue
        {
            private readonly T _option;

            public OptionValue(T option)
                : base(String.Empty)
            {
                _option = option;
            }
        }
    }
}
