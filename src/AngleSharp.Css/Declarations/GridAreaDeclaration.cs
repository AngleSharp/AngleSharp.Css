namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class GridAreaDeclaration
    {
        public static readonly String Name = PropertyNames.GridArea;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridRowStart,
            PropertyNames.GridColumnStart,
            PropertyNames.GridRowEnd,
            PropertyNames.GridColumnEnd,
        };

        public static readonly IValueConverter Converter = new GridAreaAggregator();

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class GridAreaAggregator : IValueAggregator, IValueConverter
        {
            private static readonly String seperator = " / ";
            private static readonly IValueConverter converter = Or(
                SlashSeparated(Or(
                    Assign<Object>(CssKeywords.Auto, null),
                    WithAny(Assign(CssKeywords.Span, true), IntegerConverter, IdentifierConverter))).Many(1, 4, seperator),
                AssignInitial());

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
                var tuple = value as CssTupleValue;

                if (tuple != null)
                {
                    return new[]
                    {
                        tuple.Items[0],
                        GetItem(tuple, 1),
                        GetItem(tuple, 2),
                        GetItem(tuple, 3),
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

                return GetItemSimple(tuple, index / 2);
            }

            private static ICssValue GetItemSimple(CssTupleValue tuple, Int32 index)
            {
                if (tuple.Items.Length > index)
                {
                    var nested = tuple.Items[index] as CssTupleValue;

                    if (nested != null && nested.Items.Length == 3 && nested.Items[0] == null && nested.Items[1] == null)
                    {
                        return nested.Items[2];
                    }
                }

                return new Constant<Object>(CssKeywords.Auto, null);
            }
        }
    }
}
