namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    sealed class OptionValueConverter(IValueConverter converter, ICssValue defaultValue) : IValueConverter
    {
        private readonly IValueConverter _converter = converter;
        private readonly ICssValue _defaultValue = defaultValue;

        public ICssValue? Convert(StringSource source)
        {
            if (source.IsDone || source.Current == Symbols.Comma)
            {
                return new CssInitialValue(_defaultValue);
            }

            return _converter.Convert(source);
        }
    }
}
