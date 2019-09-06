namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FootnotePolicyDeclaration
    {
        public static String Name = PropertyNames.FootnotePolicy;

        public static IValueConverter Converter = FootnotePolicyConverter;

        public static ICssValue InitialValue = InitialValues.FootnotePolicyDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
