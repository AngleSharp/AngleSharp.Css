namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WhiteSpaceDeclaration
    {
        public static String Name = PropertyNames.WhiteSpace;

        public static IValueConverter Converter = WhitespaceConverter;

        public static ICssValue InitialValue = InitialValues.WhiteSpaceDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
