namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontPaletteDeclaration
    {
        public static String Name = PropertyNames.FontPalette;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontPaletteConverter;

        public static ICssValue InitialValue = InitialValues.FontPaletteDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
