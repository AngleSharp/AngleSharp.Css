namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class BorderRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderRadius;

        public static IValueConverter Converter = new BorderRadiusAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopLeftRadius,
            PropertyNames.BorderTopRightRadius,
            PropertyNames.BorderBottomRightRadius,
            PropertyNames.BorderBottomLeftRadius,
        };

        sealed class BorderRadiusAggregator : IValueAggregator, IValueConverter
        {
            private readonly IValueConverter _converter = LengthOrPercentConverter.Periodic();

            public ICssValue Convert(StringSource source)
            {
                var start = source.Index;
                var horizontal = _converter.Convert(source) as CssPeriodicValue;
                var vertical = horizontal;
                var c = source.SkipSpacesAndComments();

                if (c == Symbols.Solidus)
                {
                    source.SkipCurrentAndSpaces();
                    vertical = _converter.Convert(source) as CssPeriodicValue;
                }

                return vertical != null ? new CssBorderRadiusValue(horizontal, vertical) : null;
            }

            private static ICssValue Both(params ICssValue[] values) => new CssRadiusValue(values);

            public ICssValue Merge(ICssValue[] values)
            {
                var topLeft = values[0] as CssRadiusValue;
                var topRight = values[1] as CssRadiusValue;
                var bottomRight = values[2] as CssRadiusValue;
                var bottomLeft = values[3] as CssRadiusValue;

                if (topLeft != null && topRight != null && bottomRight != null && bottomLeft != null)
                {
                    var horizontal = new CssPeriodicValue(new[] { topLeft.Width, topRight.Width, bottomRight.Width, bottomLeft.Width });
                    var vertical = new CssPeriodicValue(new[] { topLeft.Height, topRight.Height, bottomRight.Height, bottomLeft.Height });
                    return new CssBorderRadiusValue(horizontal, vertical);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssBorderRadiusValue radius)
                {
                    return new[]
                    {
                        Both(radius.Horizontal.Top, radius.Vertical.Top),
                        Both(radius.Horizontal.Right, radius.Vertical.Right),
                        Both(radius.Horizontal.Bottom, radius.Vertical.Bottom),
                        Both(radius.Horizontal.Left, radius.Vertical.Left),
                    };
                }

                return null;
            }
        }
    }
}
