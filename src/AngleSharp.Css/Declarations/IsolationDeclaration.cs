namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class IsolationDeclaration
    {
        public static String Name = PropertyNames.Isolation;

        public static IValueConverter Converter = IsolationConverter;

        public static ICssValue InitialValue = InitialValues.IsolationDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
