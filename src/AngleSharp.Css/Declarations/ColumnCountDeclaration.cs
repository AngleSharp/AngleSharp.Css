namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ColumnCountDeclaration
    {
        public static String Name = PropertyNames.ColumnCount;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Columns,
        };

        public static IValueConverter Converter = Or(OptionalIntegerConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
