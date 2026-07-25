#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class PlaceContentDeclaration
    {
        public static String Name = PropertyNames.PlaceContent;

        public static String[] Longhands = new[]
        {
            PropertyNames.AlignContent,
            PropertyNames.JustifyContent,
        };

        public static IValueConverter Converter = new PlaceContentAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class PlaceContentAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                AlignContentConverter,
                JustifyContentConverter);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var alignContent = values[0];
                var justifyContent = values[1];

                if (alignContent != null || justifyContent != null)
                {
                    return new CssTupleValue(new[] { alignContent, justifyContent });
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
