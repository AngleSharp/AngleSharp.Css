#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class ScrollPaddingDeclaration
    {
        public static String Name = PropertyNames.ScrollPadding;

        public static IValueConverter Converter = new ScrollPaddingAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ScrollPaddingTop,
            PropertyNames.ScrollPaddingRight,
            PropertyNames.ScrollPaddingBottom,
            PropertyNames.ScrollPaddingLeft,
        };

        sealed class ScrollPaddingAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(ScrollPaddingConverter, AssignInitial(InitialValues.ScrollPaddingTopDecl)).Periodic();

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var top = values[0];
                var right = values[1];
                var bottom = values[2];
                var left = values[3];

                if (top != null && right != null && bottom != null && left != null)
                {
                    return new CssPeriodicValue(new[] { top, right, bottom, left });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssPeriodicValue period)
                {
                    return new[] { period.Top, period.Right, period.Bottom, period.Left };
                }

                return null;
            }
        }
    }
}
