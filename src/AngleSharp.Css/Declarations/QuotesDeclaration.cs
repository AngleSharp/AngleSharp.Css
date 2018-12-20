namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class QuotesDeclaration
    {
        public static String Name = PropertyNames.Quotes;

        public static IValueConverter Converter = Or(QuotesConverter, None, AssignInitial(new[] { "«", "»" }));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
