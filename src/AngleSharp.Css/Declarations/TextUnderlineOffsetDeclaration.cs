namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextUnderlineOffsetDeclaration
    {
        public static String Name = PropertyNames.TextUnderlineOffset;

        public static String[] Shorthands = new[]
        {
            PropertyNames.TextDecoration,
        };

        public static IValueConverter Converter = TextUnderlineOffsetConverter;

        public static ICssValue InitialValue = InitialValues.TextUnderlineOffsetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
