namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FootnoteDisplayDeclaration
    {
        public static String Name = PropertyNames.FootnoteDisplay;

        public static IValueConverter Converter = FootnoteDisplayConverter;

        public static ICssValue InitialValue = InitialValues.FootnoteDisplayDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
