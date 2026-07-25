namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ScrollMarginInlineStartDeclaration
    {
        public static String Name = PropertyNames.ScrollMarginInlineStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ScrollMarginInline,
        };

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.ScrollMarginInlineStartDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
