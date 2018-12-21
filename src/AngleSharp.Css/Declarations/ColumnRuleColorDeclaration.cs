namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ColumnRuleColorDeclaration
    {
        public static String Name = PropertyNames.ColumnRuleColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ColumnRule,
        };

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Color.Transparent));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
