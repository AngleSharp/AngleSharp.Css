namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderStyleDeclaration
    {
        public static String Name = PropertyNames.BorderStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = AggregatePeriodic(LineStyleConverter);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopStyle,
            PropertyNames.BorderRightStyle,
            PropertyNames.BorderBottomStyle,
            PropertyNames.BorderLeftStyle,
        };
    }
}
