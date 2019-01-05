namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class GapDeclaration
    {
        public static readonly String Name = PropertyNames.Gap;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.ColumnGap,
            PropertyNames.RowGap,
        };

        public static readonly IValueConverter Converter = new GapAggregagtor();

        public static readonly PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        sealed class GapAggregagtor : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(WithOrder(GapConverter, GapConverter), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var col = values[0];
                var row = values[1];

                if (row != null || col != null)
                {
                    return new CssTupleValue(new[] { row, col });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var list = value as CssTupleValue;

                if (list != null)
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
