namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderBottomDeclaration
    {
        public static String Name = PropertyNames.BorderBottom;

        public static IValueConverter Converter = Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial());

        public static IValueAggregator Aggregator = new BorderBottomAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderBottomWidth,
            PropertyNames.BorderBottomStyle,
            PropertyNames.BorderBottomColor,
        };
    }
}
