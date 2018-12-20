namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderCollapseDeclaration
    {
        public static String Name = PropertyNames.BorderCollapse;

        public static IValueConverter Converter = Or(BorderCollapseConverter, AssignInitial(true));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
