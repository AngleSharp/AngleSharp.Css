namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class InitialLetterAlignDeclaration
    {
        public static String Name = PropertyNames.InitialLetterAlign;

        public static IValueConverter Converter = InitialLetterAlignConverter;

        public static ICssValue InitialValue = InitialValues.InitialLetterAlignDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
