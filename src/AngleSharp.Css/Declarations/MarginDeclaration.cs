namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class MarginDeclaration
    {
        public static String Name = PropertyNames.Margin;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero));

        public static IValueAggregator Aggregator = new MarginAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.MarginBottom,
            PropertyNames.MarginLeft,
            PropertyNames.MarginTop,
            PropertyNames.MarginRight,
        };
    }
}
