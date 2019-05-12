namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarArrowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarArrowColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarArrowColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
