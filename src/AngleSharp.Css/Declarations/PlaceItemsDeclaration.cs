#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class PlaceItemsDeclaration
    {
        public static String Name = PropertyNames.PlaceItems;

        public static String[] Longhands = new[]
        {
            PropertyNames.AlignItems,
            PropertyNames.JustifyItems,
        };

        public static IValueConverter Converter = new PlaceItemsAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class PlaceItemsAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                AlignItemsConverter,
                JustifyItemsConverter);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var alignItems = values[0];
                var justifyItems = values[1];

                if (alignItems != null || justifyItems != null)
                {
                    return new CssTupleValue(new[] { alignItems, justifyItems });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var options = value as CssTupleValue;

                if (options != null)
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
