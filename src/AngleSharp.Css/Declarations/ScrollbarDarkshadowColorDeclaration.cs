namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarDarkshadowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarDarkShadowColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarDarkshadowColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
