namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackdropFilterDeclaration
    {
        public static String Name = PropertyNames.BackdropFilter;

        public static IValueConverter Converter = BackdropFilterConverter;

        public static ICssValue InitialValue = InitialValues.BackdropFilterDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
