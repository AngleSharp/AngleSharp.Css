namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class MarginDeclaration
    {
        public static String Name = PropertyNames.Margin;

        public static IValueConverter Converter = new MarginAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.MarginBottom,
            PropertyNames.MarginLeft,
            PropertyNames.MarginTop,
            PropertyNames.MarginRight,
        };

        sealed class MarginAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(AutoLengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero));

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var top = properties.Where(m => m.Name == MarginTopDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var right = properties.Where(m => m.Name == MarginRightDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var bottom = properties.Where(m => m.Name == MarginBottomDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var left = properties.Where(m => m.Name == MarginLeftDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (top != null && right != null && bottom != null && left != null)
                {
                    return new Periodic<ICssValue>(new[] { top, right, bottom, left });
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var period = value as Periodic<ICssValue>;

                if (period != null)
                {
                    return CreateLonghands(period.Top, period.Right, period.Bottom, period.Left);
                }

                return CreateLonghands(null, null, null, null);
            }

            private IEnumerable<ICssProperty> CreateLonghands(ICssValue top, ICssValue right, ICssValue bottom, ICssValue left)
            {
                return new[]
                {
                    new CssProperty(MarginTopDeclaration.Name, MarginTopDeclaration.Converter, MarginTopDeclaration.Flags, top),
                    new CssProperty(MarginRightDeclaration.Name, MarginRightDeclaration.Converter, MarginRightDeclaration.Flags, right),
                    new CssProperty(MarginBottomDeclaration.Name, MarginBottomDeclaration.Converter, MarginBottomDeclaration.Flags, bottom),
                    new CssProperty(MarginLeftDeclaration.Name, MarginLeftDeclaration.Converter, MarginLeftDeclaration.Flags, left),
                };
            }
        }
    }
}
