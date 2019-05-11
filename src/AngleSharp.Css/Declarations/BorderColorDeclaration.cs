namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderColorDeclaration
    {
        public static String Name = PropertyNames.BorderColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = AggregatePeriodic(CurrentColorConverter);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Hashless | PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopColor,
            PropertyNames.BorderRightColor,
            PropertyNames.BorderBottomColor,
            PropertyNames.BorderLeftColor,
        };
    }
}
