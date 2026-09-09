#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class OverflowDeclaration
    {
        public static String Name = PropertyNames.Overflow;

        public static String[] Longhands = new[]
        {
            PropertyNames.OverflowX,
            PropertyNames.OverflowY,
        };

        public static IValueConverter Converter = new OverflowAggregator();

        public static ICssValue InitialValue = InitialValues.OverflowDecl;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class OverflowAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = OverflowExtendedModeConverter.Many(1, 2);

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var x = values[0];
                var y = values[1];

                if (x != null && y != null)
                {
                    return x.Equals(y) ? new CssTupleValue(new[] { x }) : new CssTupleValue(new[] { x, y });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue tuple)
                {
                    var first = tuple.Items[0];
                    var second = tuple.Items.Length > 1 ? tuple.Items[1] : first;
                    return new[] { first, second };
                }

                return null;
            }
        }
    }
}
