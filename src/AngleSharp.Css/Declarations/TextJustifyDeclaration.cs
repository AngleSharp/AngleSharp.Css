namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextJustifyDeclaration
    {
        public static String Name = PropertyNames.TextJustify;

        public static IValueConverter Converter = TextJustifyConverter;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
