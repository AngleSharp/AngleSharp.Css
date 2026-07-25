namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBlockStartStyleDeclaration
    {
        public static String Name = PropertyNames.BorderBlockStartStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
            PropertyNames.BorderBlockStart,
            PropertyNames.BorderBlockStyle,
        };

        public static IValueConverter Converter = LineStyleConverter;

        public static ICssValue InitialValue = InitialValues.BorderBlockStartStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
