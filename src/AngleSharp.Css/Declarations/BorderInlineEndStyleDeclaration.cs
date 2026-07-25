namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderInlineEndStyleDeclaration
    {
        public static String Name = PropertyNames.BorderInlineEndStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderInline,
            PropertyNames.BorderInlineEnd,
            PropertyNames.BorderInlineStyle,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderInlineEndStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
