namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class UnicodeRangeDeclaration
    {
        public static String Name = PropertyNames.UnicodeRange;

        public static IValueConverter Converter = Any;

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
