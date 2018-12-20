namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderWidthDeclaration
    {
        public static String Name = PropertyNames.BorderWidth;

        public static String Parent = PropertyNames.Border;

        public static IValueConverter Converter = Or(LineWidthConverter.Periodic(), AssignInitial());

        public static IValueAggregator Aggregator = new BorderWidthAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Children = new[]
        {
            PropertyNames.BorderLeftWidth,
            PropertyNames.BorderRightWidth,
            PropertyNames.BorderTopWidth,
            PropertyNames.BorderBottomWidth,
        };
    }
}
