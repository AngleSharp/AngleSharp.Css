namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class ListStyleValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            ListStyleConverter.Option(),
            ListPositionConverter.Option(),
            OptionalImageSourceConverter.Option());

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
