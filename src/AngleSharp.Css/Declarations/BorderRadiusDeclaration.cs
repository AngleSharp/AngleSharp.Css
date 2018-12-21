namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderRadius;

        public static IValueConverter Converter = Or(new BorderRadiusValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new BorderRadiusAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopLeftRadius,
            PropertyNames.BorderTopRightRadius,
            PropertyNames.BorderBottomLeftRadius,
            PropertyNames.BorderBottomRightRadius,
        };
    }
}
