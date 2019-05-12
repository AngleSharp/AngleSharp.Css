namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderLeftWidthDeclaration
    {
        public static String Name = PropertyNames.BorderLeftWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
            PropertyNames.BorderLeft,
            PropertyNames.BorderWidth,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.BorderLeftWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
