namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AlignmentBaselineDeclaration
    {
        public static String Name = PropertyNames.AlignBaseline;

        public static IValueConverter Converter = IdentifierConverter;

        public static ICssValue InitialValue = InitialValues.AlignmentBaselineDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}