namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PageBreakAfterDeclaration
    {
        public static String Name = PropertyNames.PageBreakAfter;

        public static IValueConverter Converter = Or(PageBreakModeConverter, AssignInitial(InitialValues.PageBreakAfterDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
