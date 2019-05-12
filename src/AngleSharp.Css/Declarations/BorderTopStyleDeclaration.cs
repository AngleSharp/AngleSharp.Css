namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderTopStyleDeclaration
    {
        public static String Name = PropertyNames.BorderTopStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderTop,
            PropertyNames.BorderStyle,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderTopStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
