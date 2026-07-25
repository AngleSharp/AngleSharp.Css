namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarColor;

        public static IValueConverter Converter = ScrollbarColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarColorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
