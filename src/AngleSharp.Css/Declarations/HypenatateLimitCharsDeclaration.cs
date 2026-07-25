namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class HypenatateLimitCharsDeclaration
    {
        public static String Name = PropertyNames.HypenatateLimitChars;

        public static IValueConverter Converter = HypenatateLimitCharsConverter;

        public static ICssValue InitialValue = InitialValues.HypenatateLimitCharsDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
