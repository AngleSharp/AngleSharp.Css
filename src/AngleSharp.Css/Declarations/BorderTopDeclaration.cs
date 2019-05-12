namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderTopDeclaration
    {
        public static String Name = PropertyNames.BorderTop;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = WithBorderSide(
            InitialValues.BorderTopWidthDecl,
            InitialValues.BorderTopStyleDecl,
            InitialValues.BorderTopColorDecl);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopWidth,
            PropertyNames.BorderTopStyle,
            PropertyNames.BorderTopColor,
        };
    }
}
