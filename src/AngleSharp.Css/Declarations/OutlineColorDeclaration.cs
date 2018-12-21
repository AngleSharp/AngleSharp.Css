namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class OutlineColorDeclaration
    {
        public static String Name = PropertyNames.OutlineColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Outline,
        };

        public static IValueConverter Converter = Or(InvertedColorConverter, AssignInitial(Color.Transparent));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
