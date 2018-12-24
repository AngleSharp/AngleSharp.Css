namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class ColumnsValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            AutoLengthConverter,
            OptionalIntegerConverter);

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
