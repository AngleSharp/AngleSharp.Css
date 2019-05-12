namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using static ValueConverters;

    static class BackgroundRepeatDeclaration
    {
        public static String Name = PropertyNames.BackgroundRepeat;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static String[] Longhands = new[]
        {
            PropertyNames.BackgroundRepeatX,
            PropertyNames.BackgroundRepeatY,
        };

        public static IValueConverter Converter = new BackgroundRepeatAggregator();

        public static ICssValue InitialValue = InitialValues.BackgroundRepeatDecl;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class BackgroundRepeatAggregator : IValueConverter, IValueAggregator
        {
            private static readonly IValueConverter converter = Or(BackgroundRepeatsConverter.FromList(), AssignInitial(InitialValues.BackgroundRepeatDecl));

            public ICssValue Convert(StringSource source) =>
                converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var h = values[0] as CssListValue;
                var v = values[1] as CssListValue;

                if (h != null && v != null && h.Items.Length == v.Items.Length)
                {
                    var repeats = new ICssValue[h.Items.Length];

                    for (var i = 0; i < repeats.Length; i++)
                    {
                        repeats[i] = new CssImageRepeatsValue(h.Items[i], v.Items[i]);
                    }

                    return new CssListValue(repeats);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssListValue list)
                {
                    var repeats = list.Items.OfType<CssImageRepeatsValue>();
                    var h = repeats.Select(m => m.Horizontal).ToArray();
                    var v = repeats.Select(m => m.Vertical).ToArray();
                    return new ICssValue[]
                    {
                        new CssListValue(h),
                        new CssListValue(v),
                    };
                }

                return null;
            }
        }
    }
}
