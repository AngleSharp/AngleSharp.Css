namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OrphansDeclaration
    {
        public static String Name = PropertyNames.Orphans;

        public static IValueConverter Converter = NaturalIntegerConverter;

        public static ICssValue InitialValue = InitialValues.OrphansDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
