namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class ListStyleDeclaration
    {
        public static String Name = PropertyNames.ListStyle;

        public static IValueConverter Converter = new ListStyleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ListStyleType,
            PropertyNames.ListStylePosition,
            PropertyNames.ListStyleImage,
        };

        sealed class ListStyleValueConverter : IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                ListStyleConverter.Option(),
                ListPositionConverter.Option(),
                OptionalImageSourceConverter.Option());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }
        }

        sealed class ListStyleAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new ListStyleValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var type = values[0];
                var position = values[1];
                var image = values[2];

                if (type != null || position != null || image != null)
                {
                    return new CssTupleValue(new[] { type, position, image });
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
                        options.Items[2],
                    };
                }

                return null;
            }
        }
    }
}
