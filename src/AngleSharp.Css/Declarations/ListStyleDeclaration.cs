namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class ListStyleDeclaration
    {
        public static String Name = PropertyNames.ListStyle;

        public static IValueConverter Converter = Or(new ListStyleValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new ListStyleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ListStyleImage,
            PropertyNames.ListStylePosition,
            PropertyNames.ListStyleType,
        };
    }
}
