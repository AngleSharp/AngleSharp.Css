namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class WordBreakDeclaration
    {
        public static String Name = PropertyNames.WordBreak;

        public static IValueConverter Converter = WordBreakConverter;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
