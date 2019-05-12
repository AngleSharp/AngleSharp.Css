namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextAlignLastDeclaration
    {
        public static String Name = PropertyNames.TextAlignLast;

        public static IValueConverter Converter = TextAlignLastConverter;

        public static ICssValue InitialValue = InitialValues.TextAlignLastDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
