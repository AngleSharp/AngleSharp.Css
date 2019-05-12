namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextJustifyDeclaration
    {
        public static String Name = PropertyNames.TextJustify;

        public static IValueConverter Converter = TextJustifyConverter;

        public static ICssValue InitialValue = InitialValues.TextJustifyDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
