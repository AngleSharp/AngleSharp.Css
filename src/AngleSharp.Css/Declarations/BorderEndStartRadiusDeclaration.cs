namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderEndStartRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderEndStartRadius;

        public static IValueConverter Converter = BorderRadiusLonghandConverter;

        public static ICssValue InitialValue = InitialValues.BorderEndStartRadiusDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
