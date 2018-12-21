namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class PaddingDeclaration
    {
        public static String Name = PropertyNames.Padding;

        public static IValueConverter Converter = Or(LengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero));

        public static IValueAggregator Aggregator = new PaddingAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.PaddingBottom,
            PropertyNames.PaddingLeft,
            PropertyNames.PaddingTop,
            PropertyNames.PaddingRight,
        };
    }
}
