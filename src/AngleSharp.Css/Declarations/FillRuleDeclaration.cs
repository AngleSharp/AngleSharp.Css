namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FillRuleDeclaration
    {
        public static String Name = PropertyNames.FillRule;

        public static IValueConverter Converter = Or(
            Assign("nonzero", "nonzero"),
            Assign("evenodd", "evenodd"));

        public static ICssValue InitialValue = InitialValues.FillRuleDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}