namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderLeftDeclaration
    {
        public static String Name = PropertyNames.BorderLeft;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = WithBorderSide(
            InitialValues.BorderLeftWidthDecl,
            InitialValues.BorderLeftStyleDecl,
            InitialValues.BorderLeftColorDecl);

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderLeftWidth,
            PropertyNames.BorderLeftStyle,
            PropertyNames.BorderLeftColor,
        };
    }
}
