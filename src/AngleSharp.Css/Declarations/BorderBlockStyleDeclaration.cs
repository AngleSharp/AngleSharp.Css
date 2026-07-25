#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class BorderBlockStyleDeclaration
    {
        public static String Name = PropertyNames.BorderBlockStyle;

        public static IValueConverter Converter = new BorderBlockStyleAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.None | PropertyFlags.Shorthand;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
        };

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderBlockStartStyle,
            PropertyNames.BorderBlockEndStyle,
        };

        sealed class BorderBlockStyleAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(LineStyleConverter, VarConverter).FlowRelative();

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var start = values[0];
                var end = values[1];

                if (start != null && end != null)
                {
                    return new CssFlowRelativeValue(new[] { start, end });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssFlowRelativeValue flowRelative)
                {
                    return new[] { flowRelative.Start, flowRelative.End };
                }

                // Single value from parent shorthand — apply to both sides
                return new[] { value, value };
            }
        }
    }
}
