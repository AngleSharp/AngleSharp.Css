namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColorSchemeDeclaration
    {
        public static String Name = PropertyNames.ColorScheme;

        public static IValueConverter Converter = ColorSchemeConverter;

        public static ICssValue InitialValue = InitialValues.ColorSchemeDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
