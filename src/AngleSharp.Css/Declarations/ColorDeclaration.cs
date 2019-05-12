namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColorDeclaration
    {
        public static String Name = PropertyNames.Color;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable;
    }
}
