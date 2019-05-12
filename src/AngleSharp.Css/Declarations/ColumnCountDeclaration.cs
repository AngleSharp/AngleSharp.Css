namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColumnCountDeclaration
    {
        public static String Name = PropertyNames.ColumnCount;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Columns,
        };

        public static IValueConverter Converter = OptionalIntegerConverter;

        public static ICssValue InitialValue = InitialValues.ColumnCountDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
