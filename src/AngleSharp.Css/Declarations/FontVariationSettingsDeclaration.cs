namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontVariationSettingsDeclaration
    {
        public static String Name = PropertyNames.FontVariationSettings;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontVariationSettingsConverter;

        public static ICssValue InitialValue = InitialValues.FontVariationSettingsDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
