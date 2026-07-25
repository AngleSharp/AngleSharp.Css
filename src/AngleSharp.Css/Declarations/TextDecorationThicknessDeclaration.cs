namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextDecorationThicknessDeclaration
    {
        public static String Name = PropertyNames.TextDecorationThickness;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = TextDecorationThicknessConverter;

        public static ICssValue InitialValue = InitialValues.TextDecorationThicknessDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
