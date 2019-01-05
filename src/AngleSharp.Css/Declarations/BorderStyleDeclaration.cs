namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class BorderStyleDeclaration
    {
        public static String Name = PropertyNames.BorderStyle;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static IValueConverter Converter = new BorderStyleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderRightStyle,
            PropertyNames.BorderTopStyle,
            PropertyNames.BorderBottomStyle,
            PropertyNames.BorderLeftStyle,
        };

        sealed class BorderStyleAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(LineStyleConverter.Periodic(), AssignInitial());

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
