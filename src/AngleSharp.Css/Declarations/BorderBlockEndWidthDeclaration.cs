namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBlockEndWidthDeclaration
    {
        public static String Name = PropertyNames.BorderBlockEndWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
            PropertyNames.BorderBlockEnd,
            PropertyNames.BorderBlockWidth,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.BorderBlockEndWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
