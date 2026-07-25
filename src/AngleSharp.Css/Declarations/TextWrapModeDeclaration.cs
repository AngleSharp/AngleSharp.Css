namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextWrapModeDeclaration
    {
        public static String Name = PropertyNames.TextWrapMode;

        public static IValueConverter Converter = TextWrapModeConverter;

        public static ICssValue InitialValue = InitialValues.TextWrapModeDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
