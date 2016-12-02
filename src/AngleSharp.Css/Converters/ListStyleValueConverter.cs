namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class ListStyleValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            ListStyleConverter.Option().For(PropertyNames.ListStyleType),
            ListPositionConverter.Option().For(PropertyNames.ListStylePosition),
            OptionalImageSourceConverter.Option().For(PropertyNames.ListStyleImage));

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
