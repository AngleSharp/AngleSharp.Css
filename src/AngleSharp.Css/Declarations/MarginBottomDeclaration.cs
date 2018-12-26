namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class MarginBottomDeclaration
    {
        public static String Name = PropertyNames.MarginBottom;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Margin,
        };

        public static IValueConverter Converter = MarginConverter;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
