#nullable disable
namespace AngleSharp.Css.Declarations
{
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
            private static readonly IValueConverter converter = WithAny(
                AlignSelfConverter,
                JustifySelfConverter);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var alignSelf = values[0];
                var justifySelf = values[1];

                if (alignSelf != null || justifySelf != null)
                {
                    return new CssTupleValue(new[] { alignSelf, justifySelf });
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
