namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontKerningDeclaration
    {
        public static String Name = PropertyNames.FontKerning;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontKerningConverter;

        public static ICssValue InitialValue = InitialValues.FontKerningDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
