namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class OutlineValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            LineWidthConverter.Option().For(PropertyNames.OutlineWidth),
            LineStyleConverter.Option().For(PropertyNames.OutlineStyle),
            InvertedColorConverter.Option().For(PropertyNames.OutlineColor));

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
