#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class FontSynthesisDeclaration
    {
        public static String Name = PropertyNames.FontSynthesis;

        public static String[] Longhands = new[]
        {
            PropertyNames.FontSynthesisWeight,
            PropertyNames.FontSynthesisStyle,
            PropertyNames.FontSynthesisSmallCaps,
        };

        public static IValueConverter Converter = new FontSynthesisAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand | PropertyFlags.Inherited;

        sealed class FontSynthesisAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                return FontSynthesisConverter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var weight = values[0];
                var style = values[1];
                var smallCaps = values[2];

                // If all are null, return null
                if (weight == null && style == null && smallCaps == null)
                {
                    return null;
                }

                // If any value is present, create a tuple
                return new CssTupleValue(new[] { weight, style, smallCaps });
            }

            public ICssValue[] Split(ICssValue value)
            {
                var tuple = value as CssTupleValue;

                if (tuple != null && tuple.Items.Length == 3)
                {
                    return new[]
                    {
                        tuple.Items[0],
                        tuple.Items[1],
                        tuple.Items[2],
                    };
                }

                // For non-tuple values (like "none"), distribute to all longhands
                return new[] { value, value, value };
            }
        }
    }
}
