namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextAlignLastDeclaration
    {
        public static String Name = PropertyNames.TextAlignLast;

        public static IValueConverter Converter = TextAlignLastConverter;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
