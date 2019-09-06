namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BookmarkStateDeclaration
    {
        public static String Name = PropertyNames.BookmarkState;

        public static IValueConverter Converter = BookmarkStateConverter;

        public static ICssValue InitialValue = InitialValues.BookmarkStateDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
