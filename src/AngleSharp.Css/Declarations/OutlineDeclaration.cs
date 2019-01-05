namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class OutlineDeclaration
    {
        public static String Name = PropertyNames.Outline;

        public static IValueConverter Converter = new OutlineAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.OutlineWidth,
            PropertyNames.OutlineStyle,
            PropertyNames.OutlineColor,
        };

        sealed class OutlineValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                LineWidthConverter.Option(),
                LineStyleConverter.Option(),
                InvertedColorConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class OutlineAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new OutlineValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var width = values[0];
                var style = values[1];
                var color = values[2];

                if (width != null || style != null || color != null)
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
