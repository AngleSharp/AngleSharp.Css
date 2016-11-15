namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;

    sealed class RequiredValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public RequiredValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source)
        {
            return !source.IsDone ? _converter.Convert(source) : null;
        }
    }
}
