namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FilterDeclaration
    {
        public static String Name = PropertyNames.Filter;

        public static IValueConverter Converter = FilterConverter;

        public static ICssValue InitialValue = InitialValues.FilterDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}