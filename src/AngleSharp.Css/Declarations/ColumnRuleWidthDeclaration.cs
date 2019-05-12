namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ColumnRuleWidthDeclaration
    {
        public static String Name = PropertyNames.ColumnRuleWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ColumnRule,
        };

        public static IValueConverter Converter = LineWidthConverter;

        public static ICssValue InitialValue = InitialValues.ColumnRuleWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
