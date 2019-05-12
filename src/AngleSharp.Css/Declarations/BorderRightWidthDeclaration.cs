namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderRightWidthDeclaration
    {
        public static String Name = PropertyNames.BorderRightWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderRight,
            PropertyNames.BorderWidth,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.BorderRightWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
