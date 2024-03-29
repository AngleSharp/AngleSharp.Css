namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class GridRowDeclaration
    {
        public static readonly String Name = PropertyNames.GridRow;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridRowStart,
            PropertyNames.GridRowEnd,
        };

        public static readonly IValueConverter Converter = new GridRowAggregator();

        public static readonly ICssValue InitialValue = null;

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class GridRowAggregator : IValueAggregator, IValueConverter
        {
            private static readonly String seperator = " / ";
            private static readonly IValueConverter converter = SlashSeparated(Or(
                Assign<Object>(CssKeywords.Auto, null),
                WithAny(Assign(CssKeywords.Span, true), IntegerConverter, IdentifierConverter))).Many(1, 2, seperator);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                return new CssTupleValue(values, seperator);
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue tuple)
                {
                    return new[]
                    {
                        tuple.Items[0],
                        GetItem(tuple, 1),
                    };
                }

                return null;
            }

            private static ICssValue GetItem(CssTupleValue tuple, Int32 index)
            {
                if (tuple.Items.Length > index)
                {
                    return tuple.Items[index];
                }
                else
                {
                    var nested = tuple.Items[0] as CssTupleValue;

                    if (nested != null && nested.Items.Length == 3 && nested.Items[0] == null && nested.Items[1] == null)
                    {
                        return nested.Items[2];
                    }
                }

                return new CssConstantValue<Object>(CssKeywords.Auto, null);
            }
        }
    }
}
