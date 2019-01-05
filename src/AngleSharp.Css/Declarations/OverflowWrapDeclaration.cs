namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class OverflowWrapDeclaration
    {
        public static String Name = PropertyNames.OverflowWrap;

        public static IValueConverter Converter = OverflowWrapConverter;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
