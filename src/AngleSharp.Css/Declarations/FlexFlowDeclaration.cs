namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class FlexFlowDeclaration
    {
        public static String Name = PropertyNames.FlexFlow;

        public static String[] Longhands = new[]
        {
            PropertyNames.FlexDirection,
            PropertyNames.FlexWrap,
        };

        public static IValueConverter Converter = new FlexFlowAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class FlexFlowAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                FlexDirectionConverter,
                FlexWrapConverter);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var direction = values[0];
                var wrap = values[1];

                if (direction != null || wrap != null)
                {
                    return new CssTupleValue(new[] { direction, wrap });
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
                    };
                }

                return null;
            }
        }
    }
}
