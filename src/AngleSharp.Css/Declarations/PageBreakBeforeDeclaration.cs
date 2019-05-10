namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PageBreakBeforeDeclaration
    {
        public static String Name = PropertyNames.PageBreakBefore;

        public static IValueConverter Converter = Or(PageBreakModeConverter, AssignInitial(InitialValues.PageBreakBeforeDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
