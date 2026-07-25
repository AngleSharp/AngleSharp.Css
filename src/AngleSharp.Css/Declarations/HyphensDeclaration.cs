namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class HyphensDeclaration
    {
        public static String Name = PropertyNames.Hyphens;

        public static IValueConverter Converter = HyphensConverter;

        public static ICssValue InitialValue = InitialValues.HyphensDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
