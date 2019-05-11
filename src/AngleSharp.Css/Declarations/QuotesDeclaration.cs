namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class QuotesDeclaration
    {
        public static String Name = PropertyNames.Quotes;

        public static IValueConverter Converter = Or(QuotesConverter, None);

        public static ICssValue InitialValue = InitialValues.QuotesDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
