namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderTopWidthDeclaration
    {
        public static String Name = PropertyNames.BorderTopWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderTop,
            PropertyNames.BorderWidth,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.BorderTopWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
