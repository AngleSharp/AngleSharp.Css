namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColumnRuleStyleDeclaration
    {
        public static String Name = PropertyNames.ColumnRuleStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ColumnRule,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(LineStyle.None));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
