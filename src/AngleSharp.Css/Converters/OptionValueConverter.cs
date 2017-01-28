namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

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
            if (source.IsDone || source.Current == Symbols.Comma)
            {
                return new OptionValue(_defaultValue);
            }

            return _converter.Convert(source);
        }

        public sealed class OptionValue : ICssValue
        {
            private readonly T _option;

            public OptionValue(T option)
            {
                _option = option;
            }

            public String CssText
            {
                get { return String.Empty; }
            }

            public T Option
            {
                get { return _option; }
            }
        }
    }
}
