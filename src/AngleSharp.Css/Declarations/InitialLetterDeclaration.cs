namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class InitialLetterDeclaration
    {
        public static String Name = PropertyNames.InitialLetter;

        public static IValueConverter Converter = InitialLetterConverter;

        public static ICssValue InitialValue = InitialValues.InitialLetterDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
