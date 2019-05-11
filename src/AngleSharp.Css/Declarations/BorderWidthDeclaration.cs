namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderWidthDeclaration
    {
        public static String Name = PropertyNames.BorderWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = AggregatePeriodic(LineWidthConverter);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopWidth,
            PropertyNames.BorderRightWidth,
            PropertyNames.BorderBottomWidth,
            PropertyNames.BorderLeftWidth,
        };
    }
}
