namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;

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

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
            }
        }
    }
}
