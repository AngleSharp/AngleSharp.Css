namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextDecorationSkipInkDeclaration
    {
        public static String Name = PropertyNames.TextDecorationSkipInk;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = TextDecorationSkipInkConverter;

        public static ICssValue InitialValue = InitialValues.TextDecorationSkipInkDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
