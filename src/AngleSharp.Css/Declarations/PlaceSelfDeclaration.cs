#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class PlaceSelfDeclaration
    {
        public static String Name = PropertyNames.PlaceSelf;

        public static String[] Longhands = new[]
        {
            PropertyNames.AlignSelf,
            PropertyNames.JustifySelf,
        };

        public static IValueConverter Converter = new PlaceSelfAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class PlaceSelfAggregator : IValueAggregator, IValueConverter
        {
            private static readonly PlaceShorthandConverter converter = new PlaceShorthandConverter(
                AlignSelfConverter,
                JustifySelfConverter,
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
