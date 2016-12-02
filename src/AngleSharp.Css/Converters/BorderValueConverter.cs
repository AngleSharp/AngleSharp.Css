namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class BorderValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            LineWidthConverter.Option().For(PropertyNames.BorderTopWidth, PropertyNames.BorderRightWidth, PropertyNames.BorderBottomWidth, PropertyNames.BorderLeftWidth),
            LineStyleConverter.Option().For(PropertyNames.BorderTopStyle, PropertyNames.BorderRightStyle, PropertyNames.BorderBottomStyle, PropertyNames.BorderLeftStyle),
            CurrentColorConverter.Option().For(PropertyNames.BorderTopColor, PropertyNames.BorderRightColor, PropertyNames.BorderBottomColor, PropertyNames.BorderLeftColor));

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
