namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class JustifyContentDeclaration
    {
        public static String Name = PropertyNames.JustifyContent;

        public static IValueConverter Converter = JustifyContentConverter;

        public static ICssValue InitialValue = InitialValues.JustifyContentDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
