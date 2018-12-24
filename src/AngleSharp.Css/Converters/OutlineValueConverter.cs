namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class OutlineValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            LineWidthConverter.Option(),
            LineStyleConverter.Option(),
            InvertedColorConverter.Option());

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
