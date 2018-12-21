namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextDecorationLineDeclaration
    {
        public static String Name = PropertyNames.TextDecorationLine;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = Or(TextDecorationLinesConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
