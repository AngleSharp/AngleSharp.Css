namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class OutlineDeclaration
    {
        public static String Name = PropertyNames.Outline;

        public static IValueConverter Converter = Or(new OutlineValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new OutlineAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.OutlineColor,
            PropertyNames.OutlineStyle,
            PropertyNames.OutlineWidth,
        };
    }
}
