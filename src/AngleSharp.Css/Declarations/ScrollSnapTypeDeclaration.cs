namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class ScrollSnapTypeDeclaration
    {
        public static String Name = PropertyNames.ScrollSnapType;

        public static IValueConverter Converter = DisplayModeConverter;

        public static ICssValue InitialValue = InitialValues.ScrollSnapTypeDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;

        sealed class GridAreaAggregator : IValueAggregator, IValueConverter
        {
            private static readonly String seperator = " / ";
            private static readonly IValueConverter converter = Or(Assign<Object>(CssKeywords.None, null), SpaceSeparated(And(
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
