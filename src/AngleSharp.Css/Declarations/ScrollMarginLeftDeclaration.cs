namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginLeftDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginLeft;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMargin,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginLeftDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
