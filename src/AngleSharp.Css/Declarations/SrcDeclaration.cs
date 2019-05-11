namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class SrcDeclaration
    {
        public static String Name = PropertyNames.Src;

        public static IValueConverter Converter = SrcListConverter;

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
