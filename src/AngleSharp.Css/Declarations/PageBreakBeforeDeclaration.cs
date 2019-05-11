namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PageBreakBeforeDeclaration
    {
        public static String Name = PropertyNames.PageBreakBefore;

        public static IValueConverter Converter = PageBreakModeConverter;

        public static ICssValue InitialValue = InitialValues.PageBreakBeforeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
