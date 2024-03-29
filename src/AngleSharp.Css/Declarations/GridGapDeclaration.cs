namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class GridGapDeclaration
    {
        public static readonly String Name = PropertyNames.GridGap;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridColumnGap,
            PropertyNames.GridRowGap,
        };

        public static readonly IValueConverter Converter = new GridGapAggregagtor();

        public static readonly ICssValue InitialValue = null;

        public static readonly PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        sealed class GridGapAggregagtor : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = WithOrder(GapConverter, GapConverter);

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var row = values[0];
                var col = values[1];

                if (row != null || col != null)
                {
                    return new CssTupleValue(new[] { row, col });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue list)
                {
                    return new[]
                    {
                        list.Items[0],
                        list.Items[1],
                    };
                }

                return null;
            }
        }
    }
}
