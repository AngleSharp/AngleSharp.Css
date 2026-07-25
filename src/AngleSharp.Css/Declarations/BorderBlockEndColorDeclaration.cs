namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBlockEndColorDeclaration
    {
        public static String Name = PropertyNames.BorderBlockEndColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
            PropertyNames.BorderBlockEnd,
            PropertyNames.BorderBlockColor,
        };

        public static IValueConverter Converter = CurrentColorConverter;

        public static ICssValue InitialValue = InitialValues.BorderBlockEndColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
