namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WhiteSpaceCollapseDeclaration
    {
        public static String Name = PropertyNames.WhiteSpaceCollapse;

        public static IValueConverter Converter = WhiteSpaceCollapseConverter;

        public static ICssValue InitialValue = InitialValues.WhiteSpaceCollapseDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
