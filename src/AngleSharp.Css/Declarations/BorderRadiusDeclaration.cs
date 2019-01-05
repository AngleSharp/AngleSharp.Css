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

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopLeftRadius,
            PropertyNames.BorderTopRightRadius,
            PropertyNames.BorderBottomRightRadius,
            PropertyNames.BorderBottomLeftRadius,
        };

        sealed class BorderRadiusValueConverter : IValueConverter
        {
            private readonly IValueConverter _converter = LengthOrPercentConverter.Periodic();

            public ICssValue Convert(StringSource source)
            {
                var start = source.Index;
                var horizontal = _converter.Convert(source) as Periodic<ICssValue>;
                var vertical = horizontal;
                var c = source.SkipSpacesAndComments();

                if (c == Symbols.Solidus)
                {
                    source.SkipCurrentAndSpaces();
                    vertical = _converter.Convert(source) as Periodic<ICssValue>;
                }

                return vertical != null ? new BorderRadius(horizontal, vertical) : null;
            }
        }

        sealed class BorderRadiusAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new BorderRadiusValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            private static ICssValue Both(ICssValue horizontal, ICssValue vertical)
            {
                return new CssTupleValue(new[] { horizontal, vertical });
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var topLeft = values[0] as CssTupleValue;
                var topRight = values[1] as CssTupleValue;
                var bottomRight = values[2] as CssTupleValue;
                var bottomLeft = values[3] as CssTupleValue;

                if (topLeft != null && topRight != null && bottomRight != null && bottomLeft != null)
                {
                    var horizontal = new Periodic<ICssValue>(new[] { topLeft.Items[0], topRight.Items[0], bottomRight.Items[0], bottomLeft.Items[0] });
                    var vertical = new Periodic<ICssValue>(new[] { topLeft.Items[1], topRight.Items[1], bottomRight.Items[1], bottomLeft.Items[1] });
                    return new BorderRadius(horizontal, vertical);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var radius = value as BorderRadius;

                if (radius != null)
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
