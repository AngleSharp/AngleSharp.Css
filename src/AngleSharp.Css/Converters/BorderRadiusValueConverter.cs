namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class BorderRadiusValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter = LengthOrPercentConverter.Periodic();

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var horizontal = _converter.Convert(source) as Periodic<ICssValue>;
            var vertical = horizontal;

            if (source.Current == Symbols.Solidus)
            {
                source.SkipCurrentAndSpaces();
                vertical = _converter.Convert(source) as Periodic<ICssValue>;
            }

            return vertical != null ? new BorderRadius(horizontal, vertical) : null;
        }
    }
}
