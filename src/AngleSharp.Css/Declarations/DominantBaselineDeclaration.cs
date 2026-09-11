namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class DominantBaselineDeclaration
    {
        public static String Name = PropertyNames.DominantBaseline;

        public static IValueConverter Converter = IdentifierConverter;

        public static ICssValue InitialValue = InitialValues.DominantBaselineDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}