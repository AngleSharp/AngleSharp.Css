namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    sealed class BorderRadiusValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter = LengthOrPercentConverter.Periodic();

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

            return vertical != null ? new BorderRadiusValue(horizontal, vertical) : null;
        }

        private sealed class BorderRadiusValue : ICssValue
        {
            private readonly ICssValue _horizontal;
            private readonly ICssValue _vertical;

            public BorderRadiusValue(ICssValue horizontal, ICssValue vertical)
            {
                _horizontal = horizontal;
                _vertical = vertical;
            }

            public String CssText
            {
                get
                {
                    if (!Object.ReferenceEquals(_horizontal, _vertical))
                    {
                        return String.Concat(_horizontal.CssText, " / ", _vertical.CssText);
                    }

                    return _horizontal.CssText;
                }
            }
        }
    }
}
