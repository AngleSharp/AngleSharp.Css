namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderStartStartRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderStartStartRadius;

        public static IValueConverter Converter = BorderRadiusLonghandConverter;

        public static ICssValue InitialValue = InitialValues.BorderStartStartRadiusDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
