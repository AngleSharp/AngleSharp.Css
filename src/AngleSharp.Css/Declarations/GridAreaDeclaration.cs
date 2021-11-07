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

        public static readonly ICssValue InitialValue = null;

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class GridAreaAggregator : IValueAggregator, IValueConverter
        {
            private static readonly String seperator = " / ";
            private static readonly IValueConverter converter = SlashSeparated(Or(
                Assign<Object>(CssKeywords.Auto, null),
                WithAny(Assign(CssKeywords.Span, true), IntegerConverter, IdentifierConverter))).Many(1, 4, seperator);

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values) => new CssTupleValue(values, seperator);

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue tuple)
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

                return GetItemSimple(tuple, index);
            }

            private static ICssValue GetItemSimple(CssTupleValue tuple, Int32 index)
            {
                if (index <= 2)
                {
                    if (tuple.Items.Length <= index)
                    {if (!int.TryParse(tuple.Items[0].CssText, out int _))
                        {
                            return tuple.Items[0];
                        }
                    }
                }
                else if (index == 3)
                {
                    if (tuple.Items.Length > 1)
                    {
                        if (!int.TryParse(tuple.Items[1].CssText, out int _))
                        {
                            return tuple.Items[1];
                        }
                    }
                    else if (!int.TryParse(tuple.Items[0].CssText, out int _))
                    {
                        return tuple.Items[0];
                    }
                }

                return new Constant<Object>(CssKeywords.Auto, null);
            }
        }
    }
}
