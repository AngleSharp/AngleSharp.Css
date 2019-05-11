namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBottomStyleDeclaration
    {
        public static String Name = PropertyNames.BorderBottomStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderStyle,
            PropertyNames.BorderBottom,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderBottomStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
