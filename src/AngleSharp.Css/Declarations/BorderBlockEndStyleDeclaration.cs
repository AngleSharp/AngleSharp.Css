namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBlockEndStyleDeclaration
    {
        public static String Name = PropertyNames.BorderBlockEndStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
            PropertyNames.BorderBlockEnd,
            PropertyNames.BorderBlockStyle,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderBlockEndStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
