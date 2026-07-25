namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextWrapStyleDeclaration
    {
        public static String Name = PropertyNames.TextWrapStyle;

        public static IValueConverter Converter = TextWrapStyleConverter;

        public static ICssValue InitialValue = InitialValues.TextWrapStyleDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
