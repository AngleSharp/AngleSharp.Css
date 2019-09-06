namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StringSetDeclaration
    {
        public static String Name = PropertyNames.StringSet;

        public static IValueConverter Converter = Or(
            WithOrder(IdentifierConverter, ContentListConverter).FromList(),
            None);

        public static ICssValue InitialValue = InitialValues.StringSetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
