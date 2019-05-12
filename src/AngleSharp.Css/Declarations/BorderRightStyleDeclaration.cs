namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderRightStyleDeclaration
    {
        public static String Name = PropertyNames.BorderRightStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderRight,
            PropertyNames.BorderStyle,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderRightStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
