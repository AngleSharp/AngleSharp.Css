namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarFaceColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarFaceColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarFaceColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
