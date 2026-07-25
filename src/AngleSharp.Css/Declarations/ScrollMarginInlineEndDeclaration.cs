namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginInlineEndDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginInlineEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMarginInline,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginInlineEndDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
