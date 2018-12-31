namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class BorderColorDeclaration
    {
        public static String Name = PropertyNames.BorderColor;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderColorAggregator();

        public static PropertyFlags Flags = PropertyFlags.Hashless | PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderTopColor,
            PropertyNames.BorderRightColor,
            PropertyNames.BorderBottomColor,
            PropertyNames.BorderLeftColor,
        };

        sealed class BorderColorAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(CurrentColorConverter.Periodic(), AssignInitial());

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
                var periodic = value as Periodic<ICssValue>;

                if (periodic != null)
                {
                    return new[]
                    {
                        periodic.Top,
                        periodic.Right,
                        periodic.Bottom,
                        periodic.Left,
                    };
                }

                return null;
            }
        }
    }
}
