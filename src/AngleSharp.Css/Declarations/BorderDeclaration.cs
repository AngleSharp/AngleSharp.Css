namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;
    using static ValueConverters;

    static class BorderDeclaration
    {
        public static String Name = PropertyNames.Border;

        public static IValueConverter Converter = new BorderAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderStyle,
            PropertyNames.BorderColor,
        };

        sealed class BorderAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                LineWidthConverter.Option(InitialValues.BorderWidthDecl),
                LineStyleConverter.Option(InitialValues.BorderStyleDecl),
                CurrentColorConverter.Option(InitialValues.BorderColorDecl));

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                if (!values.OfType<CssPeriodicValue>().Any())
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
