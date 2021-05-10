namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WidthDeclaration
    {
        public static String Name = PropertyNames.Width;

        public static IValueConverter Converter = WidthConverter;

        public static ICssValue InitialValue = InitialValues.WidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
