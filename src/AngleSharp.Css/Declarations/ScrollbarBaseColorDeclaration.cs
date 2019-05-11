namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarBaseColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarBaseColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarBaseColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
