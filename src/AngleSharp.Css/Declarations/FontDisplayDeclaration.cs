namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontDisplayDeclaration
    {
        public static String Name = PropertyNames.FontDisplay;

        public static IValueConverter Converter = FontDisplayConverter;

        public static ICssValue InitialValue = InitialValues.FontDisplayDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
