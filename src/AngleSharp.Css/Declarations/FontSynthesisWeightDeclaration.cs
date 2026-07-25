namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FontSynthesisWeightDeclaration
    {
        public static String Name = PropertyNames.FontSynthesisWeight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.FontSynthesis,
        };

        public static IValueConverter Converter = FontSynthesisWeightConverter;

        public static ICssValue InitialValue = InitialValues.FontSynthesisWeightDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
