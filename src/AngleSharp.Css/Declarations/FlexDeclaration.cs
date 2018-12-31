namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class FlexDeclaration
    {
        public static String Name = PropertyNames.Flex;

        public static String[] Longhands = new[]
        {
            PropertyNames.FlexGrow,
            PropertyNames.FlexShrink,
            PropertyNames.FlexBasis,
        };

        public static IValueConverter Converter = new FlexAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class FlexAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(None, WithAny(
                FlexGrowDeclaration.Converter,
                FlexShrinkDeclaration.Converter,
                FlexBasisDeclaration.Converter));

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var grow = values[0];
                var shrink = values[1];
                var basis = values[2];

                if (grow != null || shrink != null || basis != null)
                {
                    return new CssTupleValue(new[] { grow, shrink, basis });
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
