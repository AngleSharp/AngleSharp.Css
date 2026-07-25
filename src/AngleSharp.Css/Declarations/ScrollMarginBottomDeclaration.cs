namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginBottomDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginBottom;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMargin,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginBottomDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
