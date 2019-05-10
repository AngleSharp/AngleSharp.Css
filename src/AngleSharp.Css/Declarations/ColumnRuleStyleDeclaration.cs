namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ColumnRuleStyleDeclaration
    {
        public static String Name = PropertyNames.ColumnRuleStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ColumnRule,
        };

        public static IValueConverter Converter = Or(LineStyleConverter, AssignInitial(InitialValues.ColumnRuleStyleDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
