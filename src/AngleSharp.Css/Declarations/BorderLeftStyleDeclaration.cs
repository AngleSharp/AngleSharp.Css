namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderLeftStyleDeclaration
    {
        public static String Name = PropertyNames.BorderLeftStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderLeft,
            PropertyNames.BorderStyle,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderLeftStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
