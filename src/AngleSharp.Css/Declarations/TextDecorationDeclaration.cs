namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class TextDecorationDeclaration
    {
        public static String Name = PropertyNames.TextDecoration;

        public static IValueConverter Converter = Or(new TextDecorationValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new TextDecorationAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Children = new[]
        {
            PropertyNames.TextDecorationColor,
            PropertyNames.TextDecorationLine,
            PropertyNames.TextDecorationStyle,
        };
    }
}
