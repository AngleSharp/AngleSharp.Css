namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginTopDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginTop;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMargin,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginTopDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
