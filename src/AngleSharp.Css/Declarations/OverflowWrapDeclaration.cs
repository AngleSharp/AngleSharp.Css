namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverflowWrapDeclaration
    {
        public static String Name = PropertyNames.OverflowWrap;

        public static IValueConverter Converter = OverflowWrapConverter;

        public static ICssValue InitialValue = InitialValues.OverflowWrapDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
