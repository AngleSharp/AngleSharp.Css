namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBlockStartWidthDeclaration
    {
        public static String Name = PropertyNames.BorderBlockStartWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderBlock,
            PropertyNames.BorderBlockStart,
            PropertyNames.BorderBlockWidth,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.BorderBlockStartWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
