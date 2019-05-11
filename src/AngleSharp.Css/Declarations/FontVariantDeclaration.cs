namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontVariantDeclaration
    {
        public static String Name = PropertyNames.FontVariant;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontVariantConverter;

        public static ICssValue InitialValue = InitialValues.FontVariantDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
