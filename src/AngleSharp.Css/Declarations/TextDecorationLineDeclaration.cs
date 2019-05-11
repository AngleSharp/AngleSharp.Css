namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextDecorationLineDeclaration
    {
        public static String Name = PropertyNames.TextDecorationLine;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = TextDecorationLinesConverter;

        public static ICssValue InitialValue = InitialValues.TextDecorationLineDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
