namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BookmarkLevelDeclaration
    {
        public static String Name = PropertyNames.BookmarkLevel;

        public static IValueConverter Converter = PositiveIntegerConverter;

        public static ICssValue InitialValue = InitialValues.BookmarkLevelDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
