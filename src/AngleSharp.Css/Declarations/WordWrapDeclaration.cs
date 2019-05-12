namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WordWrapDeclaration
    {
        public static String Name = PropertyNames.WordWrap;

        public static IValueConverter Converter = OverflowWrapConverter;

        public static ICssValue InitialValue = InitialValues.WordWrapDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
