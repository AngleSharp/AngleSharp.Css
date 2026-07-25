namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderEndEndRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderEndEndRadius;

        public static IValueConverter Converter = BorderRadiusLonghandConverter;

        public static ICssValue InitialValue = InitialValues.BorderEndEndRadiusDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
