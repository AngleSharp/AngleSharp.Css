namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderInlineStartStyleDeclaration
    {
        public static String Name = PropertyNames.BorderInlineStartStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderInline,
            PropertyNames.BorderInlineStart,
            PropertyNames.BorderInlineStyle,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderInlineStartStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
