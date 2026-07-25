namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginRightDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginRight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMargin,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginRightDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
