namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class BorderDeclaration
    {
        public static String Name = PropertyNames.Border;

        public static IValueConverter Converter = new BorderAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderStyle,
            PropertyNames.BorderColor,
        };

        sealed class BorderValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                LineWidthConverter.Option(),
                LineStyleConverter.Option(),
                CurrentColorConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class BorderAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new BorderValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var width = values[0];
                var style = values[1];
                var color = values[2];

                if (width != null && style != null && color != null)
                {
                    return new CssTupleValue(new[] { width, style, color });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var options = value as CssTupleValue;

                if (options != null)
                {
                    return new[] { options.Items[0], options.Items[1], options.Items[2] };
                }

                return null;
            }
        }
    }
}
