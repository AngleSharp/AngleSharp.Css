#nullable disable
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

        // Same fix, same reasoning as GapDeclaration.Longhands - order must match
        // GridGapAggregagtor.Split()'s own [row, col] convention. The legacy name of
        // `gap` also sets `row-gap` and `column-gap`, which layouts read.
        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridRowGap,
            PropertyNames.GridColumnGap,
            PropertyNames.RowGap,
            PropertyNames.ColumnGap,
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
                    // `grid-gap: 8px` sets both gaps, as `gap` does
                    var row = list.Items[0];
                    var col = list.Items.Length > 1 && list.Items[1] != null ? list.Items[1] : row;
                    return new[] { row, col, row, col };
                }

                return null;
            }
        }
    }
}
