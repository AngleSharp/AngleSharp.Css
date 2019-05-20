namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BookmarkLabelDeclaration
    {
        public static String Name = PropertyNames.BookmarkLabel;

        public static IValueConverter Converter = Or(ContentListConverter, None);

        public static ICssValue InitialValue = InitialValues.BookmarkLabelDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
