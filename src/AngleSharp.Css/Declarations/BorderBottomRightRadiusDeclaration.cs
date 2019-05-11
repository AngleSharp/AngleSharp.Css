namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderBottomRightRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderBottomRightRadius;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderRadius,
        };

        public static IValueConverter Converter = BorderRadiusLonghandConverter;

        public static ICssValue InitialValue = InitialValues.BorderRadiusDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
