namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ListStyleTypeDeclaration
    {
        public static String Name = PropertyNames.ListStyleType;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ListStyle,
        };

        public static IValueConverter Converter = ListStyleConverter;

        public static ICssValue InitialValue = InitialValues.ListStyleTypeDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
