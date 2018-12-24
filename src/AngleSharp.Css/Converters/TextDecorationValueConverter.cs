namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class TextDecorationValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            ColorConverter.Option(),
            TextDecorationStyleConverter.Option(),
            TextDecorationLinesConverter.Option());

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
