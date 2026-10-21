namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class HeightDeclaration
    {
        public static String Name = PropertyNames.Height;

        public static IValueConverter Converter = WidthConverter;

        public static ICssValue InitialValue = InitialValues.HeightDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
