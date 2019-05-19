namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BookmarkLabelDeclaration
    {
        public static String Name = PropertyNames.BookmarkLabel;

        public static IValueConverter Converter = Any;

        public static ICssValue InitialValue = InitialValues.BookmarkLabelDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
