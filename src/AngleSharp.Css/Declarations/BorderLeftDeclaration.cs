namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderLeftDeclaration
    {
        public static String Name = PropertyNames.BorderLeft;

        public static String Parent = PropertyNames.Border;

        public static IValueConverter Converter = Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial());

        public static IValueAggregator Aggregator = new BorderLeftAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Children = new[]
        {
            PropertyNames.BorderLeftWidth,
            PropertyNames.BorderLeftStyle,
            PropertyNames.BorderLeftColor,
        };
    }
}
