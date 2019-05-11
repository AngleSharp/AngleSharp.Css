namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderTopRightRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderTopRightRadius;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderRadius,
        };

        public static IValueConverter Converter = BorderRadiusLonghandConverter;

        public static ICssValue InitialValue = InitialValues.BorderRadiusDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
