namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ListStylePositionDeclaration
    {
        public static String Name = PropertyNames.ListStylePosition;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ListStyle,
        };

        public static IValueConverter Converter = ListPositionConverter;

        public static ICssValue InitialValue = InitialValues.ListStylePositionDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
