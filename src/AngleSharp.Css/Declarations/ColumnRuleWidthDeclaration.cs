namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ColumnRuleWidthDeclaration
    {
        public static String Name = PropertyNames.ColumnRuleWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ColumnRule,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(InitialValues.ColumnRuleWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
