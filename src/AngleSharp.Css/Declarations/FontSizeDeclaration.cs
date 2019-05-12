namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontSizeDeclaration
    {
        public static String Name = PropertyNames.FontSize;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = FontSizeConverter;

        public static ICssValue InitialValue = InitialValues.FontSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
