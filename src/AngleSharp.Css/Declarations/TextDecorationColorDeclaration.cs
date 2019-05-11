namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextDecorationColorDeclaration
    {
        public static String Name = PropertyNames.TextDecorationColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.TextDecorationColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
