namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontVariantCapsDeclaration
    {
        public static String Name = PropertyNames.FontVariantCaps;

        public static String[] Shorthands = new[]
        {
            PropertyNames.FontVariant,
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontVariantCapsConverter;

        public static ICssValue InitialValue = InitialValues.FontVariantDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
