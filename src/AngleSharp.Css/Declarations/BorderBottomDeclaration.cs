namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBottomDeclaration
    {
        public static String Name = PropertyNames.BorderBottom;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = WithBorderSide(
            InitialValues.BorderBottomWidthDecl,
            InitialValues.BorderBottomStyleDecl,
            InitialValues.BorderBottomColorDecl);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderBottomWidth,
            PropertyNames.BorderBottomStyle,
            PropertyNames.BorderBottomColor,
        };
    }
}
