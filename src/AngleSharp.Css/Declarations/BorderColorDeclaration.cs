namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderColorDeclaration
    {
        public static String Name = PropertyNames.BorderColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = Or(CurrentColorConverter.Periodic(), AssignInitial());

        public static IValueAggregator Aggregator = new BorderColorAggregator();

        public static PropertyFlags Flags = PropertyFlags.Hashless | PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderLeftColor,
            PropertyNames.BorderRightColor,
            PropertyNames.BorderTopColor,
            PropertyNames.BorderBottomColor,
        };
    }
}
