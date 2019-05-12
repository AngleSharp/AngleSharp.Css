namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextAnchorDeclaration
    {
        public static String Name = PropertyNames.TextAnchor;

        public static IValueConverter Converter = TextAnchorConverter;

        public static ICssValue InitialValue = InitialValues.TextAnchorDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
