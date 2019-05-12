namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBottomWidthDeclaration
    {
        public static String Name = PropertyNames.BorderBottomWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderBottom,
            PropertyNames.BorderWidth,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.BorderBottomWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
