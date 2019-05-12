namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderCollapseDeclaration
    {
        public static String Name = PropertyNames.BorderCollapse;

        public static IValueConverter Converter = BorderCollapseConverter;

        public static ICssValue InitialValue = InitialValues.BorderCollapseDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
