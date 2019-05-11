namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PageBreakAfterDeclaration
    {
        public static String Name = PropertyNames.PageBreakAfter;

        public static IValueConverter Converter = PageBreakModeConverter;

        public static ICssValue InitialValue = InitialValues.PageBreakAfterDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
