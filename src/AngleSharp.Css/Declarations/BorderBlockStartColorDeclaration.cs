namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBlockStartColorDeclaration
    {
        public static String Name = PropertyNames.BorderBlockStartColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
            PropertyNames.BorderBlockStart,
            PropertyNames.BorderBlockColor,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderBlockStartColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
