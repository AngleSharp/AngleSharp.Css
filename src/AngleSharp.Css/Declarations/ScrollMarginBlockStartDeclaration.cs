namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginBlockStartDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginBlockStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMarginBlock,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginBlockStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
