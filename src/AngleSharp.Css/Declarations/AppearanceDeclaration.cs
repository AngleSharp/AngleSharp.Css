namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AppearanceDeclaration
    {
        public static String Name = PropertyNames.Appearance;

        public static IValueConverter Converter = AppearanceConverter;

        public static ICssValue InitialValue = InitialValues.AppearanceDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
