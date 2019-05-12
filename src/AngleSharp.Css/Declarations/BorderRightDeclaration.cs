namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderRightDeclaration
    {
        public static String Name = PropertyNames.BorderRight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = WithBorderSide(
            InitialValues.BorderRightWidthDecl,
            InitialValues.BorderRightStyleDecl,
            InitialValues.BorderRightColorDecl);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderRightWidth,
            PropertyNames.BorderRightStyle,
            PropertyNames.BorderRightColor,
        };
    }
}
