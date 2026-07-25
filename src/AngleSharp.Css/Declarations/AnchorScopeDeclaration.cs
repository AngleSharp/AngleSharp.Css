namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnchorScopeDeclaration
    {
        public static String Name = PropertyNames.AnchorScope;

        public static IValueConverter Converter = AnchorScopeConverter;

        public static ICssValue InitialValue = InitialValues.AnchorScopeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
