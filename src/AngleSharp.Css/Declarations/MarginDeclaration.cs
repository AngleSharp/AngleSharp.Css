namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class MarginDeclaration
    {
        public static String Name = PropertyNames.Margin;

        public static IValueConverter Converter = new MarginAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.MarginTop,
            PropertyNames.MarginRight,
            PropertyNames.MarginBottom,
            PropertyNames.MarginLeft,
        };

        sealed class MarginAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(AutoLengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero));

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var top = values[0];
                var right = values[1];
                var bottom = values[2];
                var left = values[3];

                if (top != null && right != null && bottom != null && left != null)
                {
                    return new Periodic<ICssValue>(new[] { top, right, bottom, left });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var period = value as Periodic<ICssValue>;

                if (period != null)
                {
                    return new[] { period.Top, period.Right, period.Bottom, period.Left };
                }

                return null;
            }
        }
    }
}
