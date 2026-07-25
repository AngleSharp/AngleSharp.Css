namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class HangingPunctuationDeclaration
    {
        public static String Name = PropertyNames.HangingPunctuation;

        public static IValueConverter Converter = HangingPunctuationConverter;

        public static ICssValue InitialValue = InitialValues.HangingPunctuationDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
