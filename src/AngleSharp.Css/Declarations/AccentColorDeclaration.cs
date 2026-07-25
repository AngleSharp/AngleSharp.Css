namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AccentColorDeclaration
    {
        public static String Name = PropertyNames.AccentColor;

        public static IValueConverter Converter = AccentColorConverter;

        public static ICssValue InitialValue = InitialValues.AccentColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
