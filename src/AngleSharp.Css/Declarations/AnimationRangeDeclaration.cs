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

    static class AnimationRangeDeclaration
    {
        public static String Name = PropertyNames.AnimationRange;

        public static String[] Longhands = new[]
        {
            PropertyNames.AnimationRangeStart,
            PropertyNames.AnimationRangeEnd,
        };

        public static IValueConverter Converter = new AnimationRangeAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class AnimationRangeAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var start = AnimationRangeConverter.Convert(source);

                if (start == null)
                {
                    return null;
                }

                var c = source.SkipSpacesAndComments();

                if (c == Symbols.Comma)
                {
                    source.SkipCurrentAndSpaces();
                    var end = AnimationRangeConverter.Convert(source);
                    source.SkipSpacesAndComments();

                    if (end == null)
                    {
                        return null;
                    }

                    return new CssTupleValue(new[] { start, end });
                }

                // Single value case: use it for both start and end
                return new CssTupleValue(new[] { start, start });
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var start = values[0];
                var end = values[1];

                if (start != null && end != null)
                {
                    return new CssTupleValue(new[] { start, end });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var tuple = value as CssTupleValue;

                if (tuple != null && tuple.Items.Length >= 2)
                {
                    return new[]
                    {
                        tuple.Items[0],
                        tuple.Items[1],
                    };
                }

                return null;
            }
        }
    }
}
