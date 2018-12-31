namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class TextDecorationDeclaration
    {
        public static String Name = PropertyNames.TextDecoration;

        public static IValueConverter Converter = new TextDecorationAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.TextDecorationColor,
            PropertyNames.TextDecorationLine,
            PropertyNames.TextDecorationStyle,
        };

        sealed class TextDecorationValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                ColorConverter.Option(),
                TextDecorationStyleConverter.Option(),
                TextDecorationLinesConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class TextDecorationAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new TextDecorationValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var color = values[0];
                var line = values[1];
                var style = values[2];

                if (color != null || style != null || line != null)
                {
                    return new CssTupleValue(new[] { color, style, line });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var options = value as CssTupleValue;

                if (options != null)
                {
                    return new[]
                    {
                        options.Items[0],
                        options.Items[1],
                        options.Items[2],
                    };
                }

                return null;
            }
        }
    }
}
