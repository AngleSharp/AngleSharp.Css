namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class ColumnRuleDeclaration
    {
        public static String Name = PropertyNames.ColumnRule;

        public static IValueConverter Converter = Or(new ColumnRuleValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new ColumnRuleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Children = new[]
        {
            PropertyNames.ColumnRuleColor,
            PropertyNames.ColumnRuleStyle,
            PropertyNames.ColumnRuleWidth,
        };
    }
}
