namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
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
            return _converter.Convert(source) ?? new OptionValue();
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
            return _converter.Convert(source) ?? new OptionValue(_defaultValue);
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
