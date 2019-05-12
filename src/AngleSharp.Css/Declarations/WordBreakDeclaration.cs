namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class WordBreakDeclaration
    {
        public static String Name = PropertyNames.WordBreak;

        public static IValueConverter Converter = WordBreakConverter;

        public static ICssValue InitialValue = InitialValues.WordBreakDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
