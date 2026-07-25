namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontSynthesisSmallCapsDeclaration
    {
        public static String Name = PropertyNames.FontSynthesisSmallCaps;

        public static String[] Shorthands = new[]
        {
            PropertyNames.FontSynthesis,
        };

        public static IValueConverter Converter = FontSynthesisSmallCapsConverter;

        public static ICssValue InitialValue = InitialValues.FontSynthesisSmallCapsDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
