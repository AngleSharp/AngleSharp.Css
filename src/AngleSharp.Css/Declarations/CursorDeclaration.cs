namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class CursorDeclaration
    {
        public static String Name = PropertyNames.Cursor;

        public static IValueConverter Converter = Or(new CursorValueConverter(), AssignInitial(SystemCursor.Auto));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
