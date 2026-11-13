#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
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
            private static readonly PlaceShorthandConverter converter = new PlaceShorthandConverter(
                AlignItemsConverter,
                JustifyItemsConverter,
                null);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values) => converter.Merge(values[0], values[1]);

            public ICssValue[] Split(ICssValue value) => converter.Split(value);
        }
    }
}
