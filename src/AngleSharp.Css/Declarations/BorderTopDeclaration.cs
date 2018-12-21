namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderTopDeclaration
    {
        public static String Name = PropertyNames.BorderTop;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial());

        public static IValueAggregator Aggregator = new BorderTopAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopWidth,
            PropertyNames.BorderTopStyle,
            PropertyNames.BorderTopColor,
        };
    }
}
