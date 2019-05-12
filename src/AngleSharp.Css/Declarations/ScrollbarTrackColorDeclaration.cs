namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollbarTrackColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarTrackColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ScrollbarTrackColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
