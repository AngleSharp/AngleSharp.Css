namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ColumnRuleWidthDeclaration
    {
        public static String Name = PropertyNames.ColumnRuleWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ColumnRule,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(Length.Medium));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
