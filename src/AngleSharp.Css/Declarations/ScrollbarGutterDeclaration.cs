namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarGutterDeclaration
    {
        public static String Name = PropertyNames.ScrollbarGutter;

        public static IValueConverter Converter = ScrollbarGutterConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarGutterDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
