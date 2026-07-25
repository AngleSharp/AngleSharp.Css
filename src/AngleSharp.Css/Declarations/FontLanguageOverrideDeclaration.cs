namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontLanguageOverrideDeclaration
    {
        public static String Name = PropertyNames.FontLanguageOverride;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontLanguageOverrideConverter;

        public static ICssValue InitialValue = InitialValues.FontLanguageOverrideDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
