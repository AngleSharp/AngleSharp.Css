namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

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
                return new Default<T>(_defaultValue);
            }

            return _converter.Convert(source);
        }
    }
}
