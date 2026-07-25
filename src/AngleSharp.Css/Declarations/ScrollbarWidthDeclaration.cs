namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarWidthDeclaration
    {
        public static String Name = PropertyNames.ScrollbarWidth;

        public static IValueConverter Converter = ScrollbarWidthConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
