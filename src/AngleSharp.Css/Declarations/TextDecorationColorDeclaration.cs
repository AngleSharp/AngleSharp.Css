namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class TextDecorationColorDeclaration
    {
        public static String Name = PropertyNames.TextDecorationColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Color.Black));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
