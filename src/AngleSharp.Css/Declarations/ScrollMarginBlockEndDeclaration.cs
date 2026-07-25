namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginBlockEndDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginBlockEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMarginBlock,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginBlockEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
