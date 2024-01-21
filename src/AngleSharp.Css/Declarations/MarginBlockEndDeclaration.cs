namespace AngleSharp.Css.Declarations
{
    using System;
    using Dom;
    using static ValueConverters;

    static class MarginBlockEndDeclaration
    {
        public static String Name = PropertyNames.MarginBlockEnd;

        public static String[] Shorthands =
        [
            PropertyNames.MarginBlock,
        ];

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MarginBlockEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
