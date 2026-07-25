namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontSynthesisStyleDeclaration
    {
        public static String Name = PropertyNames.FontSynthesisStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.FontSynthesis,
        };

        public static IValueConverter Converter = FontSynthesisStyleConverter;

        public static ICssValue InitialValue = InitialValues.FontSynthesisStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
