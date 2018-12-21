namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderStyleDeclaration
    {
        public static String Name = PropertyNames.BorderStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = Or(LineStyleConverter.Periodic(), AssignInitial());

        public static IValueAggregator Aggregator = new BorderStyleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderLeftStyle,
            PropertyNames.BorderRightStyle,
            PropertyNames.BorderTopStyle,
            PropertyNames.BorderBottomStyle,
        };
    }
}
