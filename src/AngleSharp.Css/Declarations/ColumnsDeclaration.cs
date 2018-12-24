namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class ColumnsDeclaration
    {
        public static String Name = PropertyNames.Columns;

        public static IValueConverter Converter = Or(new ColumnsValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new ColumnsAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ColumnWidth,
            PropertyNames.ColumnCount,
        };
    }
}
