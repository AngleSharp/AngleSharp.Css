namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextDecorationColorDeclaration
    {
        public static String Name = PropertyNames.TextDecorationColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.TextDecorationColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
