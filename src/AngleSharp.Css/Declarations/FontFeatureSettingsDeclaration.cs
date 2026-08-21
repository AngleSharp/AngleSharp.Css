namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontFeatureSettingsDeclaration
    {
        public static String Name = PropertyNames.FontFeatureSettings;

        public static IValueConverter Converter = FontFeatureSettingsConverter;

        public static ICssValue InitialValue = InitialValues.FontFeatureSettingsDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
