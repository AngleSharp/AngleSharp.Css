namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    sealed class BorderRadiusConverter : IValueConverter
    {
        private readonly IValueConverter _converter = LengthOrPercentConverter.Periodic(
            PropertyNames.BorderTopLeftRadius, 
            PropertyNames.BorderTopRightRadius, 
            PropertyNames.BorderBottomRightRadius, 
            PropertyNames.BorderBottomLeftRadius);

        public ICssValue Convert(StringSource source)
        {
            var start = source.Index;
            var horizontal = _converter.Convert(source);
            var vertical = horizontal;

            if (source.Current == Symbols.Solidus)
            {
                source.SkipCurrentAndSpaces();
                vertical = _converter.Convert(source);
            }

            return vertical != null ? new BorderRadiusValue(source.Substring(start), horizontal, vertical) : null;
        }

        private sealed class BorderRadiusValue : BaseValue
        {
            private readonly ICssValue _horizontal;
            private readonly ICssValue _vertical;

            public BorderRadiusValue(String value, ICssValue horizontal, ICssValue vertical)
                : base(value)
            {
                _horizontal = horizontal;
                _vertical = vertical;
            }
        }
    }
}
