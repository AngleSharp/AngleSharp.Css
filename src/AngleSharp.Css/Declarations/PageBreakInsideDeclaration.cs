namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PageBreakInsideDeclaration
    {
        public static String Name = PropertyNames.PageBreakInside;

        public static IValueConverter Converter = Or(PageBreakInsideModeConverter, AssignInitial(InitialValues.PageBreakInsideDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
