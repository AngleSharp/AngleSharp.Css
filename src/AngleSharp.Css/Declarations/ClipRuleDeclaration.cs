namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ClipRuleDeclaration
    {
        public static String Name = PropertyNames.ClipRule;

        public static IValueConverter Converter = Or(
            Assign("nonzero", "nonzero"),
            Assign("evenodd", "evenodd"));

        public static ICssValue InitialValue = InitialValues.ClipRuleDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}