namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class ColumnsDeclaration
    {
        public static String Name = PropertyNames.Columns;

        public static IValueConverter Converter = new ColumnsAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ColumnWidth,
            PropertyNames.ColumnCount,
        };

        sealed class ColumnsValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                AutoLengthConverter,
                OptionalIntegerConverter);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class ColumnsAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = new ColumnsValueConverter();

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var width = values[0];
                var count = values[1];

                if (width != null || count != null)
                {
                    return new CssTupleValue(new[] { width, count });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue options)
                {
                    return new[]
                    {
                        options.Items[0],
                        options.Items[1],
                    };
                }

                return null;
            }
        }
    }
}
