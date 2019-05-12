namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarHighlightColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarHighlightColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarHighlightColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
