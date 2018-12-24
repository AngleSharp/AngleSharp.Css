namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class UnicodeRangeDeclaration
    {
        public static String Name = PropertyNames.UnicodeRange;

        public static IValueConverter Converter = Any;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
