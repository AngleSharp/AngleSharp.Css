namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using static ValueConverters;

    static class BackgroundPositionDeclaration
    {
        public static String Name = PropertyNames.BackgroundPosition;

        public static String[] Shorthands =
        [
            PropertyNames.Background,
        ];

        public static String[] Longhands =
        [
            PropertyNames.BackgroundPositionX,
            PropertyNames.BackgroundPositionY,
        ];

        public static IValueConverter Converter = new BackgroundPositionAggregator();

        public static ICssValue InitialValue = InitialValues.BackgroundPositionDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        sealed class BackgroundPositionAggregator : IValueConverter, IValueAggregator
        {
            private static readonly IValueConverter converter = Or(PointConverter.FromList(), AssignInitial(InitialValues.BackgroundPositionDecl));

            public ICssValue Convert(StringSource source) =>
                converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var x = values[0] as CssListValue;
                var y = values[1] as CssListValue;

                if (x != null && y != null && x.Items.Length == y.Items.Length)
                {
                    var points = new ICssValue[x.Items.Length];

                    for (var i = 0; i < points.Length; i++)
                    {
                        points[i] = new CssPoint2D(x.Items[i], y.Items[i]);
                    }

                    return new CssListValue(points);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssListValue list)
                {
                    var points = list.Items.OfType<CssPoint2D>();
                    var x = points.Select(m => m.X).ToArray();
                    var y = points.Select(m => m.Y).ToArray();
                    return
                    [
                        new CssListValue(x),
                        new CssListValue(y),
                    ];
                }

                return null;
            }
        }
    }
}
