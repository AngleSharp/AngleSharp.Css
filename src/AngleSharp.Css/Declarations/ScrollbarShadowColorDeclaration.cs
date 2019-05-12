namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarShadowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarShadowColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarShadowColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
