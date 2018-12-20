namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class ContentDeclaration
    {
        public static String Name = PropertyNames.Content;

        public static IValueConverter Converter = Or(new ContentValueConverter(), AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
