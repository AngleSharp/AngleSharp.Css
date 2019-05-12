namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PageBreakInsideDeclaration
    {
        public static String Name = PropertyNames.PageBreakInside;

        public static IValueConverter Converter = PageBreakInsideModeConverter;

        public static ICssValue InitialValue = InitialValues.PageBreakInsideDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
