namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderInlineEndColorDeclaration
    {
        public static String Name = PropertyNames.BorderInlineEndColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderInline,
            PropertyNames.BorderInlineEnd,
            PropertyNames.BorderInlineColor,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderInlineEndColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
