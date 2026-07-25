namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderInlineStartColorDeclaration
    {
        public static String Name = PropertyNames.BorderInlineStartColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderInline,
            PropertyNames.BorderInlineStart,
            PropertyNames.BorderInlineColor,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderInlineStartColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
