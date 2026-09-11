namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextOverflowDeclaration
    {
        public static String Name = PropertyNames.TextOverflow;

        public static IValueConverter Converter = TextOverflowConverter;

        public static ICssValue InitialValue = InitialValues.TextOverflowDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
