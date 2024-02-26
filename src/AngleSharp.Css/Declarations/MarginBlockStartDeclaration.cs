namespace AngleSharp.Css.Declarations
{
    using System;
    using Dom;
    using static ValueConverters;

    static class MarginBlockStartDeclaration
    {
        public static String Name = PropertyNames.MarginBlockStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MarginBlock,
        };

        public static IValueConverter Converter = AutoLengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.MarginBlockStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}