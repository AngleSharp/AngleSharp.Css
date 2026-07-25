namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextWrapDeclaration
    {
        public static String Name = PropertyNames.TextWrap;

        public static IValueConverter Converter = TextWrapConverter;

        public static ICssValue InitialValue = InitialValues.TextWrapDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
