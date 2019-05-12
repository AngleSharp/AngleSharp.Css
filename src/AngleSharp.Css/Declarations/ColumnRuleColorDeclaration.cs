namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColumnRuleColorDeclaration
    {
        public static String Name = PropertyNames.ColumnRuleColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ColumnRule,
        };

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.ColumnRuleColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
