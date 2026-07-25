namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ViewTransitionNameDeclaration
    {
        public static String Name = PropertyNames.ViewTransitionName;

        public static IValueConverter Converter = ViewTransitionNameConverter;

        public static ICssValue InitialValue = InitialValues.ViewTransitionNameDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
