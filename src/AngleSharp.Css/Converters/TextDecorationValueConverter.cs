namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class TextDecorationValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            ColorConverter.Option().For(PropertyNames.TextDecorationColor),
            TextDecorationStyleConverter.Option().For(PropertyNames.TextDecorationStyle),
            TextDecorationLinesConverter.Option().For(PropertyNames.TextDecorationLine));

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
