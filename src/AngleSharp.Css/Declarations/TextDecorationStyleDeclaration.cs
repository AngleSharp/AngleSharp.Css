namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextDecorationStyleDeclaration
    {
        public static String Name = PropertyNames.TextDecorationStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = Or(TextDecorationStyleConverter, AssignInitial(TextDecorationStyle.Solid));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
