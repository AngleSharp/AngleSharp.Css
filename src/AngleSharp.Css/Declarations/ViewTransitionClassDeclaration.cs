namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ViewTransitionClassDeclaration
    {
        public static String Name = PropertyNames.ViewTransitionClass;

        public static IValueConverter Converter = ViewTransitionClassConverter;

        public static ICssValue InitialValue = InitialValues.ViewTransitionClassDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
