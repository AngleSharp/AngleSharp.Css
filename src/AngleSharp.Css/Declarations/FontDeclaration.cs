namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class FontDeclaration
    {
        public static String Name = PropertyNames.Font;

        public static IValueConverter Converter = Or(new FontValueConverter(), SystemFontConverter, AssignInitial());

        public static IValueAggregator Aggregator = new FontAggregator();

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Children = new[]
        {
            PropertyNames.FontFamily,
            PropertyNames.FontSize,
            PropertyNames.FontVariant,
            PropertyNames.FontWeight,
            PropertyNames.FontStyle,
            PropertyNames.LineHeight,
        };
    }
}
