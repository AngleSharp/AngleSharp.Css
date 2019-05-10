namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using static ValueConverters;

    static class BorderBottomDeclaration
    {
        public static String Name = PropertyNames.BorderBottom;

        public static IValueConverter Converter = new BorderBottomAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Border,
        };

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderBottomWidth,
            PropertyNames.BorderBottomStyle,
            PropertyNames.BorderBottomColor,
        };

        sealed class BorderBottomAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source) => BorderSideConverter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                if (values.Length == 3)
                {
                    return new CssTupleValue(values);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue options)
                {
                    return options.ToArray();
                }

                return null;
            }
        }
    }
}
