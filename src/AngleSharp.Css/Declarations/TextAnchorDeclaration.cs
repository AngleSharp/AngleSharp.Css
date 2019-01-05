namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextAnchorDeclaration
    {
        public static String Name = PropertyNames.TextAnchor;

        public static IValueConverter Converter = TextAnchorConverter;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
